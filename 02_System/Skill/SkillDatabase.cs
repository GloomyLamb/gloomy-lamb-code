using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SkillDatabase", menuName = "SO/Skill/Skill Database")]
public class SkillDatabase : ScriptableObject
{
    [SerializeField] private List<SkillData> _skillDatabase;

    private Dictionary<SkillType, SkillData> _cache;

    private void OnEnable()
    {
        _cache = new();
        foreach (var data in _skillDatabase)
        {
            if (!_cache.ContainsKey(data.Type))
            {
                _cache.Add(data.Type, data);
            }
        }
    }

    public bool TryGetSkillStatusData(SkillType type, out SkillData data)
    {
        return _cache.TryGetValue(type, out data);
    }
}
