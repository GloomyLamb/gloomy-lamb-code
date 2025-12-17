public class VideoFlowContext
{
    public VideoID VideoID { get; set; }
    public SceneType ReturnScene { get; set; }

    public void SetUp(VideoID videoId, SceneType returnScene)
    {
        VideoID = videoId;
        ReturnScene = returnScene;
        Logger.Log($"{VideoID} 영상 시청 후 {ReturnScene}으로 이동 설정");
    }

    public void Clear()
    {
        VideoID = default;
        ReturnScene = SceneType.None;
    }
}
