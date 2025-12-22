using UnityEngine;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] protected Animator _animator;
    [SerializeField] StatusData _statusData;
    public Vector3 Forward => _forward;
    protected Vector3 _forward;

    public Status Status => _status;
    protected Status _status;

    protected StateMachine _curStateMachine;

    private void Awake()
    {
        _forward = transform.forward;

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        _status = _statusData?.GetNewStatus();

        Init();
    }

    protected abstract void Init();

    public virtual void Attack()
    {
    }

    public virtual void GiveEffect()
    {
    }

    public virtual void Damage(float damage)
    {
    }

    public virtual void ApplyEffect()
    {
    }
}