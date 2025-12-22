using UnityEngine;

public class Shadow : MonoBehaviour, IAttackable
{
    [Header("상태 관리")]
    [SerializeField] private Animator _animator;
    public ShadowStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new ShadowStateMachine(_animator);
    }

    public void Attack()
    {
    }

    public void GiveEffect()
    {
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        _animator = transform.FindChild<Animator>("Model");
    }
#endif
    #endregion
}
