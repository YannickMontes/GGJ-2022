using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject plateformerUI = null;
    public GameObject godModeUI = null;

    private void Start()
    {
        GameManager.Instance.OnChangeStateAction += OnGameManagerStateChanged;
        OnGameManagerStateChanged(GameManager.Instance.CurrentState);
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
}
