using UnityEngine;

public class ShadowStateMachine : MoveableStateMachine
{
    public Shadow Shadow { get; private set; }

    public ShadowStateMachine(Shadow shadow, Animator animator) : base(animator)
    {
        Shadow = shadow;
    }
    public virtual void Init() 
    { 

    }
}
