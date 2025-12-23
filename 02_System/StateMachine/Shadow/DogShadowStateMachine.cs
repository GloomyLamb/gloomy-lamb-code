public class DogShadowStateMachine : ShadowStateMachine
{
    public DogShadow Shadow { get; private set; }

    #region States
    // Ground
    public IState ChaseState { get; private set; }
    public IState IdleState { get; private set; }
    public IState TransformState { get; private set; }

    // Skill
    public IState BiteState { get; private set; }
    public IState BarkState { get; private set; }
    #endregion

    public DogShadowStateMachine(DogShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        ChaseState = new DogShadowChaseState(this);
        IdleState = new DogShadowIdleState(this);
        TransformState = new DogShadowTransformState(this);

        BiteState = new DogShadowBiteState(this);
        BarkState = new DogShadowBarkState(this);

        ChangeState(IdleState);
    }
}
