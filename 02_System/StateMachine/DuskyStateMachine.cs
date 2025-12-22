using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskyStateMachine : MoveableStateMachine
{
    private Animator _anim;

    private BaseDuskyState _idleState;
    private BaseDuskyState _jumpState;
    private BaseDuskyState _hitState;
    private BaseDuskyState _attackState;
    
    public DuskyStateMachine(Animator animator) : base(animator)
    {
        _anim = animator;
    }

}
