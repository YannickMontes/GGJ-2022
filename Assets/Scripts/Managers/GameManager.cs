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

    public List<GameAction> toRunOnStart = new List<GameAction>();

    public void Start()
    {
        ChangeState(EGameState.PLATEFORMER);
        toRunOnStart?.Execute(this);
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
