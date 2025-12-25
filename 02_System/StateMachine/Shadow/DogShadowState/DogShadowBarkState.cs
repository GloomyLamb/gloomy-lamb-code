using System.Collections;
using UnityEngine;

public class DogShadowBarkState : DogShadowSkillState
{
    //private float _patternTime = 1f;
    private float _spawnTime = 1f;
    private int _spawnCount = 3;

    public DogShadowBarkState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        Logger.Log("짖기");
        base.Enter();
    }

    protected override IEnumerator StateCoroutine()
    {
        WaitForSeconds spawnTimeSec = new WaitForSeconds(_spawnTime);

        // 멈출 때까지 딜레이 주기
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _spawnCount; ++i)
        {
            StateMachine.Shadow.SpawnHowlWind();
            yield return spawnTimeSec;
        }

        StateMachine.Shadow.DonePattern = true;
        StateMachine.ChangeState(StateMachine.IdleState);
    }
}
