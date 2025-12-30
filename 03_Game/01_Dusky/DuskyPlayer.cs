using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DuskyPlayer : Player
{
    [SerializeField] DamageableDetector _damageableDetector;

    public DuskyStateMachine StateMachine => stateMachine;
    protected DuskyStateMachine stateMachine;
    private float jumpAttackMultiplier = 2f;
    private bool jumpingCheck;
    // 필요 컴포넌트
    Rigidbody rb;

    private Vector3 _lastMoveInputValue;

    private bool _jumpDelay = false;
    WaitForSeconds _jumpDelayWait = new WaitForSeconds(0.2f);


    [SerializeField] private LayerMask wallLayerMask;

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

        if (NowCondition.HasFlag(CharacterCondition.Beam))
        {
            // 이 if문 안에서 아무것도 안하면 쏘기 시작했을 때부터 고정
            Quaternion yawRotation = Quaternion.identity;
            if (CameraController.Instance != null)
                yawRotation = Quaternion.Euler(0f, CameraController.Instance.CamTransform.eulerAngles.y, 0f);
            else
                yawRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, yawRotation, Time.deltaTime * 10);
        }
        else if (_lastMoveInputValue != Vector3.zero)
        {
            if (stateMachine.CurState is IMovableState)
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_lastMoveInputValue), Time.deltaTime * 10);
        }

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
        CustomGravity();
    }

    public override void Move()
    {
        if (nowCondition.HasFlag(CharacterCondition.Stun))
        {
            return;
        }

        if (stateMachine.CurState is IMovableState)
        {
            if (_lastMoveInputValue.magnitude > 0.1f)
            {
                float moveSpeed = moveStatusData.MoveSpeed;

                if (nowCondition.HasFlag(CharacterCondition.Slow))
                    moveSpeed = moveSpeed * 0.3f; // todo : 수치 빼는건 나중에
                if (nowCondition.HasFlag(CharacterCondition.Dash) && IsGrounded())
                    moveSpeed = moveSpeed * moveStatusData.DashMultiplier;

                Vector3 newPosition = rb.position + _lastMoveInputValue * (moveSpeed * Time.fixedDeltaTime);

                if (AreaLimit.Instance != null)
                {
                    newPosition = AreaLimit.Instance.GetNextPosition(this.transform.position, newPosition);
                }
                
                rb.MovePosition(newPosition);
            }
        }
    }

    void CustomGravity()
    {
        if (rb.velocity.y < 0f)
            rb.velocity += Vector3.up * Physics.gravity.y * (moveStatusData.GravityMultiflier - 1) * Time.deltaTime;
    }

    public override void Attack()
    {
        if (_damageableDetector.CurrentTarget != null)
        {
            float damage = status.Atk;

            if (jumpingCheck)
            {
                damage *= jumpAttackMultiplier;
            }

            _damageableDetector.CurrentTarget.Damage(damage);
            jumpingCheck = false;
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
        if (NowCondition.HasFlag(CharacterCondition.Stun)) return;

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

        if (nowCondition.HasFlag(CharacterCondition.Stun))
            return;


        if (NowCondition.HasFlag(CharacterCondition.Dash) == false &&
            stateMachine.CurState != stateMachine.MoveState)
        {
            if (stateMachine.CanChange(stateMachine.MoveState))
            {
                stateMachine.ChangeState(stateMachine.MoveState);
            }
        }
        else if (stateMachine.CurState != stateMachine.DashState)
        {
            if (NowCondition.HasFlag(CharacterCondition.Dash) &&
                stateMachine.CanChange(stateMachine.DashState))
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

            rb.velocity = new Vector3(rb.velocity.x, moveStatusData.JumpForce, rb.velocity.z);
            //rb.AddForce(Vector3.up * moveStatusData.JumpForce, ForceMode.Impulse);

            StartCoroutine(JumpDelaRotuine());
        }
    }

    public override void OnAttack()
    {
        jumpingCheck = !IsGrounded() || (stateMachine.CurState == stateMachine.JumpState);
        if (stateMachine.CanChange(stateMachine.AttackState))
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    public override void Damage(float damage)
    {
        if (nowCondition.HasFlag(CharacterCondition.Invincible)) return;

        stateMachine.ChangeState(stateMachine.HitState);
        base.Damage(damage);
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
        stateMachine.ChangeState(stateMachine.LieState);
    }
}