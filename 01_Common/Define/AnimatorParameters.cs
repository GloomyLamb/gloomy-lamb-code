using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorParameters
{
    /// <summary>
    /// 움직일 때 true
    /// </summary>
    public static readonly int IsMove = Animator.StringToHash("IsMove");
    
    /// <summary>
    /// Attack 트리거
    /// </summary>
    public static readonly int Attack = Animator.StringToHash("Attack");
    
    /// <summary>
    /// Hit 트리거
    /// </summary>
    public static readonly int Hit = Animator.StringToHash("Hit");
    
    /// <summary>
    /// Die 트리거
    /// </summary>
    public static readonly int Die = Animator.StringToHash("Die");
    
    /// <summary>
    /// 죽은 상태일때 true
    /// ex) DieIdle 상태에서 벗어날 때 false 처리
    /// </summary>
    public static readonly int IsDead = Animator.StringToHash("IsDead");
    
    /// <summary>
    /// Jump 트리거
    /// </summary>
    public static readonly int Jump = Animator.StringToHash("Jump");
    
    /// <summary>
    /// 지면 위에 있지 않은 상태일때 true
    /// </summary>
    public static readonly int IsFalling = Animator.StringToHash("IsFalling");


    // todo : enum으로 바꿔야함
    public static readonly string AttackName = "Attack";
}