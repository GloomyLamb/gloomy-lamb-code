using UnityEngine;

/// <summary>
/// 챕터 1 그림자 숲 그림자
/// </summary>
public class ForestShadowController : ShadowController
{
    [Header("Shadows")]
    [SerializeField] private SlimeShadow _slimeShadow;
    [SerializeField] private DogShadow _dogShadow;
    [SerializeField] private SnailShadow _snailShadow;
    private Shadow _curShadow;

    [field: Header("Target")]
    [field: SerializeField] public Transform Target;

    private void Awake()
    {
        _curShadow = _slimeShadow;
        TransformToDog();
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
        _slimeShadow.OnMove += HandleMove;
        _dogShadow.OnMove += HandleMove;
        _snailShadow.OnMove += HandleMove;

        _slimeShadow.OnTransform += TransformToDog;
        _dogShadow.OnTransform += TransformToSnail;
        _snailShadow.OnTransform += TransformToDog;
    }

    private void OnDisable()
    {
        _slimeShadow.OnMove -= HandleMove;
        _dogShadow.OnMove -= HandleMove;
        _snailShadow.OnMove -= HandleMove;

        _slimeShadow.OnTransform -= TransformToDog;
        _dogShadow.OnTransform -= TransformToSnail;
        _snailShadow.OnTransform -= TransformToDog;
    }

    private void HandleMove()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * _curShadow.MovementSpeed * _curShadow.MovementSpeedModitier * Time.deltaTime;
    }

    #region 변형
    private void TransformToSlime()
    {
        _slimeShadow.gameObject.SetActive(true);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(false);
    }

    private void TransformToDog()
    {
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(true);
        _snailShadow.gameObject.SetActive(false);
    }

    private void TransformToSnail()
    {
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(true);
    }
    #endregion
}
