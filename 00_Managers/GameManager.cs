public class GameManager : GlobalSingletonManager<GameManager>
{
    // 각종 매니저들
    public SceneController Scene = new();
    public DataManager Data = new();

    // 비디오
    public VideoFlowContext VideoFlow { get; private set; } = new();

    protected override void Init()
    {

    }

    #region 테스트
    // 씬
    public void TestSceneController()
    {
        Scene.LoadSceneWithCoroutine(SceneType.NHP_ThreeBiomes);
    }
    #endregion
}
