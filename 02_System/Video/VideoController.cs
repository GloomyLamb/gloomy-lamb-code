using System;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 비디오 재생 & 제어 클래스
/// todo: manager로 변경 고려
/// </summary>
public class VideoController : MonoBehaviour
{
    [Header("데이터베이스")]
    [SerializeField] private VideoDatabase _videoDatabase;

    [Header("비디오 재생 관리")]
    [SerializeField] private VideoPlayer _videoPlayer;

    // 이벤트
    public event Action OnVideoFinished;

    // 캐싱
    private VideoID _currentVideoID;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopVideo();
        }
    }

    public void Init(VideoID videoId)
    {
        Logger.Log("비디오 정보 초기화");
        _currentVideoID = videoId;

        _videoPlayer.loopPointReached -= HandleVideoFinished;
        _videoPlayer.loopPointReached += HandleVideoFinished;

        PlayVideo(_currentVideoID);
    }

    /// <summary>
    /// id에 해당하는 비디오를 재생합니다.
    /// </summary>
    /// <param name="id"></param>
    public void PlayVideo(VideoID id)
    {
        if (id == VideoID.Intro)
        {
            GameManager.Instance.Data.WatchIntroVideo();
        }

        if (!_videoDatabase.TryGetClip(id, out VideoClip clip))
        {
            Debug.LogWarning($"{id} 비디오 데이터베이스에 없음");
            return;
        }

        _videoPlayer.clip = clip;
        _videoPlayer.Play();
    }

    /// <summary>
    /// 현재 재생 중인 비디오를 중지합니다.
    /// </summary>
    public void StopVideo()
    {
        _videoPlayer.Stop();
        HandleVideoFinished(_videoPlayer);
    }

    /// <summary>
    /// 비디오 재생이 끝났을 때 호출됩니다.
    /// </summary>
    /// <param name="videoPlayer"></param>
    private void HandleVideoFinished(VideoPlayer videoPlayer)
    {
        _videoPlayer.loopPointReached -= HandleVideoFinished;
        _videoPlayer.clip = null;
        OnVideoFinished?.Invoke();
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        if (_videoDatabase != null) return;
        _videoDatabase = AssetLoader.FindAndLoadByName<VideoDatabase>("VideoDatabase");

        if (_videoPlayer != null) return;
        _videoPlayer = FindObjectOfType<VideoPlayer>();
        if (_videoPlayer == null)
        {
            GameObject obj = Instantiate(AssetLoader.FindAndLoadByName("VideoPlayer"));
            _videoPlayer = obj.GetComponent<VideoPlayer>();
        }
    }
#endif
    #endregion
}
