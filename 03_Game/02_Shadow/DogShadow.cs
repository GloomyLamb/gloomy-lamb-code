using UnityEngine;

/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    [field: SerializeField] public DogShadowAnimationData AnimationData { get; private set; }

    [field: SerializeField] public Transform Target { get; private set; }

    private void Awake()
    {
        stateMachine = new DogShadowStateMachine(this, animator);
        AnimationData.Initialize();
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

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damage);
        }
    }
}
