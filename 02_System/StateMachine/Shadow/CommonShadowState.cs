public class CommonShadowState : IState
{
    protected Shadow shadow;
    protected StateMachine StateMachine { get; private set; }

    public CommonShadowState(Shadow shadow, StateMachine stateMachine)
    {
        this.shadow = shadow;
        StateMachine = stateMachine;
    }

    #region IState 구현
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }
    #endregion
}
