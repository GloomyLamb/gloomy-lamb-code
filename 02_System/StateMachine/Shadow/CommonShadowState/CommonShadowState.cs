using System;
using System.Collections;
using UnityEngine;

public class CommonShadowState : IState
{
    protected Shadow shadow;
    protected ShadowStateMachine StateMachine { get; private set; }

    protected MovementType movementType;
    protected AnimType animType;
    protected int animParameterHash;

    protected bool useCoroutine;
    protected Coroutine coroutine;

    public event Action OnUpdate;
    public event Action OnFixedUpdate;

    public CommonShadowState(Shadow shadow, ShadowStateMachine stateMachine)
    {
        this.shadow = shadow;
        StateMachine = stateMachine;
    }

    public void Init(
        MovementType movementType,
        int animParameterHash,
        AnimType animType = AnimType.Bool,
        bool useCoroutine = false)
    {
        this.movementType = movementType;
        this.animParameterHash = animParameterHash;
        this.animType = animType;
        this.useCoroutine = useCoroutine;
    }

    protected virtual void ResetParameter()
    {
    }

    protected virtual void StartCoroutine()
    {
        if (coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(StateCoroutine());
            coroutine = null;
        }
        coroutine = CustomCoroutineRunner.Instance.StartCoroutine(StateCoroutine());
    }

    protected virtual IEnumerator StateCoroutine()
    {
        yield return null;
    }

    #region IState 구현
    public virtual void Enter()
    {
        shadow.SetMovementModifier(movementType);
        switch (animType)
        {
            case AnimType.Bool:
                shadow.Animator.SetBool(animParameterHash, true);
                break;
            case AnimType.Trigger:
                shadow.Animator.SetTrigger(animParameterHash);
                break;
            default:
                break;
        }
        ResetParameter();
        if (useCoroutine)
        {
            StartCoroutine();
        }
    }

    public virtual void Exit()
    {
        switch (animType)
        {
            case AnimType.Bool:
                shadow.Animator.SetBool(animParameterHash, false);
                break;
            default:
                break;
        }
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    public virtual void Update()
    {
        OnUpdate?.Invoke();
    }
    #endregion
}
