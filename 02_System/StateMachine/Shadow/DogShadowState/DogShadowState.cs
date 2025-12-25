public class DogShadowState : ShadowState
{
    protected new DogShadow shadow;
    protected new DogShadowStateMachine StateMachine { get; private set; }

    public DogShadowState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as DogShadow;
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
}
