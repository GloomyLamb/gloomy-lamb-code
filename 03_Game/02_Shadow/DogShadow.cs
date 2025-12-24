using UnityEngine;

/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    [field: SerializeField] public DogShadowAnimationData SkillAnimationData { get; private set; }

    // todo: SO로 분리, 스킬 추가
    [SerializeField] private float _biteRange = 3f;
    public float SqrBiteRange => _biteRange * _biteRange;
    public int BiteCount { get; set; } = 0;

    // 변형 조건
    public bool DonePattern { get; set; }

    #region 초기화
    protected override void Awake()
    {
        base.Awake();

        SkillAnimationData.Initialize();
        stateMachine = new DogShadowStateMachine(this);
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damage);
        }
    }

    #region 변형
    protected override bool CanTransform()
    {
        return DonePattern;
    }

    protected override void ResetTransformFlag()
    {
        DonePattern = false;
    }
    #endregion
}
