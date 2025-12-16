public class GameManager : GlobalSingletonManager<GameManager>
{
    public SceneController Scene = new();
    public DataManager Data = new();

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
