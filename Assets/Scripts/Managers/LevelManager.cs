using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : Singleton<LevelManager>
{
    public Tilemap GroundTilemap { get; private set; }

    private List<GameObject> dynamicSpawnedObjects = new List<GameObject>();

    public GameObject SpawnDynamicObject(GameObject toSpawn, Vector3 position)
    {
        GameObject obj = Instantiate(toSpawn, transform);
        obj.transform.position = position;
        dynamicSpawnedObjects.Add(obj);
        return obj;
    }

    public void ClearDynamicSpawnObjects()
    {
        foreach(GameObject obj in dynamicSpawnedObjects)
        {
            Destroy(obj);
        }
        dynamicSpawnedObjects.Clear();
    }

    protected override void Awake()
    {
        base.Awake();
        SearchForGroundTilemap();
    }

    private void SearchForGroundTilemap()
    {
        foreach (Tilemap tilemap in FindObjectsOfType<Tilemap>())
        {
            if (tilemap.name == "Ground")
                GroundTilemap = tilemap;
        }
    }
}
