using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬을 저장하고 관리하는 컨트롤러
/// </summary>
public class SkillController : MonoBehaviour
{
    [SerializeField] private SkillDatabase _skillDatabase;

    // 스킬 상태 관리
    protected readonly Dictionary<SkillType, BaseSkill> skillDict = new();
    private BaseSkill _curSkill = null;


    /// <summary>
    /// [public] 스킬 획득
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseSkill"></param>
    public bool TryAcquireSkill(SkillType type, BaseSkill baseSkill)
    {
        if (!_skillDatabase.TryGetSkillStatusData(type, out SkillData data))
        {
            Destroy(baseSkill);
            Logger.LogWarning($"얻을 수 없는 스킬 데이터: {type}");
            return false;
        }

        if (skillDict.ContainsKey(type))
        {
            Logger.LogWarning("이미 존재하는 스킬");
            return false;
        }

        skillDict.Add(type, baseSkill);

        baseSkill.Init(data);
        baseSkill.OnStartSkill += HandleStartSkill;
        baseSkill.OnEndSkill += HandleEndSkill;

        return true;
    }


    #region curSkill 관리
    /// <summary>
    /// 스킬 사용 시 curSkill에 값 저장
    /// </summary>
    /// <param name="baseSkill"></param>
    private bool HandleStartSkill(BaseSkill baseSkill)
    {
        if (_curSkill != null)
        {
            Logger.LogWarning("이미 다른 스킬 사용 중");
            return false;
        }
        _curSkill = baseSkill;
        return true;
    }

    /// <summary>
    /// 스킬 사용 완료 시 curSkill에서 값 제거
    /// </summary>
    private void HandleEndSkill()
    {
        _curSkill = null;
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        if (_skillDatabase == null)
        {
            _skillDatabase = AssetLoader.FindAndLoadByName<SkillDatabase>("SkillDatabase");
        }
    }
#endif
    #endregion
}
