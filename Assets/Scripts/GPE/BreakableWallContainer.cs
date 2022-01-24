using System.Collections.Generic;
using UnityEngine;

public class BreakableWallContainer
{
    public BreakableWallData data = null;
    public List<BreakableWall> breakableWalls = new List<BreakableWall>();

    public int CurrentLife { get; private set; } = 0;

    public BreakableWallContainer(BreakableWallData data)
    {
        this.data = data;
    }

    public void Respawn()
    {
        foreach(BreakableWall breakableWall in breakableWalls)
        {
            breakableWall.Respawn();
        }
        CurrentLife = data.maxLife;
    }

    public void ApplyDamages(int damages)
    {
        CurrentLife = Mathf.Clamp(CurrentLife - damages, 0, data.maxLife);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentLife <= 0)
        {
            foreach (BreakableWall breakableWall in breakableWalls)
            {
                breakableWall.Die();
            }
        }
    }
}
