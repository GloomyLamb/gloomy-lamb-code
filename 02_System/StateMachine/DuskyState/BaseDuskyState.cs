/// <summary>
/// 더스키 상태 베이스 클래스
/// </summary>
public abstract class BaseDuskyState : IState
{
    protected DuskyPlayer player;
    protected DuskyStateMachine stateMachine;

    public BaseDuskyState(StateMachine stateMachine, DuskyPlayer player)
    {
        this.stateMachine = stateMachine as DuskyStateMachine;
        this.player = player;
    }

    #region 애니메이션 관리
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        player.Animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        player.Animator.SetBool(animationHash, false);
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
