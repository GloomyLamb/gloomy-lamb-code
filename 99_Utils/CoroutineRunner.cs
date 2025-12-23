using UnityEngine;

/// <summary>
/// 코루틴 러너
/// 씬 전환 시 파괴 허용
/// </summary>
public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("CoroutineRunner");
                var coroutineRunner = obj.AddComponent<CoroutineRunner>();
                _instance = coroutineRunner;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
}
