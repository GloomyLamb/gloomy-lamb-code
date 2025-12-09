using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTestPlayer : Player
{
    public override void Init()
    {
        input.BindInputEvent(InputMapName.Default, InputActionName.Move, OnMove);
        input.BindInputEvent(InputMapName.Default, InputActionName.Jump, OnJump);
    }

    private void Start()
    {
        InputManager.Instance.UseInput(input);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("Move Started");
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("Jump Started");
        }
    }


  
}
