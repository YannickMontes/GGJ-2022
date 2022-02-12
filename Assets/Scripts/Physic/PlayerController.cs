using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public struct RayRange
    {
        public RayRange(float minStart, float maxStart, float minEnd, float maxEnd, Vector2 direction)
        {
            Start = new Vector2(minStart, maxStart);
            End = new Vector2(minEnd, maxEnd);
            Direction = direction;
        }

        public readonly Vector2 Start, End, Direction;
    }

    public struct Collision
    {
        public Collision(bool collide, float distance)
        {
            isColliding = collide;
            collideDistance = distance;
        }

        public readonly float collideDistance;
        public readonly bool isColliding;
    }

    public Vector3 Velocity { get; private set; } = Vector3.zero;
    public bool HasControl { get; private set; } = true;
    public float XIAInput { get; set; } = 0.0f;

    public PlayerControllerData controllerData = null;
    public Animator animator;
    public SpriteRenderer spriteRenderer = null;

    private bool isActivated = false;

    private float horizontalSpeed = 0.0f;
    private float verticalSpeed = 0.0f;
    private float currentFallSpeed = 0;
    private Vector3 lastPosition = Vector3.zero;

    private RayRange downRays, upRays, rightRays, leftRays;
    private Collision downCol, upCol, rightCol, leftCol;
    private bool endedJumpEarly = false;
    private bool coyoteUsable = false;
    private float timeLeftGrounded;
    private float jumpApexValue = 0;
    private bool wasCollidingGroundLastFrame = false;

    private bool CanUseCoyote => coyoteUsable && !downCol.isColliding && timeLeftGrounded + controllerData.coyoteTime > Time.time;
    private bool HasBufferedJump => downCol.isColliding && InputManager.Instance.LastJumpTime + controllerData.jumpBufferTime > Time.time;

    public void SetHasControl(bool hasControl)
    {
        HasControl = hasControl;
        horizontalSpeed = 0.0f;
    }

    public bool IsCollidingDown()
    {
        return downCol.isColliding;
    }

    public void Awake()
    {
        Invoke("Activate", 0.5f);
    }

    public void Activate()
    {
        lastPosition = transform.position;
        isActivated = true;
    }

    public void Update()
    {
        //Too many frames at the same time when hitting play...
        if (!isActivated)
            return;

        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        animator.SetFloat("XVelocity", Velocity.x);
        animator.SetFloat("YVelocity", Velocity.y);
        animator.SetBool("IsGrounded", IsCollidingDown());
        animator.SetBool("IsRunning", Velocity.x != 0.0f);
        if(Velocity.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else if(Velocity.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        lastPosition = transform.position;

        wasCollidingGroundLastFrame = downCol.isColliding;

        CalcultateRayRange();
        ComputeCollisions();
        HandleCoyoteTime(wasCollidingGroundLastFrame);
        HandleHorizontalMovement();
        HandleApexJump();
        HandleGravity();
        HandleJump();
        ApplyMovement();
    }

    #region Raycast Collisions 

    private void CalcultateRayRange()
    {
        Bounds actualBounds = new Bounds(transform.position + controllerData.bounds.center, controllerData.bounds.size);
        downRays = new RayRange(actualBounds.min.x + controllerData.fromBoundsOffset, actualBounds.min.y, actualBounds.max.x - controllerData.fromBoundsOffset, actualBounds.min.y, Vector2.down);
        upRays = new RayRange(actualBounds.min.x + controllerData.fromBoundsOffset, actualBounds.max.y, actualBounds.max.x - controllerData.fromBoundsOffset, actualBounds.max.y, Vector2.up);
        leftRays = new RayRange(actualBounds.min.x, actualBounds.min.y + controllerData.fromBoundsOffset, actualBounds.min.x, actualBounds.max.y - controllerData.fromBoundsOffset, Vector2.left);
        rightRays = new RayRange(actualBounds.max.x, actualBounds.min.y + controllerData.fromBoundsOffset, actualBounds.max.x, actualBounds.max.y - controllerData.fromBoundsOffset, Vector2.right);
    }

    private void ComputeCollisions()
    {
        downCol = CastRays(downRays);
        upCol = CastRays(upRays);
        leftCol = CastRays(leftRays);
        rightCol = CastRays(rightRays);
    }

    private Collision CastRays(RayRange rayRange)
    {
        bool isColliding = false;
        float minDistance = float.MaxValue;
        for (int i = 0; i < controllerData.nbRayToCast; i++)
        {
            float rayPosition = (float)i / (controllerData.nbRayToCast - 1);
            Vector3 beginRayPosition = Vector3.Lerp(rayRange.Start, rayRange.End, rayPosition);

            RaycastHit2D hit = Physics2D.Raycast(beginRayPosition, rayRange.Direction, controllerData.rayRange, controllerData.groundLayer);
            if(hit.collider != null)
            {
                isColliding = true;
                float distance = Vector3.Distance(beginRayPosition, hit.point);
                if(distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }
        return new Collision(isColliding, minDistance);
    }

    #endregion

    private void HandleHorizontalMovement()
    {
        float horizontalInput = HasControl ? InputManager.Instance.XInput : XIAInput;
        if(horizontalInput != 0)
        {
            //Accelerate to max Speed
            horizontalSpeed += horizontalInput * Time.deltaTime * controllerData.acceleration;
            horizontalSpeed = Mathf.Clamp(horizontalSpeed, -controllerData.maxHorizontalSpeed, controllerData.maxHorizontalSpeed);
        }
        else
        {
            //Approach to zero without having a delta between original horizontalSpeed too high (respecting deceleration).
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, controllerData.deceleration * Time.deltaTime);
        }

        if (horizontalSpeed > 0 && rightCol.isColliding|| horizontalSpeed < 0 && leftCol.isColliding)
        {
            horizontalSpeed = 0;
        }
    }

    private void HandleGravity()
    {
        if(downCol.isColliding)
        {
            if(verticalSpeed < 0.0f)
                verticalSpeed = 0.0f;
        }
        else
        {
            StopAllCoroutines();
            float fallSpeed = endedJumpEarly && verticalSpeed > 0 
                ? currentFallSpeed * controllerData.endEarlyJumpModifier //Fall faster if we release jump early
                : currentFallSpeed;
            verticalSpeed -= fallSpeed * Time.deltaTime;
            if (verticalSpeed < controllerData.fallClamp)
                verticalSpeed = controllerData.fallClamp;
        }
    }

    private void HandleCoyoteTime(bool wasCollidingLastFrame)
    {
        if (wasCollidingLastFrame && !downCol.isColliding)
            timeLeftGrounded = Time.time;
        else if (!wasCollidingLastFrame && downCol.isColliding)
            coyoteUsable = true;
    }

    public void HandleApexJump()
    {
        //Apex jump is the summit of the jump curve.
        //The closer we are to the summit, the faster we will fall
        //The threshold is here to determine where we start to fall faster on the curve
        if(!downCol.isColliding)
        {
            jumpApexValue = Mathf.InverseLerp(controllerData.jumpApexThreshold, 0, Mathf.Abs(Velocity.y));
            currentFallSpeed = Mathf.Lerp(controllerData.minFallSpeed, controllerData.maxFallSpeed, jumpApexValue);
        }
        else
        {
            jumpApexValue = 0;
        }
    }

    public void HandleJump()
    {
        if (HasControl && InputManager.Instance.JumpDown)
        {
            JumpWithDataForce();
        }

        if (!downCol.isColliding && InputManager.Instance.JumpUp && !endedJumpEarly && Velocity.y > 0)
        {
           endedJumpEarly = true;
        }

        if (upCol.isColliding)
        {
            if (verticalSpeed > 0)
                verticalSpeed = 0;
        }
    }

    public void JumpWithDataForce()
    {
        Jump(controllerData.jumpSpeed);
    }

    public void JumpWithOverrideForce(float force)
    {
        Jump(force);
    }

    private void Jump(float jumpSpeed)
    {
        if(downCol.isColliding || CanUseCoyote || HasBufferedJump)
        {
            AudioManager.Instance.StartEvent(controllerData.jumpEventRef);
            verticalSpeed = jumpSpeed;
            endedJumpEarly = false;
            coyoteUsable = false;
            timeLeftGrounded = float.MinValue;
        }
    }

    public void ApplyMovement()
    {
        Vector3 moveSpeed = new Vector2(horizontalSpeed, verticalSpeed) * Time.deltaTime;
        if (!wasCollidingGroundLastFrame && downCol.isColliding)
        {
            moveSpeed.y = moveSpeed.y - downCol.collideDistance + 0.001f;
        }
        Vector3 furthestPoint = transform.position + moveSpeed + controllerData.bounds.center;

        Collider2D hit = Physics2D.OverlapBox(furthestPoint, controllerData.bounds.size, 0, controllerData.groundLayer);
        if (hit == null)
        {
            transform.position += moveSpeed;
            StickToGround();
            return;
        }

        Vector3 beginPosition = transform.position;
        var positionToMoveTo = transform.position;
        for (int i = 1; i < controllerData.collidersIterations; i++)
        {
            //Move slowly to final position
            float finalPosPercentage = (float)i / controllerData.collidersIterations;
            Vector2 posToTry = Vector2.Lerp(beginPosition, furthestPoint, finalPosPercentage);
            if (Physics2D.OverlapBox(posToTry, controllerData.bounds.size, 0, controllerData.groundLayer))
            {
                transform.position = positionToMoveTo;
                //if (i == 1)
                //{
                //    if (verticalSpeed < 0)
                //        verticalSpeed = 0;
                //    var dir = transform.position - hit.transform.position;
                //    transform.position += dir.normalized * moveSpeed.magnitude;
                //}
                return;
            }
            positionToMoveTo = posToTry;
        }
         StickToGround();
    }

    private void StickToGround()
    {
        //if(!wasCollidingGroundLastFrame && downCol.isColliding)
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.y -= downCol.collideDistance;
        //    transform.position = newPos;
        //}
    }

    private void OnDrawGizmos()
    {
        //Draw bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + controllerData.bounds.center, controllerData.bounds.size);

        //Draw raycasts
        if (controllerData.drawRaycast)
        {
            CalcultateRayRange();
            Gizmos.color = Color.blue;
            foreach (RayRange rayRange in new List<RayRange> { upRays, rightRays, downRays, leftRays})
            {
                for (int i = 0; i < controllerData.nbRayToCast; i++)
                {
                    float lerpTime = (float)i / (controllerData.nbRayToCast - 1);
                    Vector3 beginPos = Vector3.Lerp(rayRange.Start, rayRange.End, lerpTime);
                    Gizmos.DrawRay(beginPos, rayRange.Direction * controllerData.rayRange);
                }
            }
        }
    }
}
