/// <summary>
/// 그림자 - 달팽이 기본 지면 상태
/// </summary>
public class SnailShadowGroundState : SnailShadowState
{
    public SnailShadowGroundState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ground idle 애니메이션 시작
    }

    public override void Exit()
    {
        base.Exit();
        // groud idle 애니메이션 종료
    }
}
