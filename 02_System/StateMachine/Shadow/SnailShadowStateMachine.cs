public class SnailShadowStateMachine : ShadowStateMachine      // StateMachine이 상태를 바꾸기위한 클래스를 만드려고하는데
{                                                               // IState 타입으로 cur 로 저장하여 ChangeState로 상태를 갈아끼우는 역할이라고 이해.
    public SnailShadow Shadow { get; private set; }

    public SnailShadowStateMachine(SnailShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        ChaseState = new SnailShadowChaseState(shadow, this);
    }
}
