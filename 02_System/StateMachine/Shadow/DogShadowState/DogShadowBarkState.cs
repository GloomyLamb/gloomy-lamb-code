using System.Collections;
using UnityEngine;

public class DogShadowBarkState : DogShadowSkillState
{
    private float _timer;
    private float _patternTime = 1f;
    private int _spawnCount = 3;

    private Coroutine spawnRoutine;
    public DogShadowBarkState(StateMachine stateMachine) : base(stateMachine)
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

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _patternTime)
        {
            _timer = 0f;
            StateMachine.Shadow.DonePattern = true;
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }

    IEnumerator SpawnHowlWindRoutine()
    {
        int step = _spawnCount <= 1 ? 1 : _spawnCount - 1;
        float spawnTime = _patternTime / step;
        WaitForSeconds spawnTimeSec = new WaitForSeconds(spawnTime);

        for (int i = 0; i < step; ++i)
        {
            StateMachine.Shadow.SpawnHowlWind();
            yield return spawnTimeSec;
        }

        spawnRoutine = null;
    }
}
