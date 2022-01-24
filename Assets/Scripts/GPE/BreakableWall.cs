using System.Collections.Generic;
using UnityEngine;

//TODO : Handle visual
public class BreakableWall : MonoBehaviour
{
    public BreakableWallData data = null;
    public List<Collider2D> colliders = new List<Collider2D>();
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    public BreakableWallContainer Container { get; set; } = null;

    private void Awake()
    {
        Respawn();
    }

    public void Respawn()
    {
        ActivateColliders(true);
    }

    public void Die()
    {
        ActivateColliders(false);
    }

    private void ActivateColliders(bool activate)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.gameObject.SetActive(activate);
        }
        foreach(SpriteRenderer renderer in renderers)
        {
            renderer.enabled = activate;
        }
    }
}
