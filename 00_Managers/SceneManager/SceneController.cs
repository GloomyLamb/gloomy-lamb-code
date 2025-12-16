using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None,
    VideoScene,

    // 테스트용
    NHP_ThreeBiomes,
    VideoTestScene,
}

/// <summary>
/// 씬 전환 관리
/// </summary>
[System.Serializable]
public class SceneController
{
    #region Fields
    internal SceneController() { }

    [SerializeField] private SceneDatabase _sceneDatabase;

    // todo: 씬 관리 하기 고민
    // - dict으로 한 번에 정의해서 묶을 지, 씬에 저장해두고, 씬에서 넘어가면 거기서 불러올지
    private SceneType _curSceneType;
    private string _externalSceneName;

    private Coroutine _coroutine;
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

        // scene base에서 작성한 scene loaded 메서드로 사용

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

        // scene base에서 작성한 scene loaded 메서드로 사용

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
    #endregion

    #region 비디오 씬 이동
    public void OnVideoSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnVideoSceneLoaded;

        _sceneDatabase.TryGetScene(SceneType.VideoScene, out GameObject prefab);
        var videoScene = GameObject.Instantiate(prefab).GetComponent<VideoScene>();
        SceneManager.sceneLoaded += videoScene.OnSceneLoaded;
    }
    #endregion
}
