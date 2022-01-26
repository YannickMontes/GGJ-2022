using UnityEngine;

[CreateAssetMenu(menuName = "Data/HealthData")]
public class HealthControllerData : ScriptableObject
{
    public int maxLife = 1;
    public float invincibilityTime = 1.5f;
}
