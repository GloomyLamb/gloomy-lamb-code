using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 그림자 상태 머신 - 기본
/// </summary>
public class ShadowStateMachine : StateMachine
{
    #region 필드
    public Shadow Shadow { get; private set; }
    protected bool hasBeenBound = false;

    protected readonly Dictionary<IState, UnityAction> stateUpdateActions = new();
    protected readonly Dictionary<IState, UnityAction> stateFixedUpdateActions = new();
    protected readonly Dictionary<IState, Func<IEnumerator>> stateCoroutineFuncs = new();

    public ShadowState IdleState { get; protected set; }
    public ShadowState ChaseState { get; protected set; }
    public ShadowState TransformState { get; private set; }
    public ShadowState BoundState { get; private set; }
    #endregion

    #region 초기화
    /// <summary>
    /// 생성자 : 각종 State를 생성
    /// </summary>
    /// <param name="shadow"></param>
    public ShadowStateMachine(Shadow shadow)
    {
        Shadow = shadow;

        IdleState = new ShadowIdleState(shadow, this);
        ChaseState = new ShadowChaseState(shadow, this);
        TransformState = new ShadowState(shadow, this);
        BoundState = new ShadowState(shadow, this);
    }

    /// <summary>
    /// State에 각종 정보 전달
    /// </summary>
    public virtual void Init()
    {
        IdleState.Init(MovementType.Stop, Shadow.AnimationData.IdleParameterHash, AnimType.Bool);
        ChaseState.Init(MovementType.Run, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        TransformState.Init(MovementType.Stop, Shadow.AnimationData.TransformParameterHash, AnimType.Trigger);
        BoundState.Init(MovementType.Stop, Shadow.AnimationData.BoundParameterHash, AnimType.Trigger);
    }

    /// <summary>
    /// State의 Action을 담은 딕셔너리 초기화
    /// </summary>
    public virtual void Register()
    {
        // Update
        stateUpdateActions[IdleState] = HandleIdleStateUpdate;
        stateUpdateActions[ChaseState] = HandleChaseStateUpdate;

        // FixedUpdate
        stateFixedUpdateActions[ChaseState] = HandleChaseStateFixedUpdate;

        // Coroutine
        stateCoroutineFuncs[TransformState] = HandleTransformStateCoroutine;
        stateCoroutineFuncs[BoundState] = HandleBoundStateCoroutine;
    }
    #endregion

    /// <summary>
    /// 다음 상태로 넘어갈 수 있는지 확인합니다.
    /// </summary>
    /// <param name="nextState"></param>
    /// <returns></returns>
    public override bool CanChange(IState nextState)
    {
        if (nextState == TransformState && CurState is ITransmutableState)
        {
            return true;
        }

        if (nextState == BoundState && CurState is IBindableState)
        {
            return true;
        }

        return false;
    }

    #region 딕셔너리 관리
    public bool TryGetUpdateAction(IState state, out UnityAction action)
    {
        return stateUpdateActions.TryGetValue(state, out action);
    }

    public bool TryGetFixedUpdateAction(IState state, out UnityAction action)
    {
        return stateFixedUpdateActions.TryGetValue(state, out action);
    }

    public bool TryGetCoroutineFunc(IState state, out Func<IEnumerator> func)
    {
        return stateCoroutineFuncs.TryGetValue(state, out func);
    }
    #endregion

    #region 상태 Update 내부 로직
    /// <summary>
    /// 기본 상태 Update
    /// </summary>
    protected virtual void HandleIdleStateUpdate()
    {
        if (Shadow.Target != null)
        {
            ChangeState(ChaseState);
        }
    }

    /// <summary>
    /// 추적 상태 Update
    /// </summary>
    protected virtual void HandleChaseStateUpdate()
    {
        if (Shadow.Target == null)
        {
            ChangeState(IdleState);
        }
    }
    #endregion

    /// <summary>
    /// 추적 상태 FixedUpdate
    /// </summary>
    protected virtual void HandleChaseStateFixedUpdate()
    {
        Shadow.OnMove?.Invoke();
    }

    #region 상태 Coroutine 내부 로직
    protected virtual IEnumerator HandleTransformStateCoroutine()
    {
        yield return new WaitForSeconds(Shadow.TransformDuration);
        SoundManager.Instance.PlaySfxOnce(SfxName.Transform);
        Shadow.Transform();
    }

    protected virtual IEnumerator HandleBoundStateCoroutine()
    {
        hasBeenBound = true;
        yield return new WaitForSeconds(Shadow.BoundStopPoint);
        Shadow.Animator.speed = 0f;
        yield return new WaitForSeconds(Shadow.BoundDuration);
        Shadow.Animator.speed = 1f;
        ChangeState(IdleState);
    }
    #endregion
}
