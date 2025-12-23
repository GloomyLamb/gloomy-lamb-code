public class SlimeShadowTransformState : SlimeShadowGroundState
{
    public SlimeShadowTransformState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.MovementSpeedModitier = 0f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // todo: hp 확인
        // 50% 이상 -> 사냥개
        // 이하 -> 사냥개 / 달팽이
    }
}
