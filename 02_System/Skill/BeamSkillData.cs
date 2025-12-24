using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BeamSkillData", menuName = "SO/Skill/Skill Status Data")]
public class BeamSkillData : SkillStatusData
{
    [Header("Beam Prefab")]
    public BeamController BeamPrefab;
    
    [Header("Light Gauge")]
    public float LightGauge;
    public float MaxLightGauge = 100f;
    public float ChargeTimeGauge = 60f;

    [Header("Consume")]
    public float ConsumeTickGauge = 10f;
    public float ConsumeTickSec = 0.1f;
}
