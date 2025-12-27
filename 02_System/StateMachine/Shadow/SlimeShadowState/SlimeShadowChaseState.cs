using System.Collections;
using UnityEngine;

public class SlimeShadowChaseState : ShadowState
{
    protected new SlimeShadow shadow;
    protected SlimeShadowStateMachine stateMachine;

    public SlimeShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as SlimeShadow;
        this.stateMachine = stateMachine as SlimeShadowStateMachine;
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(shadow.FastChasePatternTime);
        shadow.PlusChaseCount();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
