using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : SkillController
{
    // todo: pivot 이름 잡아서 enum 키 dictionary 로 바꿔놓기 
    [SerializeField] public List<Transform> _skillPivot;

    void Start()
    {
        InputManager.Instance.UseInput(InputType.Skill);
        
        BeamSkill beamSkill = this.gameObject.AddComponent<BeamSkill>();
        if (TryAcquireSkill(SkillType.Beam, beamSkill))
        {
            BindInput(SkillType.Beam,InputType.Skill,InputMapName.Default,InputActionName.Skill_Beam);
            //beamSkill.SetBeamData(beamSkillDataSO);
        }
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
        if (!skillDict.TryGetValue(type, out BaseSkill skill))
        {
            Logger.LogWarning($"{skill} 없음");
            return;
        }

        InputManager.Instance.BindInputEvent(inputType, inputMapName, inputActionName, skill.OnUseSkill);
    }
   
}
