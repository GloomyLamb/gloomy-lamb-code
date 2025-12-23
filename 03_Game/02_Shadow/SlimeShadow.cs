using UnityEngine;

/// <summary>
/// 그림자 - 슬라임
/// </summary>
public class SlimeShadow : Shadow
{
    [field: SerializeField] public SlimeShadowAnimationData AnimationData { get; private set; }

    [field: SerializeField] public Transform Target;

    protected override void Awake()
    {
        base.Awake();

        AnimationData.Initialize();
        stateMachine = new SlimeShadowStateMachine(this);
    }

    private void Start()
    {
        if (Target == null)
        {
            Target = FindObjectOfType<Player>().transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damage);
        }
    }

    public void HandleMove()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * MovementSpeed * MovementSpeedModitier * Time.deltaTime;
    }
}
