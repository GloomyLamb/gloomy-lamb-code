using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTestPlayer : Player
{
    protected override void Init()
    {
        input.BindInputEvent(InputMapName.Default, InputActionName.Jump, OnJump);
        
    }

    private void Start()
    {
        // 현재 사용할 Input을 InpputManager에게 전달해줘야 함!
        // 이거 안하면 활성화 안되어요
        // input 접근하면 할 수는 있지만 원활한 사용을 위해 Manager 통해서 사용바람! 
        InputManager.Instance.UseInput(input);
    }

    // 또는 FixedUpdate에서
    protected override void Update()
    {
        base.Update();
        Move();
    }

    void Move()
    {
        // Axis 뽑아오는 법!
        Logger.Log(input.GetAxis(InputMapName.Default, InputActionName.Move).ToString());
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("Jump Started");
        }
    }


  
}
