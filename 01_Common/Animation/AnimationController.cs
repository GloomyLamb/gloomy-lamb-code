using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AnimationController : MonoBehaviour
{
    private Animator animator;


    //private Player player;
    // private float moveSpeed;

    public void Awake()
    {
        Logger.Log(AnimatorParameters.Attack.ToString());
        animator = GetComponent<Animator>();
        
        // 이전에 쓰던 코드. 필요한 타이밍에 넣어놓고 썼었음
        // player.OnMoveAction += OnMove;
        // player.OnIdleAction += OnIdle;
        // player.OnJumpAction += OnJump;
        // player.OnInteractAction += OnInteract;
        // player.OnTryAttackAction += OnTryAttackAction;
        // player.OnHitAction += OnHit;
        // player.OnLandingAction += OnLanding;
        
    }

    private void Update()
    {
        // 이전에 쓰던 코드! 애니메이션 걷는 속도구현 시 참고
        // if ( player.IsMoving )
        // {
        //     
        //     float walkSpeed =  player.Status.MoveSpeed;
        //     float runSpeed  = walkSpeed * player.Status.DashMultiplier; // 예: 1.5f
        //     float blend = Mathf.InverseLerp(walkSpeed, runSpeed, player.NowMoveSpeed);
        //     
        //     nowSpeed = Mathf.Lerp( nowSpeed, blend, Time.deltaTime * 2.5f );
        //     animator.SetFloat( "MoveSpeed", nowSpeed );
        // }
        // else
        // {
        //     nowSpeed = 0;
        // }
    }

    void OnMove()
    {
        animator.SetBool(AnimatorParameters.IsMove, true);
    }

    void OnIdle()
    {
        animator.SetBool(AnimatorParameters.IsMove, false);
    }

    void OnJump()
    {
        animator.SetTrigger(AnimatorParameters.Jump);
        animator.SetBool(AnimatorParameters.IsJumping, true);
    }

    void OnTryAttackAction()
    {
        animator.SetTrigger(AnimatorParameters.Attack);
    }

    void OnHit()
    {
        animator.SetTrigger(AnimatorParameters.Hit);
    }

    void OnLanding()
    {
        animator.SetBool(AnimatorParameters.IsJumping, false);
    }
}