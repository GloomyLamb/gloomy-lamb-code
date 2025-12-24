using UnityEngine;

public class SlimeShadowExpandState : SlimeShadowChaseState
{
    private bool _isExpanded = false;

    private Coroutine _coroutine;

    public SlimeShadowExpandState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.MovementSpeedModitier = 2f;
        StateMachine.Shadow.CheckExpand();
        base.Enter();

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (!_isExpanded)
        {
            _coroutine = CustomCoroutineRunner.Instance.StartCoroutine(ScaleUp(2f, 0.5f));
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
            StateMachine.ChangeState(StateMachine.ReduceState);
        }
    }
}
