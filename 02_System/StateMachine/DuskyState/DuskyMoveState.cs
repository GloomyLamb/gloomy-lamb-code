using System.Collections;
using UnityEngine;

public class DuskyMoveState : BaseDuskyState, IMovableState
{
    private string _moveName = "Move";
    private Coroutine _walkSoundRoutine = null;

    public DuskyMoveState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }


    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 0);
        
        StopCoroutine(_walkSoundRoutine);
        _walkSoundRoutine = StartCoroutine(WalkSoundRoutine());
    }

    public override void Update()
    {
        AnimatorStateInfo state = player.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName(_moveName))
        {
            player.Animator.speed = player.moveStatusData.MoveSpeed / 2;
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

        StopCoroutine(_walkSoundRoutine);
    }


    IEnumerator WalkSoundRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            SoundManager.Instance.PlaySfxRandom(SfxName.FootStep, 0.2f);
        }
    }
}