using System.Collections;
using UnityEngine;

/// <summary>
/// 그림자 - 슬라임
/// </summary>
public class SlimeShadow : Shadow
{
    [field: SerializeField] public SlimeShadowAnimationData AnimationData { get; private set; }

    // 추격 조건
    [field: Header("추격 조건")]
    [field: SerializeField] public float SlowChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float FastChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float MaxScale { get; private set; } = 3f;
    [field: SerializeField] public float MinScale { get; private set; } = 1f;
    public bool IsFastMode { get; set; }

    [Header("추가 설정")]
    [SerializeField] public float _scaleDownDuration = 1f;
    // 변형 조건
    private bool _checkExpand;
    private bool CheckScale => transform.localScale.x == 1f;
    public bool IsHitting { get; set; } // 일단 맞을 때 이거 변환

    protected override void Awake()
    {
        base.Awake();

        AnimationData.Initialize();
        stateMachine = new SlimeShadowStateMachine(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            //todo : 
            damageable.Damage(damage);
        }
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


    #region 변형

    protected override bool CanTransform()
    {
        return false;
        return _checkExpand && CheckScale;
    }

    protected override void ResetTransformFlag()
    {
        _checkExpand = false;
    }

    public void CheckExpand()
    {
        _checkExpand = true;
    }

    #endregion


    private Coroutine scaleDownRoutine;

    void StartScaleDownRoutine()
    {
        if (scaleDownRoutine != null)
            StopCoroutine(scaleDownRoutine);
        scaleDownRoutine = StartCoroutine(ScaleDownRoutine(MinScale, _scaleDownDuration));
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
}