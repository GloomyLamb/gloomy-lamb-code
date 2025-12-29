using System.Collections;
using UnityEngine;

/// <summary>
/// 그림자 - 슬라임
/// </summary>
public class SlimeShadow : Shadow
{
    // 추격 조건
    [field: Header("추격 조건")]
    [field: SerializeField] public int TotalChaseCount { get; private set; } = 20;
    [field: SerializeField] public int SlowChaseCount { get; private set; } = 10;
    [field: SerializeField] public float StopPatternTime { get; private set; } = 0.5f;
    [field: SerializeField] public float SlowChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float FastChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float MaxScale { get; private set; } = 3f;
    [field: SerializeField] public float MinScale { get; private set; } = 1f;
    public int CurChaseCount { get; private set; } = 0;

    [field: Header("추가 설정")]
    [field: SerializeField] public float ScaleUpDuration { get; private set; } = 1f;
    [field: SerializeField] public float ScaleDownDuration { get; private set; } = 1f;

    // 변형 조건
    public bool DoneExpand { get; private set; }
    private bool CheckScale => transform.localScale.x == MinScale;
    public bool IsHitting { get; set; } // 일단 맞을 때 이거 변환

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new SlimeShadowStateMachine(this);
        stateMachine.Init();
    }

    public override void ApplyEffect()
    {
        StartScaleDownRoutine();
    }

    public override void StopEffect()
    {
        if (scaleDownRoutine != null)
            StopCoroutine(scaleDownRoutine);
    }

    public void PlusChaseCount()
    {
        CurChaseCount++;
        //Logger.Log($"추격 횟수: {CurChaseCount}");
    }

    #region 변형

    protected override bool CanTransform()
    {
        return DoneExpand && CheckScale;
    }

    protected override void ResetTransformFlag()
    {
        DoneExpand = false;
    }

    public void CheckExpand()
    {
        DoneExpand = true;
        SetCollisionDamage(20f);
    }

    #endregion

    private Coroutine scaleDownRoutine;

    void StartScaleDownRoutine()
    {
        if (scaleDownRoutine != null)
            StopCoroutine(scaleDownRoutine);
        scaleDownRoutine = StartCoroutine(ScaleDownRoutine(MinScale, ScaleDownDuration));
    }

    protected IEnumerator ScaleDownRoutine(float size, float duration)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one * size;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }

    #region 에디터 전용
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        MoveStatusData = AssetLoader.FindAndLoadByName<MoveStatusData>("SlimeMoveStatusData");
    }
#endif
    #endregion
}