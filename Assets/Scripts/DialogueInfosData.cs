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
        [Serializable]
        public enum EDialogPositionType { TOP_LEFT, BOTTOM_LEFT, TOP_RIGHT }

        public EDialogPositionType dialogPositionType = EDialogPositionType.BOTTOM_LEFT;
        public string key;
        public FMODUnity.EventReference eventRef;
        public CharacterDialogData speaker;
    }

    public List<DialogLine> dialogLines = new List<DialogLine>();
}