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
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        stateMachine.animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        stateMachine.animator.SetBool(animationHash, false);
    }
    #endregion

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
