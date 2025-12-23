public class IdleShadowGroundState : IdleShadowState    //isGround 처리 함으로써 상속하는 친구들 모두 중복코드 제거
{
    public IdleShadowGroundState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.GroundParameterHash);
    }
}
