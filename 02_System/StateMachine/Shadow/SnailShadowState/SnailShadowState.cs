public class SnailShadowState : IState
{
    protected SnailShadowStateMachine StateMachine { get; private set; }

    public SnailShadowState(StateMachine stateMachine)
    {
        StateMachine = stateMachine as SnailShadowStateMachine;
    }

    #region 애니메이션 관리
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        StateMachine.Shadow.Animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        StateMachine.Shadow.Animator.SetBool(animationHash, false);
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
