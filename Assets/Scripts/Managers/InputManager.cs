using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float XInput { get; private set; } = 0.0f;
    public bool JumpUp { get; private set; } = false;
    public bool JumpDown { get; private set; } = false;
    public float LastJumpTime { get; private set; } = 0.0f;

    public void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        JumpDown = Input.GetKeyDown(KeyCode.Space);
        JumpUp = Input.GetKeyUp(KeyCode.Space);
        if (JumpDown)
        {
            LastJumpTime = Time.time;
        }
    }
}
