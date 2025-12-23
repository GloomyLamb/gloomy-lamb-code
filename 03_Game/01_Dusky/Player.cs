using System;
using System.Collections.Generic;
using UnityEngine;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour, IAttackable, IDamageable
{
    [Header("스탯 SO")] [SerializeField] protected StatusData statusData;
    [SerializeField] protected MoveStatusData moveStatusData;

    [Header("프리팹 설정")] [SerializeField] protected Animator animator;

    public Status Status => status;
    protected Status status;

    // 캐릭터 방향
    public Vector3 Forward => forward;
    protected Vector3 forward;


    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        status = statusData?.GetNewStatus();
        forward = transform.forward;

        Init();
    }

    protected abstract void Init();

    public abstract void OnMoveStart(Vector3 inputDir);
    public abstract void OnMoveEnd(Vector3 inputDir);
    public abstract void OnMove(Vector3 inputDir);
    public abstract void OnJump();
    public abstract void OnAttack();
    public abstract void OnLanding();

    public abstract void Move();
    public abstract void Jump();

    public abstract void Attack();
    public abstract void GiveEffect();
    public abstract void Damage(float damage);
    public abstract void ApplyEffect();
}