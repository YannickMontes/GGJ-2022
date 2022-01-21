using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum EGameState
    {
        PLATEFORMER, 
        GOD_MODE
    }

    public EGameState CurrentState { get; private set; }

    public Action<EGameState> OnChangeStateAction = null; 

    public void Start()
    {
        ChangeState(EGameState.PLATEFORMER);
    }

    public void ChangeState(EGameState newState)
    {
        if(newState != CurrentState)
        {
            CurrentState = newState;
            OnChangeStateAction?.Invoke(CurrentState);
        }
    }
}
