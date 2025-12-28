using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShadowStateMachine : StateMachine
{
    public Shadow Shadow { get; private set; }

    public readonly Dictionary<IState, UnityAction> StateUpdateActions = new();
    public readonly Dictionary<IState, UnityAction> StateFixedUpdateActions = new();
    public readonly Dictionary<IState, Func<IEnumerator>> StateCoroutineActions = new();

    public ShadowState IdleState { get; protected set; }
    public ShadowState ChaseState { get; protected set; }
    public ShadowState TransformState { get; private set; }

    public ShadowState HitState { get; private set; }
    public ShadowState BoundState { get; private set; }

    /// <summary>
    /// 생성자 : 각종 State를 생성
    /// </summary>
    /// <param name="shadow"></param>
    public ShadowStateMachine(Shadow shadow)
    {
        Shadow = shadow;

        IdleState = new ShadowState(shadow, this);
        ChaseState = new ShadowState(shadow, this);
        TransformState = new ShadowState(shadow, this);

        HitState = new ShadowState(shadow, this);
        BoundState = new ShadowState(shadow, this);
    }

    /// <summary>
    /// State에 각종 정보 전달
    /// </summary>
    public virtual void Init()
    {
        IdleState.Init(MovementType.Stop, Shadow.AnimationData.IdleParameterHash, AnimType.Bool);
        ChaseState.Init(MovementType.Run, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        TransformState.Init(MovementType.Stop, Shadow.AnimationData.TransformParameterHash, AnimType.Trigger, true);

        HitState.Init(MovementType.Stop, Shadow.AnimationData.HitParameterHash, AnimType.Trigger, true);
        BoundState.Init(MovementType.Stop, Shadow.AnimationData.BoundParameterHash, AnimType.Trigger, true);
    }

    /// <summary>
    /// State의 Action을 담은 딕셔너리 초기화
    /// </summary>
    public virtual void Register()
    {
        // Update
        StateUpdateActions[IdleState] = HandleIdleStateUpdate;
        StateUpdateActions[ChaseState] = HandleChaseStateUpdate;

        // FixedUpdate
        StateFixedUpdateActions[ChaseState] = HandleChaseStateFixedUpdate;

        // Coroutine
        StateCoroutineActions[TransformState] = HandleTransformStateCoroutine;
        StateCoroutineActions[HitState] = HandleHitStateCoroutine;
        StateCoroutineActions[BoundState] = HandleBoundStateCoroutine;
    }

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
        Shadow.Transform();
    }

    protected virtual IEnumerator HandleHitStateCoroutine()
    {
        yield return new WaitForSeconds(Shadow.HitDuration);
        ChangeState(IdleState);
    }

    protected virtual IEnumerator HandleBoundStateCoroutine()
    {
        yield return new WaitForSeconds(Shadow.BoundStopPoint);
        Shadow.Animator.speed = 0f;
        yield return new WaitForSeconds(Shadow.BoundDuration);
        Shadow.Animator.speed = 1f;
        ChangeState(IdleState);
    }
    #endregion
}
