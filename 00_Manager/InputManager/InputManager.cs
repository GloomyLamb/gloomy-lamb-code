using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : GlobalSingletonManager<InputManager>
{
    [SerializeField] private List<InputActionAsset> inputAssets;
    Dictionary<InputType, InputHandler> inputHandlers;

    protected override void Init()
    {
        inputHandlers = new Dictionary<InputType, InputHandler>();
        for (int i = 0; i < inputAssets.Count; i++)
        {
            string[] fullInputAssetName = inputAssets[i].name.Split("_");
            if (fullInputAssetName.Length > 1)
            {
                string inputAssetName = fullInputAssetName[1];
                if (InputType.TryParse(inputAssetName, out InputType inputType))
                {
                    if (inputHandlers.ContainsKey(inputType) == false)
                    {
                        InputHandler newInputHandler = new InputHandler(inputAssets[i], inputType);
                        inputHandlers.Add(inputType, newInputHandler);
                    }
                }
            }
        }
    }

    public InputHandler GetInputHandler(InputType inputType)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return null;
        return inputHandlers[inputType];
    }

    /// <summary>
    /// 
    /// </summary>
    public void BindInputEvent(InputType inputType, InputMapName inputMapName, InputActionName inputActionName,
        Action<InputAction.CallbackContext> action)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return;
        if (inputHandlers[inputType] != null)
        {
            inputHandlers[inputType].BindInputEvent(inputMapName, inputActionName, action);
        }
    }

    /// <summary>
    /// 원하는 Input Axis 가져오기
    /// Lock 일 때에는 눌러도 zero예요
    /// </summary>
    /// <returns></returns>
    public Vector2 GetAxis(InputType inputType,  InputActionName inputActionName, InputMapName inputMapName= InputMapName.Default)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return Vector2.zero;
        return inputHandlers[inputType] != null ? inputHandlers[inputType].GetAxis(inputActionName, inputMapName) : Vector2.zero;
    }

    /// <summary>
    /// 원하는 Input float 가져오기
    /// Lock 일 때에는 눌러도 0 으로 받아짐
    /// </summary>
    public float GetFloat(InputType inputType, InputActionName inputActionName,
        InputMapName inputMapName = InputMapName.Default)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return 0f;
        return inputHandlers[inputType] != null ? inputHandlers[inputType].GetFloat(inputActionName, inputMapName) : 0f;
    }

    
    public bool IsPressed(InputType inputType, InputActionName inputActionName, InputMapName inputMapName = InputMapName.Default)
    {
        if(inputHandlers.ContainsKey(inputType) == false) return false;
        return inputHandlers[inputType] != null? inputHandlers[inputType].IsPressed(inputActionName, inputMapName) : false;
    }

    /// <summary>
    /// Input 사용하기
    /// </summary>
    public void UseInput(InputType inputType)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return;
        inputHandlers[inputType]?.SetEnableInput(true);
    }

    /// <summary>
    /// Input 막기
    /// </summary>
    public void LockInput(InputType inputType)
    {
        if (inputHandlers.ContainsKey(inputType) == false) return;
        inputHandlers[inputType]?.SetEnableInput(false);
    }

    /// <summary>
    /// 해당 Input 빼고 모두 꺼줌.
    /// 기존 인풋 상태를 반환하니 저장했다가 필요할 때 Restore 해주기
    /// </summary>
    /// <param name="inputType"></param>
    /// <returns></returns>
    public Dictionary<InputType, bool> SoloInput(InputType inputType)
    {
        Dictionary<InputType, bool> originState = new Dictionary<InputType, bool>();

        foreach (InputHandler inputHandler in inputHandlers.Values)
        {
            if (inputHandler == null) continue;

            originState.Add(inputHandler.Type, inputHandler.Enabled);

            if (inputHandler.Type == inputType)
                UseInput(inputHandler.Type);
            else
                LockInput(inputHandler.Type);
        }

        return originState;
    }

    /// <summary>
    /// Input 상태 복구용
    /// </summary>
    /// <param name="states"></param>
    public void RestoreInput(Dictionary<InputType, bool> states)
    {
        if (states == null) return;

        foreach (InputType inputType in states.Keys)
        {
            if (states[inputType])
                UseInput(inputType);
            else
                LockInput(inputType);
        }
    }


    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    protected override void OnSceneUnloaded(Scene scene)
    {
        foreach (InputType type in inputHandlers.Keys)
        {
            if (inputHandlers[type] != null)
            {
                inputHandlers[type].DisposeInputEvent();
            }
        }
    }
}