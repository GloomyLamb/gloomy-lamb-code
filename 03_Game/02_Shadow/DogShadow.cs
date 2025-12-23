using UnityEngine;

/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    [field: SerializeField] public DogShadowAnimationData AnimationData { get; private set; }

    [field: SerializeField] public Transform Target;

    // todo: SO로 분리, 스킬 추가
    [SerializeField] private float _biteRange = 3f;
    public float SqrBiteRange => _biteRange * _biteRange;
    public int BiteCount { get; set; } = 0;

    private void Awake()
    {
        AnimationData.Initialize();
        stateMachine = new DogShadowStateMachine(this, animator);
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

    public void HandleMove()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * MovementSpeed * Time.deltaTime;
    }
}
