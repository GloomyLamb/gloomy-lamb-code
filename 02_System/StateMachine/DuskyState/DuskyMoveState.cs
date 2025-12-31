using System.Collections;
using UnityEngine;

public class DuskyMoveState : BaseDuskyState, IMovableState
{
    private readonly string _animName = "Move";
    private readonly float _animationSpeedMultiplier = 0.5f;
    private readonly float _soundLoopInterval = 0.6f;
    private readonly float _soundVolume = 0.2f;

    public DuskyMoveState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }


    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 0);

        PlayLoopSound(PlayFootSteop, _soundLoopInterval, _animationSpeedMultiplier);
    }

    void PlayFootSteop()
    {
        SoundManager.Instance?.PlaySfxRandom(SfxName.FootStep, _soundVolume);
    }

    public override void Update()
    {
        AnimatorStateInfo state = player.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName(_animName))
        {
            player.Animator.speed = player.moveStatusData.MoveSpeed * _animationSpeedMultiplier;
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

        StopLoopSound();
    }
}