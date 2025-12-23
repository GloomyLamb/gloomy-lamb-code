public class SlimeShadowAttackState : SlimeShadowBattleState
{
    public SlimeShadowAttackState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.AttackParameterHash);
    }
}
