using UnityEngine;

/// <summary>
/// 스킬 기본 클래스
/// </summary>
public abstract class BaseSkill : MonoBehaviour, IAttackable
{
    #region 필드
    // 스킬 데이터
    [SerializeField] protected SkillStatusData skillStatusData;

    // 스킬 사용 조건
    protected float cooldownTimer = 0f;                 // 쿨타임 타이머
    public bool IsUsable => IsCooldownReady() && HasEnoughResource();

    // 타겟
    protected IDamageable target;
    #endregion

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    #region IAttackable 구현
    public virtual void Attack()
    {
        if (target == null)
        {
            Logger.Log("타겟 없음");
            return;
        }
        target.Damage(skillStatusData.AttackDamage);
        ResetCooldownTimer();
    }

    public abstract void GiveEffect();
    #endregion

    #region 스킬 조건 계산
    /// <summary>
    /// 쿨타임 확인 메서드
    /// </summary>
    /// <returns></returns>
    protected bool IsCooldownReady()
    {
        return cooldownTimer > skillStatusData.Cooldown;
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
}
