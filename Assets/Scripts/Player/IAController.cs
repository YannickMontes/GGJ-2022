using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    public IABehaviorData iaData = null;
    public PlayerController playerController;

    public bool IsActive { get; private set; }

    public void SetActive(bool active)
    {
        if(active)
        {
            Invoke("RealSetActive", iaData.timeBeforeLaunch);
        }
        else
        {
            IsActive = active;
        }
    }

    private void RealSetActive()
    {
        IsActive = true;
    }

    private void Update()
    {
        if (!IsActive)
            return;

        if(playerController.IsCollidingDown())
        {
            if (HasWallFront() && playerController.Velocity.y <= 0)
                playerController.Jump();
        }
        else if(playerController.Velocity.y <= 0)
        {
            playerController.Jump();
        }

        playerController.XIAInput = 1.0f;
    }

    private bool HasWallFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, iaData.frontRaycastDistance, iaData.groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
