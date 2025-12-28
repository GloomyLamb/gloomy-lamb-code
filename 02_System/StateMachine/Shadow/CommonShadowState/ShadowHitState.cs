public class ShadowHitState : ShadowState
{
    public ShadowHitState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    //protected override IEnumerator StateCoroutine()
    //{
    //    yield return new WaitForSeconds(shadow.HitDuration);
    //    stateMachine.ChangeState(stateMachine.IdleState);
    //}
}
