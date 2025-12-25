using System.Collections;
using UnityEngine;

public class DogShadowBackwardState : DogShadowState
{
    public DogShadowBackwardState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    protected override IEnumerator StateCoroutine()
    {
        Logger.Log("물기 후 뒷걸음질");
        yield return new WaitForSeconds(shadow.BiteDuration);
        shadow.Backward();
        while (shadow.Controller.Agent.pathPending
            || shadow.Controller.Agent.remainingDistance > shadow.Controller.Agent.stoppingDistance)
        {
            yield return null;
        }
        shadow.Controller.SetActiveAgentRotation(true);
        shadow.DonePattern = true;

        StateMachine.ChangeState(StateMachine.ChaseState);
    }
}
