using UnityEngine;

/// <summary>
/// 그림자 - 슬라임
/// </summary>
public class SlimeShadow : Shadow
{
    // 추격 조건
    [field: Header("추격 조건")]
    [field: SerializeField] public float SlowChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float FastChasePatternTime { get; private set; } = 1f;
    [field: SerializeField] public float MaxScale { get; private set; } = 3f;
    [field: SerializeField] public float MinScale { get; private set; } = 1f;
    public bool IsFastMode { get; set; }
    public int ChaseCount { get; private set; } = 0;

    // 변형 조건
    private bool _checkExpand;
    private bool CheckScale => transform.localScale.x == 1f;
    public bool IsHitting { get; set; } // 일단 맞을 때 이거 변환

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new SlimeShadowStateMachine(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damage);
        }
    }

    public void PlusChaseCount()
    {
        ChaseCount++;
        Logger.Log($"추격 횟수: {ChaseCount}");
    }

    #region 변형
    protected override bool CanTransform()
    {
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
}
