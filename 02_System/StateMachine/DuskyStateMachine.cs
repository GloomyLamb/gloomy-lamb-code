using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskyStateMachine : MoveableStateMachine
{
    public IState IdleState => _idleState;
    public IState JumpState => _jumpState;
    public IState HitState => _hitState;
    public IState AttackState => _attackState;
    public IState DieState => _dieState;
    public IState MoveState => _moveState;


    private IState _idleState;
    private IState _jumpState;
    private IState _hitState;
    private IState _attackState;
    private IState _dieState;
    private IState _moveState;

    private Dictionary<IState, HashSet<IState>> changableStates = new Dictionary<IState, HashSet<IState>>();

    public event Action OnAttackAction;


    public DuskyStateMachine(Animator animator, DuskyPlayer player) : base(animator)
    {
        _idleState = new DuskyIdleState(this, player);
        _jumpState = new DuskyJumpState(this, player);
        _hitState = new DuskyHitState(this, player);
        _attackState = new DuskyAttackState(this, player, 0.2f, OnAttackAction);
        _dieState = new DuskyDieState(this, player);
        _moveState = new DuskyMoveState(this, player);

        changableStates = new Dictionary<IState, HashSet<IState>>
        {
            {
                _idleState, new HashSet<IState> { _moveState, _jumpState, _attackState }
            }
        };
    }

    public override bool CanChange(IState nextState)
    {
        if (changableStates.ContainsKey(curState))
        {
            return changableStates[curState].Contains(nextState);
        }
        return true;
    }
}