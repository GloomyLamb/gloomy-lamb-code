public class DogShadowTransformState : DogShadowGroundState
{
    public DogShadowTransformState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.TransformParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.TransformParameterHash);
    }
}
