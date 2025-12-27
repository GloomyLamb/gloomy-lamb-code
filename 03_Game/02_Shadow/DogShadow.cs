using UnityEngine;

/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    public ShadowController Controller => controller;
    [field: SerializeField] public DogShadowAnimationData SkillAnimationData { get; private set; }

    // todo: SO로 분리, 스킬 추가
    [Header("물기")]
    [SerializeField] private DamageableDetector _biteDetector;
    [SerializeField] private float _biteDetectRange = 1.2f;         // 탐지 범위
    [SerializeField] private float _biteDamage = 20f;               // 대미지
    [SerializeField] private float _healValue = 10f;                // 흡혈
    [SerializeField] private float _biteBackwardLength = 1f;        // 넉백 거리
    [field: SerializeField] public float BiteBackwardDuration { get; private set; } = 1f;   // 넉백 시간
    [field: SerializeField] public int BiteSuccessThreshold { get; private set; } = 3;      // 물기 패턴 최대 횟수
    [field: SerializeField] public float BiteDuration { get; private set; } = 0.5f;
    public float SqrBiteRange => _biteDetectRange * _biteDetectRange;
    public int BiteCount { get; private set; } = 0;

    [field: Header("짖기")]
    [field: SerializeField] public GameObject HowlEffectPrefab { get; private set; }

    // 변형 조건
    public bool DonePattern { get; set; }

    #region 초기화

    protected override void Awake()
    {
        base.Awake();

        SkillAnimationData.Initialize();
        stateMachine = new DogShadowStateMachine(this);
        stateMachine.Init();

        HowlEffectPrefab.SetActive(false);
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

    #region 스킬
    public void SpawnHowlWind()
    {
        PoolManager.Instance?.Spawn(PoolType.HowlWindPool, transform.position, Quaternion.identity);
    }

    public void Bite()
    {
        Player target = _biteDetector.CurrentTarget as Player;

        // 타겟 존재
        if (target != null)
        {
            Logger.Log("타겟 물기 성공");
            target.Damage(_biteDamage);             // 대미지
            controller.Status.AddHp(_healValue);    // 흡혈
            target.TakeStun();                      // 플레이어 효과 주기
            BiteCount++;

            // ai 끄기
            //controller.SetActiveAgentRotation(false);
            stateMachine.ChangeState(((DogShadowStateMachine)stateMachine).BackwardState);
        }
        else
        {
            Logger.Log("물기 공격 실패 -> 추적 모드");
            stateMachine.ChangeState(stateMachine.BoundState);
            return;
        }
    }

    public void Backward()
    {
        Vector3 dir = -Forward;
        Vector3 targetPos = controller.transform.position + dir * _biteBackwardLength;
        controller.Agent.SetDestination(targetPos);
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _biteDetector = transform.FindChild<DamageableDetector>("Pivot_AttackRange_Bite");
        HowlEffectPrefab = transform.FindChild<ParticleSystem>("Particle_BarkEffect").gameObject;
    }
#endif
    #endregion
}