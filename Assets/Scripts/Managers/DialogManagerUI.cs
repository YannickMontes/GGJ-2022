using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class DialogManagerUI : Singleton<DialogManagerUI>
{
    public class Traduction
    {
        public List<string> traductions = new List<string>();
    }

    [Serializable]
    public class DialogTextBox
    {
        public DialogueInfosData.DialogLine.EDialogPositionType positionType = DialogueInfosData.DialogLine.EDialogPositionType.BOTTOM_LEFT;
        public DialogUI dialogUI = null;
    }

    public List<DialogTextBox> dialogTextBoxes = new List<DialogTextBox>();

   
    public TextAsset csvText = null;

    public Action<AudioClip> OnchangeDialogue = null;
    public Action OnDialogEnd = null;
    public Action OnDialogNewLine = null;

    private Dictionary<string, Traduction> locKeysTrad = new Dictionary<string, Traduction>();

    private DialogueInfosData currentDialog = null;
    private int currentLineIndex = 0;
    private DialogUI currentDialogUI = null;

    public string GetTranslation(string key)
    {
        Traduction trad = null;
        if (locKeysTrad.TryGetValue(key, out trad))
        {
            return trad.traductions[0];
        }
        Debug.LogError($"No trad for this key: {key}");
        return null;
    }

    private void OnEnable()
    {
        DisableDialogUI();
    }

    private void DisableDialogUI()
    {
        foreach (DialogTextBox dialog in dialogTextBoxes)
        {
            dialog.dialogUI.gameObject.SetActive(false);
        }
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
            DisplayDialogue(dialogLine, dialogLine.eventRef);
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
        DisableDialogUI();
    }

    public void Update()
    {
        if(currentDialog != null)
        {
            if(InputManager.Instance.GetInputValue(InputManager.EInput.Interact, InputManager.EInputType.Down))
            {
                if(currentDialogUI.progressiveTextUI.IsWriting)
                {
                    currentDialogUI.progressiveTextUI.Skip();
                }
                else
                {
                    DisplayNextLine();
                }
            }
        }
    }

    void DisplayDialogue(DialogueInfosData.DialogLine line, FMODUnity.EventReference eventRef)
    {
        currentDialogUI?.gameObject.SetActive(false);
        currentDialogUI = null;
        foreach (DialogTextBox textBox in dialogTextBoxes)
        {
            if(textBox.positionType == line.dialogPositionType)
            {
                currentDialogUI = textBox.dialogUI;
                break;
            }
        }
        currentDialogUI.gameObject.SetActive(true);
        currentDialogUI.SetDialogLine(line);
        AudioManager.Instance.StartEvent(eventRef);
    }
}
