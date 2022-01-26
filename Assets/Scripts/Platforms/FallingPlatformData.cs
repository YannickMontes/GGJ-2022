using UnityEngine;

[CreateAssetMenu(menuName = "Data/FallingPlatform")]
public class FallingPlatformData : ScriptableObject
{
    public float timeBeforeFall = 1.0f;
    public bool willRespawn = true;
    public float respawnTime = 5.0f;

    public float fallDistance = 4.0f;
    public float fallTime = 0.5f;

    public float respawnAlphaTime = 0.5f;
}
