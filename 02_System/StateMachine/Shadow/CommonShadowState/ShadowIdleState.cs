public class ShadowIdleState : CommonShadowState
{
    public ShadowIdleState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        Init(MovementType.Stop, shadow.AnimationData.IdleParameterHash);
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
