using UnityEngine.SceneManagement;

public class GameManager : GlobalSingletonManager<GameManager>
{
    // 각종 매니저들
    public SceneController Scene = new();
    public DataManager Data = new();

    public Player Player
    {
        get
        {
            DuskyPlayer dusky = _player as DuskyPlayer;
            return dusky;
        }
    }
    private Player _player;

    // 비디오
    public VideoFlowContext VideoFlow { get; private set; } = new();

    protected override void Init()
    {
        Data.Load();
    }

    #region 비디오
    /// <summary>
    /// video id의 비디오를 보여주고 returnScene 씬으로 돌아간다.
    /// </summary>
    /// <param name="videoId"></param>
    /// <param name="returnScene"></param>
    public void ShowVideo(VideoID videoId, SceneType returnScene)
    {
        VideoFlow.SetUp(videoId, returnScene);
        Scene.LoadSceneWithCoroutine(SceneType.VideoScene);
    }
    #endregion

    #region 씬 관리
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnSceneUnloaded(Scene scene)
    {
    }
    #endregion

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void OnApplicationQuit()
    {
        Data.Save();
    }
}