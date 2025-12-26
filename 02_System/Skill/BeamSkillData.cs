using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeamSkillData", menuName = "SO/Skill/Skill Status Data")]
public class BeamSkillData : SkillData
{
    [field: Header("Beam Prefab")]
    [field: SerializeField] public BeamController BeamPrefab;

    [field: Header("Light Gauge")]
    [field: SerializeField] public float LightGauge { get; private set; }
    [field: SerializeField] public float MaxLightGauge { get; private set; } = 100f;
    [field: SerializeField] public float ChargeTimeGauge { get; private set; } = 60f;

    [field: Header("Consume")]
    [field: SerializeField] public float ConsumeTickGauge { get; private set; } = 10f;
    [field: SerializeField] public float ConsumeTickSec { get; private set; } = 0.1f;

    [field: Header("Damage Tick")]
    [field: SerializeField] public float DamageTickSec { get; private set; } = 1.0f;
}