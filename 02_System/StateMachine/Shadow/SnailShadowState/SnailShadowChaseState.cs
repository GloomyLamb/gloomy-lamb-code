public class SnailShadowChaseState : ShadowChaseState
{
    private readonly SnailShadowStateMachine snailSM;

    public SnailShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        snailSM = stateMachine as SnailShadowStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        snailSM.Shadow?.StartSlime();
    }

    public override void Exit()
    {
        base.Exit();
        snailSM.Shadow.StopSlime();
    }
}
