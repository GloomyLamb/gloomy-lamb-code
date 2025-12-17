using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private SkillStatusDatabase _skillStatusDatabase;

    private Dictionary<SkillType, BaseSkill> _skillDict = new();

    private BaseSkill _curSkill = null;

    /// <summary>
    /// [public] 스킬 획득
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseSkill"></param>
    public bool TryGetSkill(SkillType type, BaseSkill baseSkill)
    {
        if (!_skillStatusDatabase.TryGetSkillStatusData(type, out SkillStatusData data))
        {
            Destroy(baseSkill);
            Logger.LogWarning($"얻을 수 없는 스킬 데이터: {type}");
            return false;
        }

        if (_skillDict.ContainsKey(type))
        {
            Logger.LogWarning("이미 존재하는 스킬");
            return false;
        }

        _skillDict.Add(type, baseSkill);

        baseSkill.Init(data);
        baseSkill.OnStartSkill += HandleStartSkill;
        baseSkill.OnEndSkill += HandleEndSkill;

        return true;
    }

    /// <summary>
    /// 스킬 사용 시 curSkill에 값 저장
    /// </summary>
    /// <param name="baseSkill"></param>
    private void HandleStartSkill(BaseSkill baseSkill)
    {
        if (_curSkill != null)
        {
            Logger.LogWarning("이미 다른 스킬 사용 중");
            return;
        }
        _curSkill = baseSkill;
    }

    /// <summary>
    /// 스킬 사용 완료 시 curSkill에서 값 제거
    /// </summary>
    private void HandleEndSkill()
    {
        _curSkill = null;
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        _skillStatusDatabase = AssetLoader.FindAndLoadByName<SkillStatusDatabase>("TestSkillStatusDatabase");
    }
#endif
    #endregion

}
