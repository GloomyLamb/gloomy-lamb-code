/// <summary>
/// 그림자 - 달팽이 공격 기본 상태
/// </summary>
public class SnailShadowAttackState : SnailShadowState
{
    public SnailShadowAttackState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // attack 애니메이션 시작
    }

    public override void Exit()
    {
        base.Exit();
        // attck 애니메이션 종료
    }
}
