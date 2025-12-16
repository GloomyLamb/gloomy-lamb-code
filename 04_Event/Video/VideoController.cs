using UnityEngine;
using UnityEngine.Video;

#if UNITY_EDITOR
#endif

public class VideoController : MonoBehaviour
{
    [Header("데이터베이스")]
    [SerializeField] private VideoDatabase _videoDatabase;

    [Header("비디오")]
    [SerializeField] private VideoPlayer _videoPlayer;

    /// <summary>
    /// id에 해당하는 비디오를 재생합니다.
    /// </summary>
    /// <param name="id"></param>
    public void PlayVideoByName(VideoID id)
    {
        VideoClipEntry clipEntry = _videoDatabase.clips.Find(clip => clip.id == id);
        if (clipEntry != null)
        {
            _videoPlayer.clip = clipEntry.videoClip;
            _videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning($"{id} 비디오 데이터베이스에 없음");
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        if (_videoDatabase != null) return;
        _videoDatabase = AssetLoader.FindAndLoadByName<VideoDatabase>("VideoDatabase");
    }
#endif
}
