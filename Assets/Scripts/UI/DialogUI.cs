using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public Image speakerImage = null;
    public TextMeshProUGUI dialogText = null;
    public TextMeshProUGUI speakerName = null;
    public ProgressiveTextUI progressiveTextUI = null;

    public void SetDialogLine(DialogueInfosData.DialogLine dialogLine)
    {
        speakerImage.sprite = dialogLine.speaker.characterSprite;
        string text = DialogManagerUI.Instance.GetTranslation(dialogLine.key);
        progressiveTextUI.DisplayText(text);
        speakerName.text = DialogManagerUI.Instance.GetTranslation(dialogLine.speaker.characterNameKey);
    }
}
