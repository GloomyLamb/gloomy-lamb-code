public class DogShadowState : ShadowState, IState
{
    protected DogShadowStateMachine StateMachine { get; private set; }

    public DogShadowState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow,  stateMachine)
    {
        StateMachine = stateMachine as DogShadowStateMachine;
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
