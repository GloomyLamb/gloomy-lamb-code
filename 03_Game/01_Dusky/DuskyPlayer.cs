using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskyPlayer : Player
{
    [SerializeField] DamageableDetector _damageableDetector;

    public DuskyStateMachine StateMachine => stateMachine;
    protected DuskyStateMachine stateMachine;

    // 필요 컴포넌트
    Rigidbody rb;

    private Vector3 _lastMoveInputValue;

    private bool _jumpDelay = false;
    WaitForSeconds _jumpDelayWait = new WaitForSeconds(0.2f);

    protected override void Init()
    {
        stateMachine = new DuskyStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.OnInputMoveStartAction += OnMoveStart;
            controller.OnInputMoveAction += OnMove;
            controller.OnInputMoveEndAction += OnMoveEnd;
            controller.OnInputJumpAction += OnJump;
            controller.OnInputAttackAction += OnAttack;
            controller.OnInputDashAction += OnDash;
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

        // 현재 Flag 에 따라 방향 바뀌는게 달라야 함. (빔쏘는 상태일 때 체크 필요)
        if (_lastMoveInputValue != Vector3.zero)
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lastMoveInputValue), Time.deltaTime * 10);

        // 바닥일 때 Idle 로 바꿔주기
        if (stateMachine.CurState == stateMachine.JumpState && _jumpDelay == false)
        {
            if (IsGrounded())
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
        Move();
    }

    public override void Move()
    {
        if (nowCondition.HasFlag(CharacterCondition.Stun))
        {
            return;
        }

        // Flags 검사 추가해야함. (스턴)
        if (stateMachine.CurState is IMovableState)
        {
            if (_lastMoveInputValue.magnitude > 0.1f)
            {
                float moveSpeed = moveStatusData.MoveSpeed;
                
                if (nowCondition.HasFlag(CharacterCondition.Slow))
                    moveSpeed = moveSpeed * 0.3f; // todo : 수치 빼는건 나중에
                if (nowCondition.HasFlag(CharacterCondition.Dash))
                    moveSpeed = moveSpeed * 1.5f;

                Vector3 newPosition = rb.position + _lastMoveInputValue * (moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
            }
        }
    }

    public override void Attack()
    {
        if (_damageableDetector.CurrentTarget != null)
        {
            // 점프 중인지 체크해야함 
            _damageableDetector.CurrentTarget.Damage(status.Atk);
        }
    }

    public override void GiveEffect()
    {
    }

    public override void ApplyEffect()
    {
    }

    public override void OnMoveStart(Vector2 inputValue)
    {
        // if (stateMachine.CanChange(stateMachine.MoveState))
        // {
        //     stateMachine.ChangeState(stateMachine.MoveState);
        // }
    }

    public override void OnMoveEnd(Vector2 inputValue)
    {
        if (stateMachine.CanChange(stateMachine.IdleState))
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void OnMove(Vector2 inputValue)
    {
        if (inputValue.magnitude < 0.1f)
        {
            _lastMoveInputValue = Vector2.zero;
            return;
        }

        if (stateMachine.CurState != stateMachine.MoveState &&
            NowCondition.HasFlag(CharacterCondition.Dash) == false &&
            nowCondition.HasFlag(CharacterCondition.Stun) == false)
        {
            if (stateMachine.CanChange(stateMachine.MoveState))
            {
                stateMachine.ChangeState(stateMachine.MoveState);
            }
        }
        else if (stateMachine.CurState != stateMachine.DashState)
        {
            if (NowCondition.HasFlag(CharacterCondition.Dash))
            {
                stateMachine.ChangeState(stateMachine.DashState);
            }
        }

        Quaternion yawRotation = Quaternion.identity;

        Vector3 inputDir = new Vector3(inputValue.x, 0f, inputValue.y);
        if (CameraController.Instance != null)
            yawRotation = Quaternion.Euler(0f, CameraController.Instance.CamTransform.eulerAngles.y, 0f); // 투영하던 건 쉽게 Quaternion으로 변경
        else
            yawRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);

        Vector3 moveDir = yawRotation * inputDir;

        _lastMoveInputValue = moveDir;
    }

    public override void OnJump()
    {
        if (nowCondition.HasFlag(CharacterCondition.Stun)) return;
        if (IsGrounded() == false) return;

        if (stateMachine.CanChange(stateMachine.JumpState))
        {
            stateMachine.ChangeState(stateMachine.JumpState);
            rb.AddForce(Vector3.up * moveStatusData.JumpForce, ForceMode.Impulse);

            StartCoroutine(JumpDelaRotuine());
        }
    }

    public override void OnAttack()
    {
        if (stateMachine.CanChange(stateMachine.AttackState))
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        stateMachine.ChangeState(stateMachine.HitState);
    }

    IEnumerator JumpDelaRotuine()
    {
        _jumpDelay = true;
        yield return _jumpDelayWait;
        _jumpDelay = false;
    }

    public override void TakeStun()
    {
        base.TakeStun();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}