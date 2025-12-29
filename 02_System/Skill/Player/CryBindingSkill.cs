using UnityEngine;

public class CryBindingSkill : BaseSkill
{
    // todo : 프로퍼티 new
    CryBindingSkillData cryBindingSkillData;
    
    
    
    public override void Init(SkillData data)
    {
        base.Init(data);
        cryBindingSkillData = data as CryBindingSkillData;

    }

    protected override void UseSkill()
    {
        // 일단 찾아
        if (IsUsable)
        {  
            SoundManager.Instance?.PlaySfxOnce(SfxName.DuskyCry);
            PoolManager.Instance?.Spawn(PoolType.ParticleCryPool,this.transform.position,this.transform.rotation);
            
            
            Collider[] cols = Physics.OverlapSphere(this.transform.position, cryBindingSkillData.AttackRange.Radius);

            foreach (Collider col in cols)
            {
                Shadow shadow = col.GetComponent<Shadow>();
                if (shadow != null)
                {
                    shadow.Bound();
                }
            }
        }
        
        base.UseSkill();
    }


    protected override bool HasEnoughResource()
    {
        return true;
    }
}