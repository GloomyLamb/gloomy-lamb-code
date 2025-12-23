using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuskyPlayer : Player
{
    [SerializeField] protected List<Transform> _skillPivot;

    public DuskyStateMachine StateMachine => stateMachine;
    protected DuskyStateMachine stateMachine;


    protected override void Init()
    {
        stateMachine = new DuskyStateMachine(animator, this);

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.OnInputMoveStartAction += OnMoveStart;
            controller.OnInputMoveEndAction += OnMoveEnd;
            controller.OnInputJumpAction += OnJump;
            controller.OnInputAttackAction += OnAttack;    
        }
        
    }

    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Player);
    }

    private void Update()
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

    public override void OnMoveStart()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
    }

    public override void OnMoveEnd()
    {
        if (stateMachine.CurState == stateMachine.MoveState)
            stateMachine.ChangeState(stateMachine.IdleState);
    }

    public override void OnJump()
    {
    }

    public override void OnAttack()
    {
    }

    public override void OnLanding()
    {
        
    }
}