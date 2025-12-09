using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    Library,
    // todo: 미니 게임들 추가
}

/// <summary>
/// 씬 전환 관리
/// </summary>
public class SceneController
{
    #region Fields
    internal SceneController() { }

    // todo: 씬 관리 하기 고민
    // - dict으로 한 번에 정의해서 묶을 지, 씬에 저장해두고, 씬에서 넘어가면 거기서 불러올지
    private SceneType _curSceneType;
    #endregion

    /// <summary>
    /// type과 동일한 이름의 씬을 로드합니다.
    /// </summary>
    /// <param name="type"></param>
    public void LoadScene(SceneType type)
    {
        if (type == _curSceneType)
        {
            Logger.LogWarning("동일한 씬 로드");
            return;
        }

        // scene base에서 작성한 scene loaded 메서드로 사용

        AsyncOperation async = SceneManager.LoadSceneAsync(type.ToString());
        while (!async.isDone)
        {
            Logger.Log($"로딩 상태: {async.progress}%...");
        }
        _curSceneType = type;
    }

    /// <summary>
    /// 현재 씬을 리로드 합니다.
    /// </summary>
    public void ReLoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(_curSceneType.ToString());
        while (!async.isDone)
        {
            Logger.Log($"로딩 상태: {async.progress}%...");
        }
    }
}
