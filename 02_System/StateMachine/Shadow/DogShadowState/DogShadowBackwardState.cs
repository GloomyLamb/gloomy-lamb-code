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
        shadow.Controller.SetActiveAgentRotation(false);
        yield return null;
        Logger.Log("회전 false 상태");
        shadow.Backward();
        yield return new WaitForSeconds(shadow.BiteBackwardDuration);
        Logger.Log("뒷걸음질 시간동안 대기 완료");
        while (shadow.Controller.Agent.pathPending
            || shadow.Controller.Agent.remainingDistance > shadow.Controller.Agent.stoppingDistance)
        {
            Logger.Log("while문 내부");
            yield return null;
        }
        shadow.Controller.SetActiveAgentRotation(true);
        Logger.Log("회전 true 상태");
        shadow.DonePattern = true;
        Logger.Log("패턴 완료");

        StateMachine.ChangeState(StateMachine.ChaseState);
    }
}
