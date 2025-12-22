using UnityEngine;

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    [Header("애니메이터")]
    [SerializeField] protected Animator animator;

    protected ShadowStateMachine stateMachine;

    // IDamageable
    public virtual void ApplyEffect()
    {
    }

    public virtual void Damage(float damage)
    {
    }

    // IAttackable
    public virtual void Attack()
    {
    }

    public virtual void GiveEffect()
    {
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        animator = transform.FindChild<Animator>("Model");
    }
#endif
    #endregion
}
