public class SlimeShadowStateMachine : ShadowStateMachine
{
    public SlimeShadow Shadow { get; private set; }

    #region States
    // Ground
    public IState WalkState { get; private set; }
    public IState RunState { get; private set; }
    public IState ExpandState { get; private set; }
    public IState ReduceState { get; private set; }
    public IState TransformState { get; private set; }
    #endregion

    // 추적
    public int ChaseCount { get; private set; } = 0;

    public SlimeShadowStateMachine(SlimeShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        IdleState = new SlimeShadowIdleState(this);

        WalkState = new SlimeShadowWalkState(this);
        RunState = new SlimeShadowRunState(this);
        ExpandState = new SlimeShadowExpandState(this);
        ReduceState = new SlimeShadowReduceState(this);

        TransformState = new SlimeShadowTransformState(this);

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
