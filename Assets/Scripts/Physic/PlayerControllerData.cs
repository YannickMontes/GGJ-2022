using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerControllerData")]
public class PlayerControllerData : ScriptableObject
{
    public Bounds bounds = new Bounds(Vector3.zero, Vector3.one);

    [Header("Collision Detection")]
    public int nbRayToCast = 5;
    public float fromBoundsOffset = 0.1f;
    public float rayRange = 0.3f;
    public int collidersIterations = 10;
    public LayerMask groundLayer;
    public LayerMask breakableLayer;

    [Header("Horizontal Movement")]
    public float maxHorizontalSpeed = 10.0f;
    public float acceleration = 50.0f;
    public float deceleration = 50.0f;

    [Header("Gravity")]
    public float fallClamp = -40.0f;
    public float minFallSpeed = 10.0f;
    public float maxFallSpeed = 50.0f;

    [Header("Jump")]
    public float jumpSpeed = 10.0f;
    public float jumpApexThreshold = 10.0f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float endEarlyJumpModifier = 3;

    [Header("Health")]
    public HealthControllerData healthData = null;

    [Header("Audio")]
    public FMODUnity.EventReference jumpEventRef;
    public FMODUnity.EventReference footstepEventRef;

    [Header("Debug")]
    public bool drawRaycast = true;

}