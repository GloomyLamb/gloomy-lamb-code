public class GameManager : GlobalSingletonManager<GameManager>
{
    public SceneController Scene = new();

    #region 테스트
    // 씬
    public void TestSceneController()
    {
        Scene.LoadSceneWithCoroutine(SceneType.NHP_ThreeBiomes);
    }
    #endregion
}
