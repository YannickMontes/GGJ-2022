using UnityEngine;
using UnityEngine.Tilemaps;

public static class Utils
{
    public static Vector3 GetScreenPosFromMouse()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;
        return worldPos;
    }

    public static Vector3 GetCellCenterFromMouse(Tilemap tileMap)
    {
        Vector3 worldPos = GetScreenPosFromMouse();
        Vector3Int cellPos = tileMap.WorldToCell(worldPos);
        return tileMap.GetCellCenterWorld(cellPos);
    }

    public static BreakableWall GetBreakableWallFromMousePos()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;
        RaycastHit2D hit = Physics2D.Raycast(MainCamera.Instance.transform.position, worldPos - MainCamera.Instance.transform.position
            , Mathf.Infinity, LevelManager.Instance.breakableWallLayer);
        if(hit.collider != null)
        {
            return hit.collider.GetComponentInParent<BreakableWall>();
        }
        return null;
    }
}