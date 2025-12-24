using System.Collections;
using UnityEngine;

public class ShadowTransformState : CommonShadowState
{
    public ShadowTransformState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(shadow.TransformDuration);
        shadow.Transform();
    }
}
