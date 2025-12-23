public class DogShadowStateMachine : StateMachine
{
    public DogShadow Shadow { get; private set; }

    // Ground
    public DogShadowChaseState ChaseState { get; private set; }
    public DogShadowIdleState IdleState { get; private set; }
    public DogShadowTransformState TransformState { get; private set; }

    // Skill
    public DogShadowBiteState BiteState { get; private set; }
    public DogShadowBarkState BarkState { get; private set; }

    // Battle
    public DogShadowHitState HitState { get; private set; }
    public DogShadowBoundState BoundState { get; private set; }

    public DogShadowStateMachine(DogShadow shadow)
    {
        Shadow = shadow;

        ChaseState = new DogShadowChaseState(this);
        IdleState = new DogShadowIdleState(this);
        TransformState = new DogShadowTransformState(this);

        BiteState = new DogShadowBiteState(this);
        BarkState = new DogShadowBarkState(this);

        HitState = new DogShadowHitState(this);
        BoundState = new DogShadowBoundState(this);

        ChangeState(IdleState);
    }
}
