public class ShadowChaseState : CommonShadowState
{
    public ShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Default);
        base.Enter();
        shadow.Animator.SetBool(shadow.AnimationData.ChaseParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        shadow.Animator.SetBool(shadow.AnimationData.ChaseParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        if (shadow.Target == null)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        shadow.OnMove?.Invoke();
    }
}
