using UnityEngine;

public class IdleShadowStateMachine : ShadowStateMachine
{
    // todo: state 만들기
    // idle, chase, +) transform
    // Ground
    public IdleShadowIdleState IdleState { get; private set; }
    public IdleShadowWalkState WalkState { get; private set; }
    public IdleShadowRunState RunState { get; private set; }
    public IdleShadowTransformState TransformState { get; private set; }

    // Attack
    public IdleShadowHitState HitState { get; private set; }
    public IdleShadowBoundState BoundState { get; private set; }

    public IdleShadowStateMachine(Shadow shadow, Animator animator) : base(shadow, animator)
    {
        IdleState = new IdleShadowIdleState(this);

        ChangeState(IdleState);
    }
}
