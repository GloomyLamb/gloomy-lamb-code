using UnityEngine;

public class IdleShadowStateMachine : ShadowStateMachine
{
    public IdleShadow IdleShadow { get; private set; }
    // todo: state 만들기
    // idle, chase, +) transform
    // Ground
    public IdleShadowIdleState IdleState { get; private set; }
    public IdleShadowWalkState WalkState { get; private set; }
    public IdleShadowRunState RunState { get; private set; }
    public IdleShadowExpandState ExpandState { get; private set; }
    public IdleShadowTransformState TransformState { get; private set; }

    // Attack
    public IdleShadowHitState HitState { get; private set; }
    public IdleShadowBoundState BoundState { get; private set; }

    public IdleShadowStateMachine(Shadow shadow, Animator animator) : base(shadow, animator)
    {
        IdleShadow = shadow as IdleShadow;

        IdleState = new IdleShadowIdleState(this);
        WalkState = new IdleShadowWalkState(this);
        RunState = new IdleShadowRunState(this);
        ExpandState = new IdleShadowExpandState(this);
        TransformState = new IdleShadowTransformState(this);

        HitState = new IdleShadowHitState(this);
        BoundState = new IdleShadowBoundState(this);

        ChangeState(IdleState);
    }
}
