using UnityEngine;

public class DuskyDashState:BaseDuskyState, IMovableState
{
    private readonly string _animName = "Move";
    private readonly float _animationSpeedMultiplier = 0.3f;
    private readonly float _soundLoopInterval = 0.4f;
    private readonly float _soundVolume = 0.2f;
    
    public DuskyDashState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 1);
        PlayLoopSound(PlayFootStep,_soundLoopInterval,_animationSpeedMultiplier);
    }

    void PlayFootStep()
    {
        SoundManager.Instance?.PlaySfxOnce(SfxName.FootStep, _soundVolume, 1);
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