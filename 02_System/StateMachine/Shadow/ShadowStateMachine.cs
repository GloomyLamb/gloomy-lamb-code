using UnityEngine;

public class ShadowStateMachine : MoveableStateMachine
{
    protected Shadow shadow;

    public ShadowStateMachine(Shadow shadow, Animator animator) : base(animator)
    {
        this.shadow = shadow;
    }
}
