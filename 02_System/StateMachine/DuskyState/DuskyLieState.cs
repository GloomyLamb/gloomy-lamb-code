public class DuskyLieState : BaseDuskyState
{
    private readonly float _soundVolume = 0.3f;
    private readonly float _soundDelay = 0.3f;

    public DuskyLieState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Die);
        player.Animator.SetBool(AnimatorParameters.IsDead, true);
        PlayDelaySound(PlayFallDownSound, _soundDelay);
    }

    void PlayFallDownSound()
    {
        SoundManager.Instance?.PlaySfxOnce(SfxName.FallDown, _soundVolume);
    }

    public override void Update()
    {
        if (player.NowCondition.HasFlag(CharacterCondition.Stun) == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        player.Animator.SetBool(AnimatorParameters.IsDead, false);
    }
}