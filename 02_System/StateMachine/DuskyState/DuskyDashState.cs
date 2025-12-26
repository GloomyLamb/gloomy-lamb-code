using UnityEngine;

public class DuskyDashState:BaseDuskyState, IMovableState
{
    public DuskyDashState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 1);
        
    }

    public override void Update()
    {
        AnimatorStateInfo state = player.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Move"))
        {
            player.Animator.speed = player.moveStatusData.MoveSpeed / 6;
        }
        else
        {
            player.Animator.speed = 1f;
        }
    }

    public override void Exit()
    {
        player.Animator.speed = 1f;
        StopAnimation(AnimatorParameters.IsMove);
    }
}