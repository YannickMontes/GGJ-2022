using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LINQtoCSV;
using System;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : Singleton<DialogueManager>
{
    public IEnumerable<DialogueImportCSV> dialogueImportCSV;
    public CsvFileDescription inputFile = new CsvFileDescription{
        SeparatorChar = ';',
        FirstLineHasColumnNames = true
    };
    public DialogueInfosData dialogueInfo;
    public GameObject godDialogueUI;
    public GameObject humanDialogueUI;

    public Action<AudioClip> OnchangeDialogue = null;
    // Start is called before the first frame update
    void Start()
    {
        godDialogueUI = gameObject.transform.Find("DialogUI").Find("GodDialogueUI").gameObject;
        humanDialogueUI = gameObject.transform.Find("DialogUI").Find("HumanDialogueUI").gameObject;
        CsvContext cc = new CsvContext();
        dialogueImportCSV = cc.Read<DialogueImportCSV>("Test.csv",inputFile);

        /** Debug pour chaque ligne**/ 
        foreach(DialogueImportCSV dialogueKeyLine in dialogueImportCSV){
            Debug.Log(dialogueKeyLine.key);
        }
        /** Find du debug **/
        findDialogue("test");
    }

    // Update is called once per frame
    void Update()
    {        
    }

    void findDialogue(string key){
        foreach(DialogueImportCSV dialogueKeyLine in dialogueImportCSV){
            if(dialogueKeyLine.key==key){
                int index = 0 ;
                foreach(String keyLoop in dialogueInfo.key){
                    if(keyLoop==key){
                        Debug.Log(dialogueKeyLine.dialogueLines);
                        DisplayDialogue(dialogueKeyLine.dialogueLines,
                                        dialogueInfo.audioClip[index],
                                        dialogueInfo.character[index],
                                        dialogueInfo.dialogueImage[index]);
                        index++;
                    }                    
                }
            }
        }
    }

    void DisplayDialogue(string lines,AudioClip clip, bool isGod, Sprite dialogueImage){
        OnchangeDialogue?.Invoke(clip);
        if(isGod){
            godDialogueUI.gameObject.transform.Find("GodImage").GetComponent<UnityEngine.UI.Image>().sprite = dialogueImage;
            godDialogueUI.gameObject.transform.Find("GodDialogue").GetComponent<TextMeshProUGUI>().text = lines;
        } else {
            humanDialogueUI.gameObject.transform.Find("HumanImage").GetComponent<UnityEngine.UI.Image>().sprite = dialogueImage;
            humanDialogueUI.gameObject.transform.Find("HumanDialogue").GetComponent<TextMeshProUGUI>().text = lines;
        }
    }
}
