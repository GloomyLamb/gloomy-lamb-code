using System.Collections;
using UnityEngine;

public class ShadowBoundState : CommonShadowState
{
    private Coroutine _boundCoroutine;

    public ShadowBoundState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        shadow.Animator.SetTrigger(shadow.AnimationData.HitParameterHash);

        if (_boundCoroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_boundCoroutine);
            _boundCoroutine = null;
        }
        _boundCoroutine = CustomCoroutineRunner.Instance.StartCoroutine(Binding());
    }

    private IEnumerator Binding()
    {
        yield return shadow.BoundStopPoint;
        shadow.Animator.speed = 0f;
        yield return shadow.BoundDuration;
        shadow.Animator.speed = 1f;
        StateMachine.ChangeState(StateMachine.IdleState);
    }
}
