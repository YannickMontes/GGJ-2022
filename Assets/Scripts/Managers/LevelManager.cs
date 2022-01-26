using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : Singleton<LevelManager>
{
    public Tilemap GroundTilemap { get; private set; }
    public Tilemap BreakableTilemap { get; private set; }

    public LayerMask breakableWallLayer;

    public Action OnReloadLevelAction = null;

    private List<GameObject> dynamicSpawnedObjects = new List<GameObject>();
    private List<BreakableWallContainer> breakableWallsContainer = new List<BreakableWallContainer>();

    public GameObject SpawnDynamicObject(GameObject toSpawn, Vector3 position)
    {
        GameObject obj = Instantiate(toSpawn, transform);
        obj.transform.position = position;
        dynamicSpawnedObjects.Add(obj);
        return obj;
    }

    public void ReloadLevel()
    {
        ClearDynamicSpawnObjects();
        RefillBreakableWalls();
        OnReloadLevelAction?.Invoke();
    }

    protected override void Awake()
    {
        base.Awake();
        SearchForTilemaps();
        AggregateBreakableWalls();
    }

    private void SearchForTilemaps()
    {
        foreach (Tilemap tilemap in FindObjectsOfType<Tilemap>())
        {
            if (tilemap.name == "Ground")
                GroundTilemap = tilemap;
            else if (tilemap.name == "Breakable")
                BreakableTilemap = tilemap;
        }
    }

    private void AggregateBreakableWalls()
    {
        List<BreakableWall> allWalls = FindObjectsOfType<BreakableWall>().ToList();
        breakableWallsContainer = new List<BreakableWallContainer>();
        while(allWalls.Count > 0)
        {
            BreakableWall currentWall = allWalls[0];
            BreakableWallContainer container = new BreakableWallContainer(currentWall.data);
            currentWall.Container = container;
            breakableWallsContainer.Add(container);
            container.breakableWalls.Add(currentWall);
            allWalls.Remove(currentWall);

            Vector3Int breakableCell = BreakableTilemap.WorldToCell(currentWall.transform.position);
            FillAdjacentTilesWithSameObject(breakableCell, ref container, ref allWalls);
        }
    }

    private void FillAdjacentTilesWithSameObject(Vector3Int cellPos, ref BreakableWallContainer container, ref List<BreakableWall> leftWalls)
    {
        Vector3Int rightCell = new Vector3Int(cellPos.x + 1, cellPos.y, cellPos.z);
        Vector3Int leftCell = new Vector3Int(cellPos.x - 1, cellPos.y, cellPos.z);
        Vector3Int upCell = new Vector3Int(cellPos.x, cellPos.y + 1, cellPos.z);
        Vector3Int downCell = new Vector3Int(cellPos.x, cellPos.y - 1, cellPos.z);

        if(AddCellToList(rightCell, ref container, ref leftWalls))
            FillAdjacentTilesWithSameObject(rightCell, ref container, ref leftWalls);

        if (AddCellToList(leftCell, ref container, ref leftWalls))
            FillAdjacentTilesWithSameObject(leftCell, ref container, ref leftWalls);

        if (AddCellToList(upCell, ref container, ref leftWalls))
            FillAdjacentTilesWithSameObject(upCell, ref container, ref leftWalls);

        if (AddCellToList(downCell, ref container, ref leftWalls))
            FillAdjacentTilesWithSameObject(downCell, ref container, ref leftWalls);
    }

    private bool AddCellToList(Vector3Int cell, ref BreakableWallContainer container, ref List<BreakableWall> leftWalls)
    {
        for (int i = 0; i < leftWalls.Count; i++)
        {
            BreakableWall wall = leftWalls[i];
            Vector3Int wallCell = BreakableTilemap.WorldToCell(wall.transform.position);
            if(wallCell == cell 
                && wall.data == container.data
                && !container.breakableWalls.Contains(wall))
            {
                container.breakableWalls.Add(wall);
                wall.Container = container;
                leftWalls.Remove(wall);
                return true;
            }
        }
        return false;
    }

    private void RefillBreakableWalls()
    {
        foreach (BreakableWallContainer breakableWall in breakableWallsContainer)
        {
            breakableWall.Respawn();
        }
    }

    private void ClearDynamicSpawnObjects()
    {
        foreach (GameObject obj in dynamicSpawnedObjects)
        {
            Destroy(obj);
        }
        dynamicSpawnedObjects.Clear();
    }

}
