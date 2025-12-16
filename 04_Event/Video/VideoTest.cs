using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoTest : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.Instance.Scene.LoadSceneWithCoroutine("VideoScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
