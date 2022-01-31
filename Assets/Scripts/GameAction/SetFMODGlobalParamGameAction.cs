using UnityEngine;

[CreateAssetMenu(menuName = "GameAction/Set Global Param action")]
public class SetFMODGlobalParamGameAction : GameAction
{
    public string paramName = null;
    public float value = 0.0f;

    public override void Execute(MonoBehaviour behavior)
    {
        AudioManager.Instance.SetGlobalParameter(paramName, value);
    }
}