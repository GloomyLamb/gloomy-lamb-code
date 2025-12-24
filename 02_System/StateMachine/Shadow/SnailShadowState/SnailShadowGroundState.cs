/// <summary>
/// 그림자 - 달팽이 기본 지면 상태
/// </summary>
public class SnailShadowGroundState : SnailShadowState
{
    public SnailShadowGroundState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.GroundParameterHash);
    }
}
