public class VideoFlowContext
{
    public VideoID VideoID { get; set; }
    public SceneType ReturnScene { get; set; }

    public void SetUp(VideoID videoId, SceneType returnScene)
    {
        VideoID = videoId;
        ReturnScene = returnScene;
    }

    public void Clear()
    {
        VideoID = default;
        ReturnScene = SceneType.None;
    }
}
