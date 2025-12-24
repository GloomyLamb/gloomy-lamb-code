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

    private void Awake()
    {
        _slimeShadow.Init(this);
        _dogShadow.Init(this);
        _snailShadow.Init(this);

        _curShadow = _slimeShadow;
        ChangeToSlime();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _slimeShadow.OnMove += HandleMove;
        _dogShadow.OnMove += HandleMove;
        _snailShadow.OnMove += HandleMove;

        _slimeShadow.OnChange += ChangeToDog;
        _dogShadow.OnChange += ChangeToSnail;
        _snailShadow.OnChange += ChangeToDog;
    }

    private void OnDisable()
    {
        _slimeShadow.OnMove -= HandleMove;
        _dogShadow.OnMove -= HandleMove;
        _snailShadow.OnMove -= HandleMove;

        _slimeShadow.OnChange -= ChangeToDog;
        _dogShadow.OnChange -= ChangeToSnail;
        _snailShadow.OnChange -= ChangeToDog;
    }

    private void HandleMove()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * _curShadow.MovementSpeed * _curShadow.MovementSpeedModitier * Time.deltaTime;
    }

    #region 변형
    private void ChangeToSlime()
    {
        Logger.Log("슬라임으로 변형");
        _slimeShadow.gameObject.SetActive(true);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(false);
    }

    private void ChangeToDog()
    {
        Logger.Log("개로 변형");
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(true);
        _snailShadow.gameObject.SetActive(false);
    }

    private void ChangeToSnail()
    {
        Logger.Log("달팽이 변형");
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(true);
    }
    #endregion
}
