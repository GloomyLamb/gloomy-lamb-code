public class DogShadowStateMachine : ShadowStateMachine
{
    public DogShadow Shadow { get; private set; }

    #region States
    // Skill
    public IState BiteState { get; private set; }
    public IState BarkState { get; private set; }
    #endregion

    public DogShadowStateMachine(DogShadow shadow) : base(shadow)
    {
        Shadow = shadow;
        ChaseState = new DogShadowChaseState(shadow, this);

        BiteState = new DogShadowBiteState(this);
        BarkState = new DogShadowBarkState(this);
    }
}
