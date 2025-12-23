using UnityEngine;

public class SlimeShadowStateMachine : ShadowStateMachine
{
    public SlimeShadow SlimeShadow { get; private set; }
    // todo: state 만들기
    // idle, chase, +) transform
    // Ground
    public SlimeShadowIdleState IdleState { get; private set; }
    public SlimeShadowWalkState WalkState { get; private set; }
    public SlimeShadowRunState RunState { get; private set; }
    public SlimeShadowExpandState ExpandState { get; private set; }
    public SlimeShadowReduceState ReduceState { get; private set; }
    public SlimeShadowTransformState TransformState { get; private set; }

    // Battle
    public SlimeShadowHitState HitState { get; private set; }
    public SlimeShadowBoundState BoundState { get; private set; }

    public SlimeShadowStateMachine(Shadow shadow, Animator animator) : base(shadow, animator)
    {
        SlimeShadow = shadow as SlimeShadow;

        IdleState = new SlimeShadowIdleState(this);

        WalkState = new SlimeShadowWalkState(this);
        RunState = new SlimeShadowRunState(this);
        ExpandState = new SlimeShadowExpandState(this);
        ReduceState = new SlimeShadowReduceState(this);

        TransformState = new SlimeShadowTransformState(this);

        HitState = new SlimeShadowHitState(this);
        BoundState = new SlimeShadowBoundState(this);

        ChangeState(IdleState);
    }

    public void StopAnimator()
    {
        animator.speed = 0f;
    }

    public void StartAnimator()
    {
        animator.speed = 1f;
    }
}
