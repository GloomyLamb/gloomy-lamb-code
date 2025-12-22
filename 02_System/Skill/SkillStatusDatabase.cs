using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SkillStatusDatabase", menuName = "SO/Skill/Skill Status Database")]
public class SkillStatusDatabase : ScriptableObject
{
    [SerializeField] private List<SkillStatusData> _skillStatusDatas;

    private Dictionary<SkillType, SkillStatusData> _cache;

    private void OnEnable()
    {
        _cache = new();
        foreach (var data in _skillStatusDatas)
        {
            if (!_cache.ContainsKey(data.Type))
            {
                _cache.Add(data.Type, data);
            }
        }
    }

    public bool TryGetSkillStatusData(SkillType type, out SkillStatusData data)
    {
        return _cache.TryGetValue(type, out data);
    }
}
