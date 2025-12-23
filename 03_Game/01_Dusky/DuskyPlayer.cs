using System.Collections.Generic;
using UnityEngine;

public class DuskyPlayer : Player
{
    [SerializeField] protected List<Transform> _skillPivot;

    public DuskyStateMachine StateMachine => stateMachine;
    protected DuskyStateMachine stateMachine;

    // 필요 컴포넌트
    Rigidbody rb;

    protected override void Init()
    {
        stateMachine = new DuskyStateMachine(this);

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.OnInputMoveStartAction += OnMoveStart;
            controller.OnInputMoveEndAction += OnMoveEnd;
            controller.OnInputJumpAction += OnJump;
            controller.OnInputAttackAction += OnAttack;
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Player);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public override void Move()
    {
        Vector3 newPosition = rb.position + forward * (moveStatusData.MoveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    public override void Jump()
    {
    }

    public override void Attack()
    {
    }

    public override void GiveEffect()
    {
    }

    public override void Damage(float damage)
    {
    }

    public override void ApplyEffect()
    {
    }

    public override void OnMoveStart(Vector3 inputDir)
    {
        if (stateMachine.CurState is IMovableState)
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }

    public override void OnMoveEnd(Vector3 inputDir)
    {
        if (stateMachine.CurState == stateMachine.MoveState)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void OnMove(Vector3 inputDir)
    {
        if (stateMachine.CurState is DuskyMoveState ||
            stateMachine.CurState is DuskyJumpState)
        {

        }
    }



    public override void OnJump()
    {
        // if (stateMachine.CurState.)
        // {
        //     stateMachine.ChangeState(stateMachine.JumpState);
        // }
    }

    public override void OnAttack()
    {
        // if (stateMachine.CurState is IAttackableState)
        // {
        //     stateMachine.ChangeState(stateMachine.AttackState);
        // }
    }

    public override void OnLanding()
    {
        //if()
    }
}