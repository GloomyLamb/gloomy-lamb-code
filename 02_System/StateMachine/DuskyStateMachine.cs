using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskyStateMachine : MoveableStateMachine
{
    public BaseDuskyState IdleState => _idleState;
    public BaseDuskyState JumpState => _jumpState;
    public BaseDuskyState HitState => _hitState;
    public BaseDuskyState AttackState => _attackState;
    public BaseDuskyState DieState => _dieState;
    public BaseDuskyState MoveState => _moveState;


    private BaseDuskyState _idleState;
    private BaseDuskyState _jumpState;
    private BaseDuskyState _hitState;
    private BaseDuskyState _attackState;
    private BaseDuskyState _dieState;
    private BaseDuskyState _moveState;


    public DuskyStateMachine(Animator animator) : base(animator)
    {
        _idleState = new DuskyIdleState(this);
        _jumpState = new DuskyJumpState(this);
        _hitState = new DuskyHitState(this);
        _attackState = new DuskyAttackState(this);
        _dieState = new DuskyDieState(this);
        _moveState = new DuskyMoveState(this);
    }
}