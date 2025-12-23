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
        curState?.Exit();    //이전상태 Exit 호출 함으로써 idle 종료처리 
        curState = state;    //현재상태 교체 Idle -> Chase 
        curState?.Enter();   //Chase 상태 Enter 호출 함으로써 Chase 시작처리
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
}