using System.Collections;
using UnityEngine;

public class ShadowState : IState
{
    protected Shadow shadow;
    protected ShadowStateMachine StateMachine { get; private set; }

    protected MovementType movementType;
    protected AnimType animType;
    protected int animParameterHash;

    protected bool useCoroutine;
    protected Coroutine coroutine;

    public ShadowState(Shadow shadow, ShadowStateMachine stateMachine)
    {
        this.shadow = shadow;
        StateMachine = stateMachine;
    }

    /// <summary>
    /// State 내부 필드 값 설정을 초기화합니다.
    /// </summary>
    /// <param name="movementType">움직임 보정값 조정을 위해 필요한 값</param>
    /// <param name="animParameterHash">애니메이션 해시</param>
    /// <param name="animType">애니메이션 처리 방법</param>
    /// <param name="useCoroutine">enter 시 코루틴 사용 여부</param>
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

    /// <summary>
    /// State 내부에 초기화가 필요한 파라미터 값이 있을 경우 override 합니다.
    /// </summary>
    protected virtual void ResetParameter()
    {
    }

    /// <summary>
    /// 코루틴을 사용하는 경우 Enter 시 코루틴을 시작합니다.
    /// </summary>
    protected virtual void StartCoroutine()
    {
        StopCoroutine();
        coroutine = CustomCoroutineRunner
            .Instance
            .StartCoroutine(StateMachine.StateCoroutineActions[this]?.Invoke());
    }

    private void StopCoroutine()
    {
        if (coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    protected virtual IEnumerator StateCoroutine()
    {
        yield return null;
    }

    #region IState 구현
    /// <summary>
    /// State가 시작할 경우 호출됩니다.
    /// </summary>
    public virtual void Enter()
    {
        // 그림자 움직임 보정값 설정
        shadow.SetMovementMultiplier(movementType);

        // 애니메이션 처리
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

        // 초기화 필요한 필드 초기화
        ResetParameter();

        // 코루틴 사용 시 시작
        if (useCoroutine)
        {
            StartCoroutine();
        }
    }

    /// <summary>
    /// State가 끝날 경우 호출됩니다.
    /// </summary>
    public virtual void Exit()
    {
        // 애니메이션 처리
        switch (animType)
        {
            case AnimType.Bool:
                shadow.Animator.SetBool(animParameterHash, false);
                break;
            default:
                break;
        }

        StopCoroutine();
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
        if (StateMachine.StateUpdateActions.TryGetValue(this, out var action))
        {
            action?.Invoke();
        }
    }

    public virtual void PhysicsUpdate()
    {
        if (StateMachine.StateFixedUpdateActions.TryGetValue(this, out var action))
        {
            action?.Invoke();
        }
    }
    #endregion
}
