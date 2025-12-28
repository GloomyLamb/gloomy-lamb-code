using System.Collections;
/// <summary>
/// 그림자 - 달팽이
/// </summary>
using UnityEngine;
public class SnailShadow : Shadow
{
    [Header("Snail Slime")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float slimeDestroyTime = 3f;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;
    [SerializeField] private Vector3 spawnScale = Vector3.one * 0.5f;
    private Coroutine slimeRoutine;

    // 변형
    [Header("변형")]
    [SerializeField] private float _cycleTime = 10f;
    public float CycleTime => _cycleTime;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new SnailShadowStateMachine(this);
        stateMachine.Init();
    }

    protected override void Update()
    {
        base.Update();


    }

    public void StartSlime()
    {
        if (slimePrefab == null)
        {
            return;
        }

        if (slimeRoutine != null) StopCoroutine(slimeRoutine);   //slimeRoutine이 이미 돌고 있으면 멈추기 중복실행 방지 
        slimeRoutine = StartCoroutine(SlimeTrailCoroutine());
    }

    public void StopSlime()
    {
        if (slimeRoutine != null)
        {
            StopCoroutine(slimeRoutine);
            slimeRoutine = null;
        }
    }

    private IEnumerator SlimeTrailCoroutine()
    {
        // Chase 전환 “시점부터” 생성되게: 즉시 1번 뿌리고 싶으면 아래 SpawnSlime() 유지
        // 전환 후 3초 뒤부터 뿌리고 싶으면 이 줄을 지우고 WaitForSeconds부터 시작
        SpawnSlime();

        var wait = new WaitForSeconds(spawnInterval);

        while (true)
        {
            yield return wait;
            SpawnSlime();
        }
    }

    private void SpawnSlime()
    {
        Vector3 pos = transform.position + spawnOffset;
        Quaternion rot = Quaternion.identity;

        GameObject slime = Instantiate(slimePrefab, pos, rot);
        slime.transform.localScale = spawnScale;
        Destroy(slime, slimeDestroyTime);
    }

    protected override bool CanTransform()
    {
        return false;
    }

    protected override void ResetTransformFlag()
    {
    }

    #region 에디터 전용
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        MoveStatusData = AssetLoader.FindAndLoadByName<MoveStatusData>("SnailMoveStatusData");
    }
#endif
    #endregion
}
