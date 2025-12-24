public class ShadowChaseState : CommonShadowState
{
    public ShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        Init(MovementType.Default, shadow.AnimationData.ChaseParameterHash);
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
