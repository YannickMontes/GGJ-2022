using UnityEngine;

[CreateAssetMenu(menuName = "Data/IAController")]
public class IABehaviorData : ScriptableObject
{
    public float timeBeforeLaunch = 2.0f;

    public float frontRaycastDistance = 5.0f;
    public LayerMask groundLayer;
}
