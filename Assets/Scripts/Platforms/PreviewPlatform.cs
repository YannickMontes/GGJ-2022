using UnityEngine;

public class PreviewPlatform : MonoBehaviour
{
    public bool IsCollidingGround { get { return nbColliding > 0; } }

    private int nbColliding = 0;

    public void OnEnable()
    {
        nbColliding = 0;
    }

    private void OnDisable()
    {
        nbColliding = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        nbColliding++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nbColliding--;
    }
}
