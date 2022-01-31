using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : ScriptableObject
{
    public abstract void Execute(MonoBehaviour behavior);
}

public static class GameActionExtensions
{
    public static void Execute(this List<GameAction> actions, MonoBehaviour behavior)
    {
        foreach(GameAction action in actions)
        {
            if (action == null)
                continue;
            action.Execute(behavior);
        }
    }
}