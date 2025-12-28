public class SlimeShadowState : ShadowState
{
    protected SlimeShadow SlimeShadow => (SlimeShadow)shadow;
    protected SlimeShadowStateMachine SlimeStateMachine => (SlimeShadowStateMachine)StateMachine;

    public SlimeShadowState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }
}
