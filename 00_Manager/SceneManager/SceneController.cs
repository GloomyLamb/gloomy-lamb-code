using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None,
    VideoScene,
    LibraryScene,
    ShadowForestScene,
}

/// <summary>
/// 씬 전환 관리
/// </summary>
[System.Serializable]
public class SceneController
{
    #region Fields

    [SerializeField] private SceneDatabase _sceneDatabase;

    private Dictionary<SceneType, Type> _baseSceneTypeDict;

    private BaseScene _curScene;
    private SceneType _curSceneType;
    private string _externalSceneName;

    private Coroutine _coroutine;
    #endregion

    #region 초기화
    public SceneController()
    {
        InitSceneTypeDict();
    }

    /// <summary>
    /// BaseScene의 Type을 캐싱해놓을 딕셔너리
    /// BaseScene에 Init이 있는 경우에만 추가하면 됩니다.
    /// </summary>
    private void InitSceneTypeDict()
    {
        _baseSceneTypeDict = new()
        {
            { SceneType.VideoScene, typeof(VideoScene) },
            { SceneType.LibraryScene, typeof(LibraryScene) },
        };
    }
    #endregion

    #region 씬 로드
    /// <summary>
    /// type과 동일한 이름의 씬을 비동기로 로드합니다.
    /// </summary>
    /// <param name="type"></param>
    public void LoadSceneWithCoroutine(SceneType type)
    {
        if (type == _curSceneType)
        {
            Logger.LogWarning("동일한 씬 로드");
            return;
        }
        _curSceneType = type;
        _externalSceneName = null;

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (_coroutine != null)
        {
            CoroutineRunner.instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = CoroutineRunner.instance.StartCoroutine(LoadSceneAsync());
    }

    /// <summary>
    /// sceneName과 동일한 이름의 씬을 비동기로 로드합니다.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneWithCoroutine(string sceneName)
    {
        if (sceneName == _curSceneType.ToString())
        {
            Logger.LogWarning("동일한 씬 로드");
            return;
        }
        _curSceneType = SceneType.None;
        _externalSceneName = sceneName;

        if (_coroutine != null)
        {
            CoroutineRunner.instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = CoroutineRunner.instance.StartCoroutine(LoadSceneAsync());
    }

    /// <summary>
    /// 현재 씬을 리로드 합니다.
    /// </summary>
    public void ReLoadScene()
    {
        SceneManager.LoadScene(_curSceneType.ToString());
    }

    /// <summary>
    /// 코루틴 비동기로 씬을 로딩합니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync()
    {
        // todo: 로딩 씬 필요
        string sceneName = _curSceneType == SceneType.None ? _externalSceneName : _curSceneType.ToString();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
            Logger.Log($"로딩 상태: {async.progress * 100}%...");
        }
    }

    /// <summary>
    /// 씬 로드가 완료된 직후 실행됩니다.
    /// 씬에 BaseScene이 필요한 경우 초기화합니다.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (!_baseSceneTypeDict.TryGetValue(_curSceneType, out Type type))
        {
            Logger.Log("base scene type 없음");
            _curScene = null;
            return;
        }

        if (!_sceneDatabase.TryGetScene(_curSceneType, out GameObject prefab))
        {
            Logger.Log("scene database에 scene data 없음");
            _curScene = null;
            return;
        }

        Logger.Log("scene prefab 생성");
        var sceneObj = (BaseScene)GameObject.Instantiate(prefab).GetComponent(type);

        sceneObj.Init();
        _curScene = sceneObj;
    }
    #endregion
}
