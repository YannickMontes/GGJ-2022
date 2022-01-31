using UnityEngine;

[CreateAssetMenu(menuName = "GameAction/Start Event action")]
public class StartEventGameAction : GameAction
{
    public FMODUnity.EventReference eventRef;

    public override void Execute(MonoBehaviour behavior)
    {
        AudioManager.Instance.StartEvent(eventRef, behavior?.transform);
    }
}
