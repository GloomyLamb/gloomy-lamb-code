using UnityEngine;

public class VideoTest : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ShowVideo(VideoID.Test, SceneType.VideoTestScene);
    }
}
