using System.Collections;
using UnityEngine;

public class DuskyHitState : BaseDuskyState, IMovableState
{
    // todo : 공통 변수들 어떻게 관리할건지
    private readonly float _soundVolume = 0.3f;
    readonly float _animationDuration = 0.2f;
    
    private Coroutine _hitStateRoutine;
    public DuskyHitState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Hit);
     
        SoundManager.Instance?.PlaySfxOnce(SfxName.Hit, _soundVolume);
        CameraController.Instance?.Impulse();
        
        if(_hitStateRoutine != null)
            CustomCoroutineRunner.Instance.StopCoroutine(_hitStateRoutine);
        _hitStateRoutine = CustomCoroutineRunner.Instance.StartCoroutine(TransitionToIdle());
    }

    public override void Update()
    {
      
    }

    public override void Exit()
    {
        if(_hitStateRoutine != null)
            CustomCoroutineRunner.Instance.StopCoroutine(_hitStateRoutine);
    }

    IEnumerator TransitionToIdle()
    {
        yield return new WaitForSeconds(_animationDuration);
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}