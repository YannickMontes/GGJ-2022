using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Data/AllBreakables")]
public class AllBreakableWallsData : ScriptableObject
{
    [Serializable]
    public class BreakableWallDirection
    {
        public List<Sprite> sprites = new List<Sprite>();
        public Sprite wallSprite = null;
        public List<Vector2> mandatoryDirs = new List<Vector2>();
        public List<Vector2> additionnalDirs = new List<Vector2>();
    }

    public Sprite GetWallSprite(List<Vector2> freeDirs, int life)
    {
        int maxIntersect = int.MinValue;
        BreakableWallDirection maxIntersectWall = null;
        foreach(BreakableWallDirection breakableWallDirection in allWallDirections)
        {
            IEnumerable<Vector2> mandatoryIntersect = freeDirs.Intersect(breakableWallDirection.mandatoryDirs);
            if(mandatoryIntersect.Count() == breakableWallDirection.mandatoryDirs.Count)
            {
                int count = mandatoryIntersect.Count();
                IEnumerable<Vector2> additionnalIntersect = freeDirs.Intersect(breakableWallDirection.additionnalDirs);
                count += additionnalIntersect.Count();
                if (count > maxIntersect)
                {
                    maxIntersect = count;
                    maxIntersectWall = breakableWallDirection;
                }
            }
        }
        for (int i = 0; i < thresholds.Count; i++)
        {
            if(thresholds[i].y >= life && life > thresholds[i].x)
            {
                return maxIntersectWall.sprites[i];
            }
        }
        return maxIntersectWall.wallSprite;
    }

    public FMODUnity.EventReference GetEventToLaunch(int breakableLife)
    {
        int index = Mathf.Clamp(breakableLife - 1, 0, onClickEvents.Count - 1);
        return onClickEvents[index];
    }

    public List<Vector2> thresholds = new List<Vector2>();
    public List<FMODUnity.EventReference> onClickEvents = new List<FMODUnity.EventReference>();
    public List<BreakableWallDirection> allWallDirections = new List<BreakableWallDirection>();
}
