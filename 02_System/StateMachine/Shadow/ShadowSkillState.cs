using UnityEngine;

/// <summary>
/// 그림자 상태 - 스킬
/// </summary>
public class ShadowSkillState : ShadowState, IBindableState
{
    protected int skillParameterHash = Animator.StringToHash("@Skill");

    public ShadowSkillState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        shadow.Animator.SetTrigger(skillParameterHash);
    }
}
