using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 챕터 1 광선 발사 스킬
/// </summary>
public class Laser : BaseSkill
{
    [Header("빛 게이지")]
    [SerializeField] private float _useLightGauge = 10f;    // 빛 소모량
    [SerializeField] private float _fillLightGauge = 5f;    // 빛 충전량

    [SerializeField] private float _lightGauge;             // 빛 게이지
    [SerializeField] private float _lightGaugeMax;          // 빛 최대 게이지
    private bool _useSkill;

    #region 초기화
    /// <summary>
    /// [public] input handler에 스킬 이벤트 바인딩
    /// </summary>
    /// <param name="input"></param>
    public void BindInput(InputHandler input)
    {
        InputManager.Instance.BindInputEvent(InputType.Player,InputMapName.Default, InputActionName.Skill_Laser, OnSkill);
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _useSkill = true;
            if (_lightGauge <= 0)
            {
                _useSkill = false;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _useSkill = false;
        }
    }
    #endregion

    protected override void Update()
    {
        base.Update();

        UpdateLightGauge();
        if (_useSkill)
        {
            UpdateLaserState();
        }
    }

    /// <summary>
    /// [public] 광선 쏘기 효과 주기
    /// </summary>
    public override void GiveEffect()
    {
        Logger.Log("효과 주기");
    }

    /// <summary>
    /// 빛 게이지 관리
    /// </summary>
    private void UpdateLightGauge()
    {
        if (_useSkill)
        {
            _lightGauge = Math.Max(_lightGauge - _useLightGauge * Time.deltaTime, 0);
        }
        else
        {
            _lightGauge = Math.Min(_lightGauge + _fillLightGauge * Time.deltaTime, _lightGaugeMax);
        }
    }

    private void UpdateLaserState()
    {
        ShootLight();                               // todo: 라이트 쏘기
        IDamageable target = DetectDamageable();    // todo: 범위 탐지하기
        target.Damage(10f);                         // todo: 탐지에 걸린 몬스터 데려와서 데미지 and 효과 주기
    }

    /// <summary>
    /// 라이트 쏘기
    /// </summary>
    private void ShootLight()
    {

    }

    /// <summary>
    /// 범위로 탐지해서 데미지 줄 수 있는 오브젝트 찾기
    /// </summary>
    /// <returns></returns>
    private IDamageable DetectDamageable()
    {
        // physics overlap sphere로 탐지 
        // 범위는 수치로 빼서 테스트 하도록 하기
        return null;
    }

    protected override bool HasEnoughResource()
    {
        throw new NotImplementedException();
    }
}
