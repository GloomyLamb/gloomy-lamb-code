public class TestSkill : BaseSkill
{
    public override void Init(SkillStatusData data)
    {
        base.Init(data);
        Logger.Log("테스트 스킬 데이터 연결");
    }

    public override void GiveEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override bool HasEnoughResource()
    {
        throw new System.NotImplementedException();
    }
}
