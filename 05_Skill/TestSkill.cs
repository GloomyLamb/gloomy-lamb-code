public class TestSkill : BaseSkill
{
    public void Start()
    {
        target = FindObjectOfType<TestShadow>();
    }

    public override void Init(SkillStatusData data)
    {
        base.Init(data);
        Logger.Log("테스트 스킬 데이터 연결");
    }

    public override void GiveEffect()
    {
        Logger.Log("테스트 스킬 효과");
    }

    protected override bool HasEnoughResource()
    {
        Logger.Log("테스트 스킬 조건 만족");
        return true;
    }
}
