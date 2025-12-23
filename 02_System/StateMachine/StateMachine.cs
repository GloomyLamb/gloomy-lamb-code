public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

/// <summary>
/// 상태 머신 베이스 클래스
/// </summary>
public abstract class StateMachine
{
    public IState CurState => curState;
    protected IState curState;

    public void ChangeState(IState state)
    {
        curState?.Exit();
        curState = state;
        curState?.Enter();
    }

    public void HandleInput()
    {
        curState?.HandleInput();
    }

    public void Update()
    {
        curState?.Update();
    }

    public void PhysicsUpdate()
    {
        curState?.PhysicsUpdate();
    }

    public virtual bool CanTransition(IState nextState)
    {
        return true;
    }
}