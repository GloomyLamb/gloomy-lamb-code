using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Start,
    Play,
    Pause,
    Resume,
    Dead,
    Clear,
    Exit,
}

public abstract class BaseMiniGame : MonoBehaviour
{
    #region 필드
    // 게임 상태 관리
    private readonly Dictionary<GameState, Action> _stateHandlers = new();
    private Dictionary<GameState, GameState[]> _allowedTransitions;

    private GameState _currentGameState = GameState.None;
    #endregion

    #region 초기화
    protected virtual void Awake()
    {
        InitStateHandlers();
        _allowedTransitions = DefineTransitions();
    }

    protected virtual void Start()
    {

    }

    private void InitStateHandlers()
    {
        _stateHandlers.Clear();
        _stateHandlers[GameState.Start] = HandleStart;
        _stateHandlers[GameState.Play] = HandlePlay;
        _stateHandlers[GameState.Pause] = HandlePause;
        _stateHandlers[GameState.Resume] = HandleResume;
        _stateHandlers[GameState.Dead] = GameOver;
        _stateHandlers[GameState.Clear] = HandleClear;
        _stateHandlers[GameState.Exit] = HandleExit;
    }

    /// <summary>
    /// FSM 상태 전이 정의 (필요 시 override 가능)
    /// </summary>
    protected virtual Dictionary<GameState, GameState[]> DefineTransitions()
    {
        return new Dictionary<GameState, GameState[]>
        {
            { GameState.None,  new[] { GameState.Start } },
            { GameState.Start, new[] { GameState.Play } },
            { GameState.Play,  new[] { GameState.Pause, GameState.Dead, GameState.Clear } },
            { GameState.Pause, new[] { GameState.Resume, GameState.Start, GameState.Exit } },
            { GameState.Resume, new[] { GameState.Play } },
            { GameState.Dead, new[] { GameState.Start, GameState.Exit } },
            { GameState.Clear, new[] { GameState.Start, GameState.Exit } },
            { GameState.Exit, new[] { GameState.None } },
        };
    }
    #endregion

    #region 게임 상태 변경
    /// <summary>
    /// [public] 게임 상태를 변경하는 메서드
    /// </summary>
    /// <param name="nextState"></param>
    /// <returns></returns>
    public bool TryChangeGameState(GameState nextState)
    {
        if (!CanTransitionTo(nextState)) return false;

        _currentGameState = nextState;
        Logger.Log($"상태 변경: {_currentGameState}");

        HandleStateChanged(nextState);
        return true;
    }

    /// <summary>
    /// 변경 가능한 상태인지 검증
    /// </summary>
    /// <param name="gameState"></param>
    /// <returns></returns>
    private bool CanTransitionTo(GameState gameState)
    {
        // 동일한 상태일 경우 스킵
        if (_currentGameState == gameState) return false;

        // FSM 유효한지 확인
        if (!_allowedTransitions.TryGetValue(_currentGameState, out var allowedStates) ||
            Array.IndexOf(allowedStates, gameState) == -1)
        {
            Logger.Log($"상태 변경 불가: {_currentGameState} → {gameState}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 게임 상태에 따라 다른 로직을 처리하기 위한 메서드
    /// 유효성 검사를 위해 ChangeGameState 사용을 권장
    /// </summary>
    /// <param name="state"></param>
    private void HandleStateChanged(GameState state)
    {
        if (_stateHandlers.TryGetValue(state, out var handler))
        {
            handler?.Invoke();
        }
        else
        {
            Logger.Log($"{state} 할당 함수 없음");
        }
    }
    #endregion

    #region 미니 게임 내부 동작 로직
    /// <summary>
    /// 미니 게임에서 사용할 데이터를 초기화할 때 사용
    /// </summary>
    protected abstract void ResetGame();

    /// <summary>
    /// 게임 시작 시, 초기화하고 플레이로 진입
    /// </summary>
    protected virtual void HandleStart()
    {
        ResetGame();
        TryChangeGameState(GameState.Play);
    }

    /// <summary>
    /// 플레이
    /// </summary>
    protected abstract void HandlePlay();

    /// <summary>
    /// 일시 정지 상태
    /// </summary>
    protected abstract void HandlePause();

    /// <summary>
    /// 재시작
    /// </summary>
    protected abstract void HandleResume();

    /// <summary>
    /// 게임 오버
    /// </summary>
    protected abstract void GameOver();

    /// <summary>
    /// 클리어
    /// </summary>
    protected abstract void HandleClear();

    /// <summary>
    /// 게임 나가기
    /// </summary>
    protected abstract void HandleExit();
    #endregion
}
