using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 더스키 상태 베이스 클래스
/// </summary>
public abstract class BaseDuskyState : IState
{
    protected DuskyPlayer player;
    protected DuskyStateMachine stateMachine;

    public BaseDuskyState(StateMachine stateMachine, DuskyPlayer player)
    {
        this.stateMachine = stateMachine as DuskyStateMachine;
        this.player = player;
    }

    #region 애니메이션 관리
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        player.Animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        player.Animator.SetBool(animationHash, false);
    }
    #endregion



    #region 애니메이션 사운드 타이밍

    
    private Coroutine _soundLoopRoutine = null;
    private Coroutine _soundDelayRoutine = null;

    protected void PlayLoopSound(Action playAction, float interval, float animationSpeedMultiplier, bool immediateStart = false)
    {
        StopLoopSound();
        _soundLoopRoutine = CustomCoroutineRunner.Instance.StartCoroutine(
            LoopSoundRoutine(playAction, interval, animationSpeedMultiplier, immediateStart));
    }

    protected void PlayDelaySound(SfxName sfxName, float volume, float delay)
    {
        StopDelaySound();
        _soundDelayRoutine = CustomCoroutineRunner.Instance.StartCoroutine(
            PlaySoundOnceDelayRoutine(sfxName, volume, delay));
    }

    protected void StopLoopSound()
    {
        if (_soundLoopRoutine != null)
            CustomCoroutineRunner.Instance.StopCoroutine(_soundLoopRoutine);
        _soundLoopRoutine = null;
    }

    protected void StopDelaySound()
    {
        if (_soundDelayRoutine != null)
            CustomCoroutineRunner.Instance.StopCoroutine(_soundDelayRoutine);
    }
    
    protected IEnumerator LoopSoundRoutine(Action playAction, float interval, float animationSpeedMultiplier, bool immediateStart = false)
    {
        WaitForSeconds wait = new WaitForSeconds(interval/ (player.moveStatusData.MoveSpeed * animationSpeedMultiplier));
        while (true)
        {
            yield return wait;
            playAction?.Invoke();
            //SoundManager.Instance?.PlaySfxRandom(sfxName, volume);
        }
    }

    protected IEnumerator PlaySoundOnceDelayRoutine(SfxName sfxName, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        SoundManager.Instance?.PlaySfxOnce(sfxName, volume);
    }

    #endregion
   

    #region IState 구현

    public abstract void Enter();

    public abstract void Exit();

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }

    #endregion
}