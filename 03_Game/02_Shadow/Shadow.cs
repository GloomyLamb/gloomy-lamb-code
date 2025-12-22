using UnityEngine;

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    [Header("애니메이션")]
    [SerializeField] protected Animator animator;

    protected ShadowStateMachine stateMachine;

    // todo: 추후 SO로 분리
    [SerializeField] protected float movementSpeed = 10f;
    [field: SerializeField] public float MovementSpeedModitier { get; set; } = 1f;
    protected float rotatingDamping = 60f;
    [SerializeField] protected float damage = 10f;

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
