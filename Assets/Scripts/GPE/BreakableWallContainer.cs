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
        CurrentLife = data.maxLife;
    }

    public void Respawn()
    {
        CurrentLife = data.maxLife;
        foreach(BreakableWall breakableWall in breakableWalls)
        {
            breakableWall.Respawn();
        }
    }

    public void ApplyDamages(int damages)
    {
        CurrentLife = Mathf.Clamp(CurrentLife - damages, 0, data.maxLife);
        foreach(BreakableWall breakableWall in breakableWalls)
        {
            breakableWall.OnDamaged();
        }
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
