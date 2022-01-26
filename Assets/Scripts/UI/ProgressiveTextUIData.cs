using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProgressiveTextUI")]
public class ProgressiveTextUIData : ScriptableObject
{
    public float defaultTimeBetweenChars = 0.1f;
    public float speedUpTimeBetweenChars = 0.0001f;
}
