using UnityEngine.Tilemaps;

public class LevelManager : Singleton<LevelManager>
{
    public Tilemap GroundTilemap { get; private set; }

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
