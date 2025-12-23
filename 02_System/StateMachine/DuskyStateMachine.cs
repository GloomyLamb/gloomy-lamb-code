using System;
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

    public event Action OnAttackAction;


    public DuskyStateMachine(Animator animator, DuskyPlayer player) : base(animator)
    {
        _idleState = new DuskyIdleState(this, player);
        _jumpState = new DuskyJumpState(this, player);
        _hitState = new DuskyHitState(this,player);
        _attackState = new DuskyAttackState(this, player, 0.2f, OnAttackAction);
        _dieState = new DuskyDieState(this, player);
        _moveState = new DuskyMoveState(this, player);
    }
}