using UnityEngine;

public class PreviewPlatform : MonoBehaviour
{
    public bool IsCollidingGround { get; private set; }

    public void OnEnable()
    {
        IsCollidingGround = false;
    }

    private void OnDisable()
    {
        IsCollidingGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsCollidingGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsCollidingGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsCollidingGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsCollidingGround = false;
    }
}
