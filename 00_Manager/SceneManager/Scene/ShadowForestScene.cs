public class ShadowForestScene : BaseScene
{
    private void Start()
    {
        PoolManager.Instance?.UsePool(PoolType.HowlWindPool);
        PoolManager.Instance?.UsePool(PoolType.ParticleCryPool);
        PoolManager.Instance?.UsePool(PoolType.ParticleCryBindingPool);

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
                    skillController.BindInput(SkillType.Beam, InputType.Skill, InputMapName.Default, InputActionName.Skill_Beam);
                }

                CryBindingSkill cryBindingSkill = player.gameObject.AddComponent<CryBindingSkill>();
                if (skillController.TryAcquireSkill(SkillType.CryBinding, cryBindingSkill))
                {
                    skillController.BindInput(SkillType.CryBinding, InputType.Skill, InputMapName.Default, InputActionName.Skill_CryBinding);
                }
            }
        }

        SoundManager.Instance.PlayBgm(BgmName.ShadowForest, volume: 0.6f);
    }
}
