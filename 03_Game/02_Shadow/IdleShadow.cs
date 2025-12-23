using UnityEngine;

/// <summary>
/// 그림자 - 기본
/// </summary>
public class IdleShadow : Shadow
{
    [field: SerializeField] public IdleShadowAnimationData AnimationData { get; private set; }

    [field: SerializeField] public Transform Target { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        stateMachine = new IdleShadowStateMachine(this, animator);
    }

    private void Start()
    {
        if (Target == null)
        {
            Target = FindObjectOfType<Player>().transform;
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damage);
        }
    }
}
