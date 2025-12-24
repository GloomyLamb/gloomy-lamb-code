using UnityEngine;

public class SlimeShadowExpandState : SlimeShadowChaseState
{
    private bool _isExpanded = false;

    private Coroutine _coroutine;

    public SlimeShadowExpandState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Run);
        shadow.CheckExpand();
        base.Enter();

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (!_isExpanded)
        {
            _coroutine = CustomCoroutineRunner
                .Instance
                .StartCoroutine(ScaleUp(shadow.MaxScale, 0.5f));
            _isExpanded = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // todo: 빛을 맞았을 때 reduce
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Logger.Log("축소 패턴 진입");
            StateMachine.ChangeState(stateMachine.ReduceState);
        }
    }
}
