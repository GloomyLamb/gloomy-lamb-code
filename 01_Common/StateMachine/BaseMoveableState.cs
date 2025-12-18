/// <summary>
/// 애니메이션(움직임)이 들어가는 상태 베이스 클래스
/// </summary>
public abstract class BaseMoveableState : IState
{
    protected MoveableStateMachine stateMachine;

    protected BaseMoveableState(MoveableStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    #region 애니메이션 관리
    protected void StartAnimation(int animationHash)
    {
        stateMachine.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.animator.SetBool(animationHash, false);
    }
    #endregion

    #region IState 구현
    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void PhysicsUpdate()
    {
    }

    public void Update()
    {
    }
    #endregion
}
