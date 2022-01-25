using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject plateformerUI = null;
    public GameObject godModeUI = null;
    public DialogManagerUI dialogManagerUI = null;

    public void LaunchDialog(DialogueInfosData dialog)
    {
        dialogManagerUI.gameObject.SetActive(true);
        dialogManagerUI.LaunchDialog(dialog);
        dialogManagerUI.OnDialogEnd += OnDialogEnd;
    }

    private void Start()
    {
        GameManager.Instance.OnChangeStateAction += OnGameManagerStateChanged;
        OnGameManagerStateChanged(GameManager.Instance.CurrentState);
        dialogManagerUI.gameObject.SetActive(false);
    }

    private void OnGameManagerStateChanged(GameManager.EGameState newState)
    {
        if(newState == GameManager.EGameState.GOD_MODE)
        {
            plateformerUI.SetActive(false);
            godModeUI.SetActive(true);
        }
        else if(newState == GameManager.EGameState.PLATEFORMER)
        {
            plateformerUI.SetActive(true);
            godModeUI.SetActive(false);
        }
    }

    private void OnDialogEnd()
    {
        dialogManagerUI.gameObject.SetActive(false);
    }
}
