using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : GlobalSingletonManager<InputManager>
{
    // todo : 리스트로 바꿔야 할 수도 있을 수 있으니 수정 고려해 두기
    InputHandler nowInputHandler;


    protected override void Init()
    {
    }

    /// <summary>
    /// 사용할 InputHandler 넣어주기
    /// 컨트롤 시작할 플레이어나 오브젝트(UI 등) 
    /// </summary>
    /// <param name="inputHandler"></param>
    public void UseInput(InputHandler inputHandler)
    {
        nowInputHandler?.SetEnableInput(false);

        if (inputHandler == null) return;

        nowInputHandler = inputHandler;
        nowInputHandler.SetEnableInput(true);
        
        if(nowInputHandler.UseCursor)
            ShowCursor();
        else
            HideCursor();
    }

    /// <summary>
    /// 현재 Input 막기
    /// </summary>
    /// <param name="isLock"></param>
    public void LockInput()
    {
        nowInputHandler?.SetEnableInput(false);
    }

    /// <summary>
    /// 현재 Input 풀기
    /// </summary>
    /// <param name="isLock"></param>
    public void UnlockInput(bool isLock)
    {
        nowInputHandler?.SetEnableInput(true);
    }
    
    
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

}
