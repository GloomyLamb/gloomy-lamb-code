using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoTest : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.VideoFlow.SetUp(VideoID.Test, SceneType.VideoTest);
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.Instance.Scene.LoadSceneWithCoroutine(SceneType.Video);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
