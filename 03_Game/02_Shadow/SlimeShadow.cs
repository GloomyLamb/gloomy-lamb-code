using UnityEngine;

/// <summary>
/// 그림자 - 슬라임
/// </summary>
public class SlimeShadow : Shadow
{
    [field: SerializeField] public SlimeShadowAnimationData AnimationData { get; private set; }

    [field: SerializeField] public Transform Target { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        stateMachine = new SlimeShadowStateMachine(this, animator);
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
