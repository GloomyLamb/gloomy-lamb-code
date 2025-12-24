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

    #region 초기화
    protected override void Awake()
    {
        base.Awake();

        _slimeShadow.Init(this);
        _dogShadow.Init(this);
        _snailShadow.Init(this);

        TransformToSlime();
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

        _slimeShadow.OnTransform += TransformToDog;
        _dogShadow.OnTransform += TransformToSnail;
        _snailShadow.OnTransform += TransformToDog;
    }
    #endregion

    private void OnDisable()
    {
        _slimeShadow.OnMove -= HandleMove;
        _dogShadow.OnMove -= HandleMove;
        _snailShadow.OnMove -= HandleMove;

        _slimeShadow.OnTransform -= TransformToDog;
        _dogShadow.OnTransform -= TransformToSnail;
        _snailShadow.OnTransform -= TransformToDog;
    }

    #region 변형
    private void TransformToSlime()
    {
        Logger.Log("슬라임으로 변형");
        _slimeShadow.gameObject.SetActive(true);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(false);
        curShadow = _slimeShadow;
    }

    private void TransformToDog()
    {
        Logger.Log("개로 변형");
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(true);
        _snailShadow.gameObject.SetActive(false);
        curShadow = _dogShadow;
    }

    private void TransformToSnail()
    {
        Logger.Log("달팽이 변형");
        _slimeShadow.gameObject.SetActive(false);
        _dogShadow.gameObject.SetActive(false);
        _snailShadow.gameObject.SetActive(true);
        curShadow = _snailShadow;
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();

        _slimeShadow = transform.FindChild<SlimeShadow>("SlimeShadow");
        _dogShadow = transform.FindChild<DogShadow>("DogShadow");
        _snailShadow = transform.FindChild<SnailShadow>("SnailShadow");
    }
#endif
    #endregion
}
