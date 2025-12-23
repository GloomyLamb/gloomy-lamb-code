using UnityEngine;

public class SnailShadowStateMachine : ShadowStateMachine      // StateMachine이 상태를 바꾸기위한 클래스를 만드려고하는데
{                                                               // IState 타입으로 cur 로 저장하여 ChangeState로 상태를 갈아끼우는 역할이라고 이해.
    public Transform Target { get; set; }
    public IState IdleState { get; set; }
    public IState ChaseState { get; set; }

    public SnailShadow Snail { get; private set; }
    public SnailShadowStateMachine(Shadow shadow, Animator animator) : base(shadow, animator)
    {
        Snail = shadow as SnailShadow;
        IdleState = new SnailShadowIdleState(this);
        ChaseState = new SnailShadowChaseState(this);
        ChangeState(IdleState);

    }
    
    
        
    

}
