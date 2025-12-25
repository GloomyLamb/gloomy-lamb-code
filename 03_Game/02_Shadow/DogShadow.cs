using System.Collections;
using UnityEngine;

/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    [field: SerializeField] public DogShadowAnimationData SkillAnimationData { get; private set; }

    // todo: SO로 분리, 스킬 추가
    [Header("물기")]
    [SerializeField] private DamageableDetector _biteDetector;
    [SerializeField] private float _biteDetectRange = 1.2f;         // 탐지 범위
    [SerializeField] private float _biteDamage = 20f;               // 대미지
    [SerializeField] private float _biteKnockbackDuration = 1f;     // 넉백 시간
    [SerializeField] private float _biteKnockbackSpeed = 3f;        // 넉백 속도
    [SerializeField] private float _healValue = 10f;                // 흡혈
    [SerializeField] private int _biteSuccessThreshold = 3;         // 물기 패턴 최대 횟수
    [field: SerializeField] public float BiteDuration = 0.5f;
    public float SqrBiteRange => _biteDetectRange * _biteDetectRange;
    public int BiteCount { get; private set; } = 0;

    [Header("짖기")]
    [SerializeField] private HowlWind _howlWindPrefab;

    // 변형 조건
    public bool DonePattern { get; set; }

    #region 초기화

    protected override void Awake()
    {
        base.Awake();

        SkillAnimationData.Initialize();
        stateMachine = new DogShadowStateMachine(this);
        stateMachine.Init();
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

    Coroutine _biteCoroutine;

    public void SpawnHowlWind()
    {
        Instantiate(_howlWindPrefab, transform.position, Quaternion.identity);
    }

    public void Bite()
    {
        Player target = _biteDetector.CurrentTarget as Player;

        // 타겟 존재
        if (target != null)
        {
            target.Damage(_biteDamage);             // 대미지
            controller.Status.AddHp(_healValue);    // 흡혈
            target.TakeStun();                      // 플레이어 효과 주기
            BiteCount++;

            if (_biteCoroutine != null)             // 뒤로 가기
            {
                StopCoroutine(_biteCoroutine);
                _biteCoroutine = null;
            }

            _biteCoroutine = StartCoroutine(KnockbackCoroutine());
        }
        else
        {
            Logger.Log("물기 공격 실패 -> 추적 모드");
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
    }

    /// <summary>
    /// 물기 공격 이후 넉백 동작하기
    /// </summary>
    /// <returns></returns>
    private IEnumerator KnockbackCoroutine()
    {
        controller.StopNevMeshAgent();

        Vector3 dir = -controller.transform.forward;
        float timer = 0f;

        while (timer < _biteKnockbackDuration)
        {
            controller.transform.position += _biteKnockbackSpeed * Time.deltaTime * dir;
            timer += Time.deltaTime;
            yield return null;
        }

        controller.StartNevMeshAgent();

        // 상태 전이
        if (BiteCount < _biteSuccessThreshold)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
        else
        {
            stateMachine.ChangeState(((DogShadowStateMachine)stateMachine).BarkState);
        }
    }

    #endregion
}