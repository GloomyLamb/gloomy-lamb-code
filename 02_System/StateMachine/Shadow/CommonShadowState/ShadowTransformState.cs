using System.Collections;
using UnityEngine;

public class ShadowTransformState : CommonShadowState
{
    public ShadowTransformState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        Init(MovementType.Stop, shadow.AnimationData.TransformParameterHash, AnimType.Bool, true);
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(shadow.TransformDuration);
        shadow.Transform();
    }
}
