using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Serializable]
    public enum EInput
    {
        Jump,
        ChooseAction1,
        ChooseAction2,
        ChooseAction3,
        DoAction,
        Interact
    }

    [Serializable]
    public enum EInputType { Down, Pressed, Up }

    public struct InputValue
    {
        public InputValue(bool init)
        {
            byType = new Dictionary<EInputType, bool>();
            foreach(EInputType inputType in Enum.GetValues(typeof(EInputType)))
            {
                byType.Add(inputType, init);
            }
        }

        public Dictionary<EInputType, bool> byType; 
    }

    public float XInput { get; private set; } = 0.0f;
    public bool JumpUp { get; private set; } = false;
    public bool JumpDown { get; private set; } = false;
    public float LastJumpTime { get; private set; } = 0.0f;

    private Dictionary<EInput, InputValue> inputValues = new Dictionary<EInput, InputValue>();

    public bool GetInputValue(EInput input, EInputType inputType)
    {
        return inputValues[input].byType[inputType];
    }

    protected override void Awake()
    {
        base.Awake();
        foreach(EInput inputName in Enum.GetValues(typeof(EInput)))
        {
            inputValues.Add(inputName, new InputValue(false));
        }
    }

    private void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        JumpDown = Input.GetKeyDown(KeyCode.Space);
        JumpUp = Input.GetKeyUp(KeyCode.Space);
        if (JumpDown)
        {
            LastJumpTime = Time.time;
        }

        foreach(KeyValuePair<EInput, InputValue> input in inputValues)
        {
            InputValue inputValue = input.Value;
            inputValue.byType[EInputType.Down] = Input.GetButtonDown(input.Key.ToString());
            inputValue.byType[EInputType.Pressed] = Input.GetButton(input.Key.ToString());
            inputValue.byType[EInputType.Up] = Input.GetButtonUp(input.Key.ToString());
        }
    }
}
