using System;
using TMPro;
using UnityEngine;

public class ProgressiveTextUI : MonoBehaviour
{
    public TextMeshProUGUI textBox = null;
    public ProgressiveTextUIData data = null;

    public Action OnWriteTextEndAction = null;

    public bool IsWriting { get { return textToDisplay != null; } }

    private string textToDisplay = null;
    private int currentCharIndex = 0;
    private bool isSkiping = false;
    private float lastTimeDisplayedChar = 0.0f;

    public void DisplayText(string text)
    {
        StopAllCoroutines();
        isSkiping = false;
        textToDisplay = text.Substring(1);
        textBox.text = text[0].ToString();
        currentCharIndex = 0;
        lastTimeDisplayedChar = Time.time;
    }

    public void Skip()
    {
        isSkiping = true;
    }

    private void Update()
    {
        if(textToDisplay != null)
        {
            DisplayText();
        }
    }

    public void DisplayText()
    {
        float elapsedTime = Time.time - lastTimeDisplayedChar;
        float nbCharToDisplay = elapsedTime / (isSkiping ? data.speedUpTimeBetweenChars : data.defaultTimeBetweenChars);
        int newCharIndex = (int)Mathf.Clamp(currentCharIndex + Mathf.Round(nbCharToDisplay), 0, textToDisplay.Length);
        while(currentCharIndex < newCharIndex)
        {
            textBox.text += textToDisplay[currentCharIndex];
            currentCharIndex++;
            lastTimeDisplayedChar = Time.time;
        }

        if (currentCharIndex >= textToDisplay.Length)
            OnWriteTextEnd();
    }

    private void OnWriteTextEnd()
    {
        StopAllCoroutines();
        isSkiping = false;
        textToDisplay = null;
        currentCharIndex = 0;
        OnWriteTextEndAction?.Invoke();
    }
}
