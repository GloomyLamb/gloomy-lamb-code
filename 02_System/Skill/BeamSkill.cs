using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BeamSkill : BaseSkill
{
    [SerializeField] private float lightGauge; // 현재 빛 게이지
    [SerializeField] private float maxLightGauge; // 최대 빛 게이지
    [SerializeField] private float chargeTimeGauge; // 빛 게이지 충전 시간
    [SerializeField] private float consumeTickGauge; // 빔 사용시 틱당 소모되는 빛 게이지
    [SerializeField] private float consumeTickSec; // 빔 사용시 틱당 시간

    [Header("References")] //실제 빔연출 컨트롤러 불러오기
    [SerializeField] private BeamController beamController;

    private bool isBeaming = false; // 빔 발사 중인지 여부

    private float tickTimer = 0f; // 틱 타이머

    private bool hasUsedOnce = false; // 빔 스킬이 한 번이라도 사용되었는지 여부 
    // 첫사용에만 시작조건이 100이기때문에 변수명 선언
    //false면 시작조건 = 100 , true면 시작조건 = 10 이상 


    private void Awake()
    {
        // if (beamController == null)
        //     beamController = GetComponentInChildren<BeamController>(true);
    }

    public override void Init(SkillStatusData data)
    {
        base.Init(data);

        if (beamController != null)
            beamController.SetEnabled(false);

        BeamSkillData beamData = (BeamSkillData)data;
        if (beamData == null) return;

        beamSkillData = beamData;
        maxLightGauge = beamSkillData.MaxLightGauge;
        chargeTimeGauge = beamSkillData.ChargeTimeGauge;
        consumeTickGauge = beamSkillData.ConsumeTickGauge;
        consumeTickSec = beamSkillData.ConsumeTickSec;

        // 테스트용: 시작 시 풀게이지
        lightGauge = maxLightGauge;
        hasUsedOnce = false;
        isBeaming = false;
        tickTimer = 0f;

        beamController = Instantiate(beamData.BeamPrefab);
        if (beamController != null)
        {
            PlayerSkillController skillController = GetComponent<PlayerSkillController>();
            if (skillController != null)
            {
                if (skillController._skillPivot == null || skillController._skillPivot.Count <= 0) return;
                beamController.transform.parent = skillController._skillPivot[0];
                beamController.transform.localPosition = Vector3.zero;
                beamController.SetEnabled(false);    
            }
            
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isBeaming) // 빔 발사 중이 아니면 게이지 충전시작
            ChargeGauge();
        else
            ConsumeGaugeTick();
    }

    public override void OnUseSkill(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        {
            UseSkill();
        }
        else if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
        {
            StopBeam();
        }
    }

    public override void Attack()
    {
        // 여기서부터가 “빔 시작” 처리

        isBeaming = true;
        tickTimer = 0f;
        hasUsedOnce = true;

        if (beamController != null)
            beamController.PlayBeam();
    }


    protected override bool HasEnoughResource()
    {
        if (!hasUsedOnce)
            return lightGauge >= maxLightGauge;
        return lightGauge >= consumeTickGauge;
    }

    private void StopBeam()
    {
        if (!isBeaming) return;

        isBeaming = false;
        tickTimer = 0f;

        if (beamController != null)
            beamController.SetEnabled(false);
    }

    private void ChargeGauge()
    {
        if (chargeTimeGauge <= 0f) return;

        float chargePerSec = maxLightGauge / chargeTimeGauge; // 100/60
        lightGauge = Mathf.Clamp(lightGauge + chargePerSec * Time.deltaTime, 0f, maxLightGauge);
    }

    private void ConsumeGaugeTick()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= consumeTickSec)
        {
            tickTimer -= consumeTickSec;
            Debug.Log($"빔 사용 전 - 남은 게이지: {lightGauge}");
            lightGauge = Mathf.Clamp(lightGauge - consumeTickGauge, 0f, maxLightGauge);

            Debug.Log($"빔 사용 중 - 남은 게이지: {lightGauge}");
            if (lightGauge <= 0f)
            {
                StopBeam();
            }
        }
    }

    public override void GiveEffect()
    {
        Logger.Log("빔 이펙트 ON");
    }

    public void Test_BeamUseSkill()
    {
        UseSkill();
        Logger.Log("스킬 사용 테스트");
    }
}