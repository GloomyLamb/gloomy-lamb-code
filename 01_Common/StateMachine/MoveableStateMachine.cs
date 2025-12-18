using UnityEngine;

/// <summary>
/// 애니메이션 관련 상태머신 
/// </summary>
public abstract class MoveableStateMachine : StateMachine
{
    public Animator animator;

    public MoveableStateMachine(Animator animator)
    {
        this.animator = animator;
    }
}
