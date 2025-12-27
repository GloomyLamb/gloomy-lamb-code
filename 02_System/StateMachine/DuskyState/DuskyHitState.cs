public class DuskyHitState : BaseDuskyState, IMovableState
{
    // todo : 공통 변수들 어떻게 관리할건지
    private readonly float _soundVolume = 0.3f;


    public DuskyHitState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Hit);
        SoundManager.Instance?.PlaySfxOnce(SfxName.Hit, _soundVolume);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
    }
}