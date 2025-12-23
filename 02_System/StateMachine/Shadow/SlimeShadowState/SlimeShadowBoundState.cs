using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeShadowBoundState : SlimeShadowAttackState
{
    private float _stopPoint = 0.1f;
    private float _timer;
    private bool _done = false;

    private Coroutine _coroutine;

    public SlimeShadowBoundState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.HitParameterHash);
    }

    public override void Exit()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.HitParameterHash);
        _done = false;
    }

    public override void Update()
    {
        base.Update();

        if (_timer < _stopPoint)
        {
            _timer += Time.deltaTime;
            return;
        }

        if (!_done)
        {
            StateMachine.StopAnimator();

            if (_coroutine != null)
            {
                Unity.VisualScripting.CoroutineRunner.instance.StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _coroutine = Unity.VisualScripting.CoroutineRunner.instance.StartCoroutine(Binding());

            _done = true;
        }
    }

    private IEnumerator Binding()
    {
        yield return new WaitForSeconds(3f);
        StateMachine.StartAnimator();
        StateMachine.ChangeState(StateMachine.IdleState);
    }
}
