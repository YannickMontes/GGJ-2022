using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LINQtoCSV;

public class DialogueImportCSV : ScriptableObject
{
        [CsvColumn(FieldIndex = 1)]
        public string key { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string dialogueLines { get; set; }
}
