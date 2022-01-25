using System.Collections.Generic;
using UnityEngine;
using LINQtoCSV;
using System;
using TMPro;
using UnityEngine.UI;
public class DialogManagerUI : Singleton<DialogManagerUI>
{
    public class Traduction
    {
        public List<string> traductions = new List<string>();
    }

    public GameObject godDialogueUI;
    public GameObject humanDialogueUI;

    public Image godImage = null;
    public TextMeshProUGUI godDialog = null;

    public Image humanImage = null;
    public TextMeshProUGUI humanDialog = null;

    public TextAsset csvText = null;

    public Action<AudioClip> OnchangeDialogue = null;
    public Action OnDialogEnd = null;
    public Action OnDialogNewLine = null;

    private Dictionary<string, Traduction> locKeysTrad = new Dictionary<string, Traduction>();

    private DialogueInfosData currentDialog = null;
    private int currentLineIndex = 0;

    private void OnEnable()
    {
        humanDialogueUI.SetActive(false);
        godDialogueUI.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        string[] lines = csvText.text.Split("\r\n");
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                continue;

            string[] elements = line.Split(',');
            string key = elements[0];
            Traduction trad = new Traduction();
            for (int i = 1; i < elements.Length; i++)
                trad.traductions.Add(elements[i]);
            locKeysTrad[key] = trad;
            Debug.Log($"Key: {key}, FR Trads: {trad.traductions[0]}");
        }
    }

    public void LaunchDialog(DialogueInfosData dialogData)
    {
        if(currentDialog != null)
        {
            return;
        }

        currentDialog = dialogData;
        currentLineIndex = -1;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if(currentDialog.dialogLines.Count > currentLineIndex + 1)
        {
            currentLineIndex++;
            DialogueInfosData.DialogLine dialogLine = currentDialog.dialogLines[currentLineIndex];
            Traduction trad = null;
            locKeysTrad.TryGetValue(dialogLine.key, out trad);
            DisplayDialogue(trad.traductions[0], dialogLine.clip, dialogLine.speaker.isGod, dialogLine.speaker.characterSprite);
            OnDialogNewLine?.Invoke();
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        currentDialog = null;
        currentLineIndex = -1;
        OnDialogEnd?.Invoke();

        humanDialogueUI.SetActive(false);
        godDialogueUI.SetActive(false);
    }

    public void Update()
    {
        if(currentDialog != null)
        {
            if(InputManager.Instance.GetInputValue(InputManager.EInput.Interact, InputManager.EInputType.Down))
            {
                DisplayNextLine();
            }
        }
    }

    void DisplayDialogue(string lines,AudioClip clip, bool isGod, Sprite dialogueImage)
    {
        if(isGod){
            humanDialogueUI.SetActive(false);
            godDialogueUI.SetActive(true);
            godImage.sprite = dialogueImage;
            godDialog.text = lines;
        } else
        {
            humanDialogueUI.SetActive(true);
            godDialogueUI.SetActive(false);
            humanImage.sprite = dialogueImage;
            humanDialog.text = lines;
        }
        AudioManager.Instance.PlayAudio(clip);
    }
}
