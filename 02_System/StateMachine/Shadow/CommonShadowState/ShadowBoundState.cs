public class ShadowBoundState : ShadowState
{
    public ShadowBoundState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    //protected override IEnumerator StateCoroutine()
    //{
    //    yield return new WaitForSeconds(shadow.BoundStopPoint);
    //    shadow.Animator.speed = 0f;
    //    yield return new WaitForSeconds(shadow.BoundDuration);
    //    shadow.Animator.speed = 1f;
    //    stateMachine.ChangeState(stateMachine.IdleState);
    //}
}
