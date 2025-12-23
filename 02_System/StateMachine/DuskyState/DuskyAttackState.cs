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

    public DuskyAttackState(StateMachine stateMachine, DuskyPlayer player,
    float attackAnimDelay, Action attackAction) : base(stateMachine, player)
    {
        _attackAnimTiming = attackAnimDelay;
        _attackAction = attackAction;

    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Attack);
        _attackRotuine = CoroutineRunner.instance.StartCoroutine(AttackRoutine());
    }

    public override void Update()
    {
        AnimatorStateInfo animInfo = player.Animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName(AnimatorParameters.AttackName) && animInfo.normalizedTime >= 1f)
        {
            DuskyStateMachine duskySm = (DuskyStateMachine)stateMachine;
            if (duskySm != null)
            {
                stateMachine.ChangeState(duskySm.IdleState);
            }
        }
    }

    public override void Exit()
    {
        CoroutineRunner.instance.StopCoroutine(_attackRotuine);
        base.Exit();
    }


    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackAnimTiming);
        _attackAction?.Invoke();
    }
}