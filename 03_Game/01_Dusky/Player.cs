using System.Collections;
using System.Collections;
using UnityEngine;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour, IAttackable, IDamageable
{
    [Header("스탯 SO")]
    [SerializeField] protected StatusData statusData;
    [SerializeField] protected MoveStatusData moveStatusData;

    [Header("Ground 설정")]
    [SerializeField] private float playerScale = 0.02f;
    [SerializeField] public float groundRayDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("프리팹 설정")]
    [SerializeField] protected Animator animator;

    public Animator Animator => animator;

    public Status Status => status;
    protected Status status;

    // todo : 컨디션 관리하는 애 만들기
    public CharacterCondition NowCondition => nowCondition;
    protected CharacterCondition nowCondition = CharacterCondition.None;


    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        status = statusData?.GetNewStatus();
        Init();
    }

    protected abstract void Init();

    public abstract void OnMoveStart(Vector2 inputValue);
    public abstract void OnMoveEnd(Vector2 inputValue);
    public abstract void OnMove(Vector2 inputValue);
    public abstract void Move();
    public abstract void OnJump();
    public abstract void OnAttack();

    public virtual void OnDash(bool value)
    {
        if (value)
        {
            AddCondition(CharacterCondition.Dash, true);
        }
        else
        {
            AddCondition(CharacterCondition.Dash, false);
        }
    }

    public abstract void Attack();
    public abstract void GiveEffect();

    public virtual void Damage(float damage)
    {
        status.AddHp(-damage);
        StartCoroutine(AddConditionRoutine(CharacterCondition.Invincible,status.InvincibleTime));
    }

    public abstract void ApplyEffect();

    public void AddCondition(CharacterCondition condition)
    {
        nowCondition |= condition;   
    }

    public void RemoveCondition(CharacterCondition condition)
    {
        nowCondition &= ~condition;
    }


    Coroutine slowDownRoutine;
    Coroutine stunRoutine;

    // todo : 버프 디버프 받는걸로 변경해야함. 구조는 나중에! 중간발표대비 임시구현
    public virtual void TakeSlowDown()
    {
        if (slowDownRoutine != null)
            StopCoroutine(slowDownRoutine);
        slowDownRoutine = StartCoroutine(AddConditionRoutine(CharacterCondition.Slow, 15f));
    }

    public virtual void TakeStun()
    {
        if (stunRoutine != null)
            StopCoroutine(stunRoutine);
        stunRoutine = StartCoroutine(AddConditionRoutine(CharacterCondition.Stun, 3f));
    }

    IEnumerator AddConditionRoutine(CharacterCondition condition, float duration)
    {
        nowCondition |= condition;
        yield return new WaitForSeconds(duration);
        nowCondition &= ~condition;
    }

    public void AddCondition(CharacterCondition condition, bool value)
    {
        if (value)
        {
            nowCondition |= condition;
        }
        else
        {
            nowCondition &= ~condition;
        }
    }

    #region Ground Check

    protected bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * playerScale) + (transform.up * 0.05f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], groundRayDistance, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + (transform.forward * playerScale),
            transform.position + (transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.forward * playerScale),
            transform.position + (-transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (transform.right * playerScale),
            transform.position + (transform.right * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.right * playerScale),
            transform.position + (-transform.right * playerScale) + (-transform.up * groundRayDistance));
    }
#endif

    #endregion
}