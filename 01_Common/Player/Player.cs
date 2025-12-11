using System;
using UnityEngine;
using UnityEngine.InputSystem;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] protected InputActionAsset inputAction;
    protected InputHandler input;
    
    [Header("Pivot")]
    [SerializeField] protected Transform pivot;
    
    public Vector3 Forward => forward;
    protected Vector3 forward;
    
    private void Awake()
    {
        input = new InputHandler(inputAction, InputType.Player);
        forward = transform.forward;
        Init();
    }

    protected abstract void Init();
    
    private void OnDestroy()
    {
        input?.DisposeInputEvent();
    }
    
}
