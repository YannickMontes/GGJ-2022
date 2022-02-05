using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public BreakableWallData data = null;
    public List<Collider2D> colliders = new List<Collider2D>();
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    private List<Vector2> freeDirs = new List<Vector2>();

    public BreakableWallContainer Container { get; set; } = null;

    private void Awake()
    {
        CheckFreeDirs();
        Respawn();
    }

    public void Respawn()
    {
        ActivateColliders(true);
        CheckSprite();
    }

    public void OnDamaged()
    {
        CheckSprite();
    }

    public void Die()
    {
        ActivateColliders(false);
    }

    private void CheckSprite()
    {
        Sprite newSprite = LevelManager.Instance.allBreakablesWallData.GetWallSprite(freeDirs, Container.CurrentLife);
        foreach(SpriteRenderer spriteRenderer in renderers)
        {
            spriteRenderer.sprite = newSprite;
        }
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

    private void CheckFreeDirs()
    {
        freeDirs.Clear();
        Vector3Int thisWallPos = LevelManager.Instance.BreakableTilemap.WorldToCell(transform.position);
        Vector3Int checkingTile = thisWallPos;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                checkingTile.x = thisWallPos.x + i;
                checkingTile.y = thisWallPos.y + j;
                bool hasNeigh = false;
                foreach (BreakableWall wall in Container.breakableWalls)
                {
                    Vector3Int wallPos = LevelManager.Instance.BreakableTilemap.WorldToCell(wall.transform.position);
                    if (wallPos == checkingTile)
                    {
                        hasNeigh = true;
                        break;
                    }
                }
                if (!hasNeigh)
                {
                    freeDirs.Add(new Vector2(i, j));
                }
            }
        }
    }
}
