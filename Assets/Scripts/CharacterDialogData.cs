using UnityEngine;

[CreateAssetMenu(menuName = "Data/CharacterDialog")]
public class CharacterDialogData : ScriptableObject
{
    public string characterNameKey = null;
    public Sprite characterSprite = null;
    public bool isGod = true;
}