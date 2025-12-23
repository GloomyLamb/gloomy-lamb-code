using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnInputMoveStartAction;
    public event Action<Vector2> OnInputMoveAction;
    public event Action<Vector2> OnInputMoveEndAction;
    public event Action OnInputAttackAction;
    public event Action OnInputJumpAction;
    
    
    private void Start()
    {
        InputManager.Instance.BindInputEvent(InputType.Player, InputMapName.Default, InputActionName.Jump, OnInputJump);
        InputManager.Instance.BindInputEvent(InputType.Player, InputMapName.Default, InputActionName.Move, OnInputMove);
        InputManager.Instance.BindInputEvent(InputType.Player,InputMapName.Default,InputActionName.Attack, OnInputAttack);
    }
    
    private void Update()
    {
        UpdateInputMove();
    }

    
    private void UpdateInputMove()
    {
        Vector2 inputValue = InputManager.Instance.GetAxis(InputType.Player, InputActionName.Move);
        OnInputMoveAction?.Invoke(inputValue);
    }

    void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (context.phase == InputActionPhase.Started)
        {
            OnInputMoveStartAction?.Invoke(inputValue);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnInputMoveEndAction?.Invoke(inputValue);
        }
    }


    void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnInputJumpAction?.Invoke();
        }
    }

    void OnInputAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnInputAttackAction?.Invoke();
        }
    }
    
    
}