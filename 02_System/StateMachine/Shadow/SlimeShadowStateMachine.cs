public class SlimeShadowStateMachine : ShadowStateMachine
{
    public SlimeShadow Shadow { get; private set; }

    #region States
    // Ground
    public IState WalkState { get; private set; }
    public IState RunState { get; private set; }
    public IState ExpandState { get; private set; }
    public IState ReduceState { get; private set; }
    #endregion

    // 추적

    public SlimeShadowStateMachine(SlimeShadow shadow) : base(shadow)
    {
        Shadow = shadow;
        IdleState = new SlimeShadowIdleState(shadow, this);

        WalkState = new SlimeShadowWalkState(shadow, this);
        RunState = new SlimeShadowRunState(shadow, this);
        ExpandState = new SlimeShadowExpandState(shadow, this);
        ReduceState = new SlimeShadowReduceState(shadow, this);
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
