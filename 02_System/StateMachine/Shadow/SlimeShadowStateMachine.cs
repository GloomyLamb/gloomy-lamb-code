public class SlimeShadowStateMachine : StateMachine
{
    public SlimeShadow Shadow { get; private set; }
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

    // 추적
    public int ChaseCount { get; private set; } = 0;

    public SlimeShadowStateMachine(SlimeShadow shadow)
    {
        Shadow = shadow;

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

    public void PlusChaseCount()
    {
        ChaseCount++;
        Logger.Log($"추격 횟수: {ChaseCount}");
    }

    public void StopAnimator()
    {
        Shadow.Animator.speed = 0f;
    }

    public void StartAnimator()
    {
        Shadow.Animator.speed = 1f;
    }
}
