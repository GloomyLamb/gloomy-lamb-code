using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 스킬 기본 클래스
/// </summary>
public abstract class BaseSkill : MonoBehaviour, IAttackable
{
    #region 필드
    // 스킬 데이터
    protected SkillStatusData skillStatusData;

    // 스킬 사용 조건
    protected float cooldownTimer = 0f;                 // 쿨타임 타이머
    public bool IsUsable => IsCooldownReady() && HasEnoughResource();

    // 타겟
    protected IDamageable target;

    // 이벤트
    public event Func<BaseSkill, bool> OnStartSkill;
    public event Action OnEndSkill;

    // 코루틴
    private Coroutine _coroutine;
    private WaitForSeconds _delay;
    #endregion

    #region 초기화
    public virtual void Init(SkillStatusData data)
    {
        skillStatusData = data;
        _delay = new WaitForSeconds(data.Duration.TotalTime);

        // input이랑 바인딩하기
    }
    #endregion

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    /// <summary>
    /// [public] input handler에 구독할 이벤트
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnUseSkill(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            UseSkill();
        }
    }

    /// <summary>
    /// 스킬 내부 로직
    /// </summary>
    protected virtual void UseSkill()
    {
        if (IsUsable)
        {
            bool canStart = OnStartSkill == null || OnStartSkill.Invoke(this);
            if (!canStart)
            {
                Logger.LogWarning("스킬 사용 불가");
                return;
            }

            Attack();
            GiveEffect();

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(nameof(EndSkillCoroutine));
        }
        else
        {
            Logger.Log($"{skillStatusData.Type} 스킬 사용 불가");
        }
    }

    /// <summary>
    /// 스킬 총 시간이 지난 후에 스킬 완료 이벤트 사용하기
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndSkillCoroutine()
    {
        yield return _delay;
        OnEndSkill?.Invoke();
    }

    #region IAttackable 구현
    public virtual void Attack()
    {
        Logger.Log("공격");
        if (target == null)
        {
            Logger.Log("타겟 없음");
            return;
        }
        target.Damage(skillStatusData.AttackDamage);
        GiveEffect();
        ResetCooldownTimer();
    }

    public virtual void GiveEffect()
    {
        target.ApplyEffect();
    }
    #endregion

    #region 스킬 조건 계산
    /// <summary>
    /// 쿨타임 확인 메서드
    /// </summary>
    /// <returns></returns>
    protected bool IsCooldownReady()
    {
        bool coolReady = cooldownTimer > skillStatusData.Cooldown;
        Logger.Log($"쿨타임 체크: {coolReady}");
        return coolReady;
    }

    /// <summary>
    /// 스킬의 여러 사용 가능 조건을 달성했는지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    protected abstract bool HasEnoughResource();

    /// <summary>
    /// 쿨타임 타이머 초기화
    /// </summary>
    private void ResetCooldownTimer()
    {
        cooldownTimer = 0f;
    }
    #endregion

    #region 테스트
    public void Test_UseSkill()
    {
        UseSkill();
        Logger.Log("스킬 사용 테스트");
    }
    #endregion
}
