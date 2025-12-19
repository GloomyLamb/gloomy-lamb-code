using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleInputBind : MonoBehaviour
{
    private void Start()
    {
        // input 연결
        InputManager.Instance.BindInputEvent(InputType.Player,InputMapName.Default,InputActionName.Jump,OnJump);

        // 사용
        InputManager.Instance.UseInput(InputType.Player);
    }

    // 또는 FixedUpdate에서
    protected void Update()
    {
        Move();

        if (InputManager.Instance.IsPressed(InputType.Player,InputActionName.Move))
        {
            Logger.Log("Move Pressed");
        }
    }

    void Move()
    {
        // Axis 뽑아오는 법!
        Logger.Log(InputManager.Instance.GetAxis(InputType.Player, InputActionName.Move).ToString());
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("Jump Started");
        }
    }


  
}
