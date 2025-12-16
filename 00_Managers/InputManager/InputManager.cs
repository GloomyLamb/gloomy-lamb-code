using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : GlobalSingletonManager<InputManager>
{
    Dictionary<InputType, InputHandler> inputHandlers;

    protected override void Init()
    {
        inputHandlers = new Dictionary<InputType, InputHandler>();

        foreach (InputType type in Enum.GetValues(typeof(InputType)))
        {
            inputHandlers[type] = null;
        }
        
    }

    /// <summary>
    /// 사용할 InputHandler 넣어주기
    /// 컨트롤 시작할 플레이어나 오브젝트(UI 등) 
    /// </summary>
    /// <param name="inputHandler"></param>
    public void UseInput(InputHandler inputHandler)
    {
        if (inputHandler == null) return;

        inputHandlers[inputHandler.Type]?.SetEnableInput(false);
        inputHandlers[inputHandler.Type] = inputHandler;
        inputHandler.SetEnableInput(true);
        Logger.Log("Enable Input");
        
    }

    /// <summary>
    /// Input 막기
    /// </summary>
    /// <param name="isLock"></param>
    public void LockInput(InputType inputType)
    {
        inputHandlers[inputType]?.SetEnableInput(false);
    }

    /// <summary>
    /// Input 풀기
    /// </summary>
    /// <param name="isLock"></param>
    public void UnlockInput(InputType inputType)
    {
        inputHandlers[inputType]?.SetEnableInput(true);
    }
    
    /// <summary>
    /// 해당 Input 빼고 모두 꺼줌.
    /// 기존 인풋 상태를 반환하니 저장했다가 필요할 때 Restore 해주기
    /// </summary>
    /// <param name="inputType"></param>
    /// <returns></returns>
    public Dictionary<InputType, bool> SoloInput(InputType inputType)
    {
        Dictionary<InputType, bool> originState =  new Dictionary<InputType, bool>();
        
        foreach (InputHandler inputHandler in inputHandlers.Values)
        {
            if(inputHandler == null) continue;
            
            originState.Add(inputHandler.Type, inputHandler.Enabled);
            
            if (inputHandler.Type == inputType)
                UnlockInput(inputHandler.Type);
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
        if(states == null) return;

        foreach (InputType inputType in states.Keys)
        {
            if(states[inputType])
                UnlockInput(inputType);
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
            inputHandlers[type] = null;
        }
    }
}
