public class SlimeShadowExpandState : SlimeShadowState
{
    public SlimeShadowExpandState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    //protected override IEnumerator StateCoroutine()
    //{
    //    Transform target = shadow.transform;
    //    Vector3 startScale = target.localScale;
    //    Vector3 endScale = startScale * SlimeShadow.MaxScale;
    //    float elapsed = 0f;

    //    while (elapsed < SlimeShadow.ScaleUpDuration)
    //    {
    //        target.localScale = Vector3.Lerp(startScale, endScale, elapsed / SlimeShadow.ScaleUpDuration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    target.localScale = endScale;
    //    SlimeShadow.CheckExpand();
    //    stateMachine.ChangeState(stateMachine.ChaseState);
    //}
}
