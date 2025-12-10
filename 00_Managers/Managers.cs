using UnityEngine;

public class Managers : MonoBehaviour
{
    // todo: generic 매니저 들어오면 싱글톤 처리

    public SceneController Scene = new();
    public CameraManager Camera;

    #region 테스트
    // 씬
    public void TestSceneController()
    {
        Scene.TestLoadSceneAsync();
    }
    #endregion
}
