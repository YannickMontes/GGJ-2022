using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DialogueAudio")]
public class DialogueInfosData : ScriptableObject
{
    public List<string> key; 
    public List<AudioClip> audioClip;
    public List<bool> character;
    public List<Sprite> dialogueImage;
}
