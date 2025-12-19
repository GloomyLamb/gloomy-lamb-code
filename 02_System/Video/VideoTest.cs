using UnityEngine;

public class VideoTest : MonoBehaviour
{
    public void TestShowVideo()
    {
        GameManager.Instance.ShowVideo(VideoID.Test, SceneType.LibraryScene);
    }
}
