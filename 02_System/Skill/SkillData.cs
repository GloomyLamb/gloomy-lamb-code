using UnityEngine;

[CreateAssetMenu(fileName = "new SkillData", menuName = "SO/Skill/Skill Data")]
public class SkillData : ScriptableObject
{
    [field: SerializeField] public SkillType Type { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public AttackRange AttackRange { get; private set; }
    [field: SerializeField] public Duration Duration { get; private set; }
}

[System.Serializable]
public class AttackRange
{
    // 임의로 넣은 값이라 더 회의 필요
    [field: SerializeField] public float Radius { get; private set; }
}

[System.Serializable]
public class Duration
{
    [field: SerializeField] public float TotalTime { get; private set; }
    [field: SerializeField] public float AttackPoint { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
}