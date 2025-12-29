using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowForestScene : BaseScene
{
    private void Start()
    {
        PoolManager.Instance?.UsePool(PoolType.HowlWindPool);
        
        Player player = FindObjectOfType<Player>(); 
        GameManager.Instance?.SetPlayer(player);
        
        
        if (player != null)
        {
            PlayerSkillController skillController = player.GetComponent<PlayerSkillController>();
            if (skillController != null)
            {
                BeamSkill beamSkill = player.gameObject.AddComponent<BeamSkill>();
                if (skillController.TryAcquireSkill(SkillType.Beam, beamSkill))
                {
                    skillController.BindInput(SkillType.Beam,InputType.Skill,InputMapName.Default,InputActionName.Skill_Beam);
                }
                
                CryBindingSkill cryBindingSkill = player.gameObject.AddComponent<CryBindingSkill>();
                if (skillController.TryAcquireSkill(SkillType.CryBinding, cryBindingSkill))
                {
                    skillController.BindInput(SkillType.CryBinding,InputType.Skill,InputMapName.Default,InputActionName.Skill_CryBinding);
                }
            }
        }
        
    }
}
