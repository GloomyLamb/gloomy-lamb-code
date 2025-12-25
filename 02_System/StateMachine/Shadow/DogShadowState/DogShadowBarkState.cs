using System.Collections;
using UnityEngine;

public class DogShadowBarkState : DogShadowSkillState
{
    private float _timer;
    
    //private float _patternTime = 1f;
    private float _spawnTime = 1f;
    private int _spawnCount = 3;

    private Coroutine spawnRoutine;
    public DogShadowBarkState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow,  stateMachine)
    {
    }

    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.SkillAnimationData.BarkParameterHash);
        Logger.Log("짖기");
        
        // todo: 짖기 스킬 연결
        
        
        if(spawnRoutine != null) CustomCoroutineRunner.Instance.StopCoroutine(spawnRoutine);
        spawnRoutine = CustomCoroutineRunner.Instance.StartCoroutine(SpawnHowlWindRoutine());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.SkillAnimationData.BarkParameterHash);
        
        if(spawnRoutine != null) CustomCoroutineRunner.Instance.StopCoroutine(spawnRoutine);
    }


    IEnumerator SpawnHowlWindRoutine()
    {
        WaitForSeconds spawnTimeSec = new WaitForSeconds(_spawnTime);
        
        for (int i = 0; i < _spawnCount; ++i)
        {
            StateMachine.Shadow.SpawnHowlWind();
            yield return spawnTimeSec;
        }
        
        spawnRoutine = null;
        
        StateMachine.Shadow.DonePattern = true;
        StateMachine.ChangeState(StateMachine.IdleState);
    }
}
