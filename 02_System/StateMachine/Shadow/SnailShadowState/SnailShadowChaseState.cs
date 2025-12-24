public class SnailShadowChaseState : SnailShadowGroundState
{
    private readonly SnailShadowStateMachine snailSM;

    public SnailShadowChaseState(StateMachine stateMachine) : base(stateMachine)
    {
        snailSM = (SnailShadowStateMachine)stateMachine; // 상태 머신 캐스팅
    }

    public override void Enter()
    {
        base.Enter();
        StateMachine.Shadow.MovementSpeedModitier = 1f;
        StartAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
        snailSM.Shadow?.StartSlime();
    }

    public override void Update()
    {
        base.Update();

        StateMachine.Shadow.OnMove?.Invoke();

        //  타겟이 사라졌으면 Idle로 복귀
        if (StateMachine.Shadow.Target == null)
        {
            snailSM.ChangeState(snailSM.IdleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
        snailSM.Shadow.StopSlime();
    }
}
