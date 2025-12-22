/// <summary>
/// 그림자 - 달팽이 기본 움직임
/// </summary>
public class SnailShadowIdleState : SnailShadowGroundState
{
    public SnailShadowIdleState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // idle 움직임 애니메이션 시작
    }

    public override void Exit()
    {
        base.Exit();
        // idle 움직임 애니메이션 종료
    }
}
