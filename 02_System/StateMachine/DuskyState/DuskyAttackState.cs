// todo : 공격하면서 이동할 수 있는지? 공격 모션이 나올 때 이동 가능한지?

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DuskyAttackState : BaseDuskyState
{
    private float _attackAnimTiming;
    private Action _attackAction;
    private Coroutine _attackRotuine;
    
    private readonly float _soundVolume = 0.3f;

    public DuskyAttackState(StateMachine stateMachine, DuskyPlayer player,
    float attackAnimDelay) : base(stateMachine, player)
    {
        _attackAnimTiming = attackAnimDelay;

    }

    public override void Enter()
    {
        // todo : 후에 동작 플래그를 따로 만들어서 Enter 가 두 번 들어오는거 자체를 막기
        AnimatorStateInfo animInfo = player.Animator.GetCurrentAnimatorStateInfo(0);
        if(animInfo.IsName(AnimatorParameters.AttackName) == false)
            player.Animator.SetTrigger(AnimatorParameters.Attack);
        _attackRotuine = CoroutineRunner.instance.StartCoroutine(AttackRoutine());
        
        SoundManager.Instance?.PlaySfxOnce(SfxName.Attack, _soundVolume);
    }

    public override void Update()
    {
        AnimatorStateInfo animInfo = player.Animator.GetCurrentAnimatorStateInfo(0);

        //Debug.Log(animInfo.normalizedTime);
        if (animInfo.IsName(AnimatorParameters.AttackName) && animInfo.normalizedTime >= 1f)
        {
            if (stateMachine != null)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        CustomCoroutineRunner.Instance.StopCoroutine(_attackRotuine);
    }


    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackAnimTiming);
        player.Attack();
    }
}