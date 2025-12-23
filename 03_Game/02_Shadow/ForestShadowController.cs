using UnityEngine;

/// <summary>
/// 챕터 1 그림자 숲 그림자
/// </summary>
public class ForestShadowController : ShadowController
{
    [Header("Shadows")]
    [SerializeField] private SlimeShadow _idleShadow;
    [SerializeField] private DogShadow _dogShadow;
    [SerializeField] private SnailShadow _snailShadow;
    private Shadow _curShadow;

    [field: Header("Target")]
    [field: SerializeField] public Transform Target;

    private void Awake()
    {
        _curShadow = _idleShadow;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Start()
    {
        if (Target == null)
        {
            Target = FindObjectOfType<Player>().transform;
        }
    }

    private void Init()
    {
        _idleShadow.OnMove += HandleMove;
        _dogShadow.OnMove += HandleMove;
        _snailShadow.OnMove += HandleMove;
    }

    private void OnDisable()
    {
        _idleShadow.OnMove -= HandleMove;
        _dogShadow.OnMove -= HandleMove;
        _snailShadow.OnMove -= HandleMove;
    }

    private void HandleMove()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * _curShadow.MovementSpeed * Time.deltaTime;
    }
}
