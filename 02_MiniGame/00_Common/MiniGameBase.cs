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

public abstract class MiniGameBase : MonoBehaviour
{
    #region 필드
    // 게임 상태 관리
    private readonly Dictionary<GameState, Action> _stateHandlers = new();
    private GameState _currentGameState = GameState.None;
    #endregion

    #region 초기화
    protected virtual void Awake()
    {
        InitStateHandlers();
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
    #endregion

    #region [public] 게임 상태 관리
    /// <summary>
    /// [public] 게임 상태를 변경하는 메서드
    /// </summary>
    /// <param name="gameState"></param>
    public void ChangeGameState(GameState gameState)
    {
        if (!CanTransitionTo(gameState)) return;

        _currentGameState = gameState;
        Logger.Log($"상태 변경: {_currentGameState}");
        HandleStateChanged(gameState);

        if (gameState == GameState.Start)
        {
            ResetGame();
            ChangeGameState(GameState.Play);
        }
    }
    #endregion

    #region 스테이지 상태 변경 조건 관리
    /// <summary>
    /// GameState FSM -> Figma FSM 참고
    /// </summary>
    private readonly Dictionary<GameState, GameState[]> _allowedTransitions = new()
    {
        { GameState.None,  new[] { GameState.Start } },
        { GameState.Start, new[] { GameState.Play } },
        { GameState.Play,  new[] { GameState.Pause, GameState.Clear, GameState.Dead } },
        { GameState.Pause,  new[] { GameState.Start, GameState.Resume, GameState.Exit } },
        { GameState.Resume, new[] { GameState.Play } },
        { GameState.Dead,  new[] { GameState.Start, GameState.Exit } },
        { GameState.Clear, new[] { GameState.Exit,  GameState.Start } },
        { GameState.Exit,   new[] { GameState.None } },
    };

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

    #region Abstract 함수
    protected abstract void ResetGame();
    protected abstract void HandleStart();
    protected abstract void HandlePause();
    protected virtual void HandleStart()
    {
        ResetGame();
        ChangeGameState(GameState.Play);
    }
    protected virtual void HandlePause()
    {
        // 타이머 제로로 맞추기
    }
    protected abstract void HandleResume();
    protected abstract void GameOver();
    protected abstract void HandleClear();
    protected abstract void HandleExit();
    #endregion
}
