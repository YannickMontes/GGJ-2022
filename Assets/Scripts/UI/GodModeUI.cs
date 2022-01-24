using System;
using System.Collections.Generic;
using UnityEngine;

public class GodModeUI : MonoBehaviour
{
    public List<GodAction> godActions = new List<GodAction>();

    public string selectedParam = "Selected";

    private int currentSelectedActionIndex = 0;
    private GodAction currentAction = null;

    public void OnEnable()
    {
        currentSelectedActionIndex = 0;
        currentAction = godActions[currentSelectedActionIndex];
        for (int i = 0; i < godActions.Count; i++)
        {
            godActions[i].SetActive(i == currentSelectedActionIndex);
            SetSelectedAnimation(i, currentSelectedActionIndex == i);
        }
    }

    private void OnDisable()
    {
        currentSelectedActionIndex = 0;
        currentAction = null;
        for (int i = 0; i < godActions.Count; i++)
        {
            godActions[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < godActions.Count; i++)
        {
            InputManager.EInput input = Enum.Parse<InputManager.EInput>($"ChooseAction{i + 1}");
            if(InputManager.Instance.GetInputValue(input, InputManager.EInputType.Down))
            {
                ChangeSelectedAction(i);
            }
        }
        if(InputManager.Instance.GetInputValue(InputManager.EInput.DoAction, InputManager.EInputType.Down))
        {
            DoAction();
        }
    }

    public void ChangeSelectedAction(int newActionIndex)
    {
        if(newActionIndex >= 0 && newActionIndex < godActions.Count)
        {
            SetSelectedAnimation(currentSelectedActionIndex, false);
            SetActionActive(currentSelectedActionIndex, false);

            currentSelectedActionIndex = newActionIndex;
            currentAction = godActions[currentSelectedActionIndex];

            SetActionActive(currentSelectedActionIndex, true);
            SetSelectedAnimation(currentSelectedActionIndex, true);
        }
    }

    private void SetSelectedAnimation(int animatorIndex , bool selected)
    {
        godActions[animatorIndex].animator.SetBool(selectedParam, selected);
    }

    private void SetActionActive(int actionIndex, bool active)
    {
        godActions[actionIndex].SetActive(active);
    }

    private void DoAction()
    {
        currentAction.TryExecute();
    }
}
