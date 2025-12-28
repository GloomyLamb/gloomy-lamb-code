public class ShadowTransformState : ShadowState
{
    public ShadowTransformState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    //protected override IEnumerator StateCoroutine()
    //{
    //    yield return new WaitForSeconds(shadow.TransformDuration);
    //    shadow.Transform();
    //}
}
