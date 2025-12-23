public class SlimeShadowBattleState : SlimeShadowState
{
    public SlimeShadowBattleState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.BattleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.BattleParameterHash);
    }
}
