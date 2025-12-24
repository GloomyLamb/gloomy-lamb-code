using UnityEngine;

public class ShadowHitState : CommonShadowState
{
    private float _timer;

    public ShadowHitState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        shadow.Animator.SetTrigger(shadow.AnimationData.HitParameterHash);
        _timer = 0f;
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > shadow.HitDuration)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
