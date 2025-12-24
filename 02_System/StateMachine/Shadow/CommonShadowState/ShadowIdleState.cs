public class ShadowIdleState : CommonShadowState
{
    public ShadowIdleState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        Logger.Log("Enter Idle State");
        shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        shadow.Animator.SetBool(shadow.AnimationData.IdleParameterHash, true);
    }

    public override void Exit()
    {
        Logger.Log("Exit Idle State");
        base.Exit();
        shadow.Animator.SetBool(shadow.AnimationData.IdleParameterHash, false);
    }

    public override void Update()
    {
        base.Update();

        if (shadow.Target != null)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
        }
    }
}
