using System;
using System.Collections.Generic;

public class DuskyStateMachine : StateMachine
{
    public IState IdleState => _idleState;
    public IState JumpState => _jumpState;
    public IState HitState => _hitState;
    public IState AttackState => _attackState;
    public IState DieState => _dieState;
    public IState MoveState => _moveState;
    public IState DashState => _dashState;

    private IState _idleState;
    private IState _jumpState;
    private IState _hitState;
    private IState _attackState;
    private IState _dieState;
    private IState _moveState;
    private IState _dashState;

    

    private Dictionary<IState, HashSet<IState>> changableStates = new Dictionary<IState, HashSet<IState>>();

    public event Action OnAttackAction;

    public DuskyStateMachine(DuskyPlayer player)
    {
        _idleState = new DuskyIdleState(this, player);
        _moveState = new DuskyMoveState(this, player);
        _jumpState = new DuskyJumpState(this, player);
        _attackState = new DuskyAttackState(this, player, 0.2f);
        _hitState = new DuskyHitState(this, player);
        _dieState = new DuskyDieState(this, player);
        _dashState = new DuskyDashState(this, player);



        changableStates = new Dictionary<IState, HashSet<IState>>
        {
            {
                _idleState, 
                new HashSet<IState> { _moveState, _jumpState, _attackState, _hitState, _dieState }
            },
            {
                _moveState,
                new HashSet<IState> { _idleState, _jumpState, _attackState, _hitState, _dieState }
            },
            {
                _jumpState,
                new HashSet<IState> { _idleState, _attackState, _hitState, _dieState }
            },
            {
                _attackState,
                new HashSet<IState> { _idleState,  _hitState, _dieState }
            },
            {
                _hitState,
                new HashSet<IState> { _idleState, _dieState }
            },
            {
                _dieState,
                new HashSet<IState> { _idleState }  // 부활같은거 하면 일어나야해
            },
            
        };
    }

    public override bool CanChange(IState nextState)
    {
        if (curState == null) return false;
        
        if (changableStates.ContainsKey(curState))
        {
            return changableStates[curState].Contains(nextState);
        }

        return true;
    }
    
    public void ChangeState(IState state)
    {
        base.ChangeState(state);
        // curState?.Exit();    //이전상태 Exit 호출 함으로써 idle 종료처리 
        // curState = state;    //현재상태 교체 Idle -> Chase 
        // curState?.Enter();   //Chase 상태 Enter 호출 함으로써 Chase 시작처리
    }
}