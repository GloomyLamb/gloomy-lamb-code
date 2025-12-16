/// <summary>
/// 비디오 씬 클래스
/// </summary>
public class VideoScene : SceneBase
{
    private VideoController _videoController;

    /// <summary>
    /// 씬 전환 시 호출되는 메서드
    /// 게임 매니저에 저장되어 있는 video flow context를 참조하여 비디오를 재생하고,
    /// 비디오 재생이 종료되었을 때의 이벤트를 구독한다.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public override void Init()
    {
        base.Init();

        Logger.Log("비디오 컨트롤러 탐색");
        _videoController = FindObjectOfType<VideoController>();

        Logger.Log("비디오 컨트롤러 설정 - 종료 이벤트, 초기화");
        _videoController.OnVideoFinished += HandleVideoFinished;
        VideoFlowContext videoFlowContext = GameManager.Instance.VideoFlow;
        _videoController.Init(videoFlowContext.VideoID);
    }

    /// <summary>
    /// 비디오 재생이 종료되었을 때 호출되는 메서드
    /// video flow context에서 반환할 씬을 가져와 해당 씬으로 전환한다.
    /// </summary>
    private void HandleVideoFinished()
    {
        _videoController.OnVideoFinished -= HandleVideoFinished;

        VideoFlowContext videoFlowContext = GameManager.Instance.VideoFlow;
        SceneType next = videoFlowContext.ReturnScene;
        videoFlowContext.Clear();

        GameManager.Instance.Scene.LoadSceneWithCoroutine(next);
    }
}
