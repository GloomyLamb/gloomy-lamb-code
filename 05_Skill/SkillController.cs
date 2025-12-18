using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 스킬을 저장하고 관리하는 컨트롤러
/// </summary>
public class SkillController : MonoBehaviour
{
    [SerializeField] private SkillStatusDatabase _skillStatusDatabase;

    // 스킬 상태 관리
    private readonly Dictionary<SkillType, BaseSkill> _skillDict = new();
    private BaseSkill _curSkill = null;

    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Skill);
    }

    /// <summary>
    /// [public] 스킬 획득
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseSkill"></param>
    public bool TryAcquireSkill(SkillType type, BaseSkill baseSkill)
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
    /// [public] 스킬과 input 묶기
    /// </summary>
    /// <param name="type"></param>
    /// <param name="inputType"></param>
    /// <param name="inputMapName"></param>
    /// <param name="inputActionName"></param>
    public void BindInput(
        SkillType type,
        InputType inputType,
        InputMapName inputMapName,
        InputActionName inputActionName
        )
    {
        if (!_skillDict.TryGetValue(type, out BaseSkill skill))
        {
            Logger.LogWarning($"{skill} 없음");
            return;
        }

        InputManager.Instance.BindInputEvent(inputType, inputMapName, inputActionName, skill.OnUseSkill);
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
        _skillStatusDatabase = AssetLoader.FindAndLoadByName<SkillStatusDatabase>("TestSkillStatusDatabase");
    }
#endif
    #endregion

    #region 테스트
    public void Test_GetTestSkill()
    {
        TestSkill skill = this.AddComponent<TestSkill>();
        if (!TryAcquireSkill(SkillType.Test, skill)) return;
        Logger.Log($"{_skillDict[SkillType.Test]} 연결 완료");
    }

    public void Test_GetBeamSkill()
    {
        Laser skill = this.AddComponent<Laser>();
        if (!TryAcquireSkill(SkillType.Beam, skill)) return;
        Logger.Log($"{_skillDict[SkillType.Beam]} 연결 완료");
    }

    public void Test_UseTestSkill()
    {
        if (!_skillDict.TryGetValue(SkillType.Test, out BaseSkill skill))
        {
            Logger.Log("스킬 없음");
            return;
        }
        skill.Test_UseSkill();
    }
    #endregion
}
