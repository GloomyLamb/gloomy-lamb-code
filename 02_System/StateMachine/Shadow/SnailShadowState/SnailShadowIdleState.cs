/// <summary>
/// 그림자 - 달팽이 기본 움직임
/// </summary>
public class SnailShadowIdleState : SnailShadowGroundState        // 목표 감지 로직 포함
{
    public SnailShadowIdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        StartAnimation(StateMachine.Shadow.CommonAnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        if (StateMachine.Shadow.Target == null)
            return;
        StateMachine.ChangeState(StateMachine.ChaseState); // ChaseState로 전환시도 
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.CommonAnimationData.IdleParameterHash);
    }
}
