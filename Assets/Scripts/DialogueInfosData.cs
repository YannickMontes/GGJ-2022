using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DialogueAudio")]
public class DialogueInfosData : ScriptableObject
{
    [Serializable]
    public class DialogLine
    {
        public string key;
        public AudioClip clip;
        public CharacterDialogData speaker;
    }

    public List<DialogLine> dialogLines = new List<DialogLine>();
}