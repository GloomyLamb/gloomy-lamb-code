using UnityEngine;

[System.Serializable]
public class ShadowAnimationData
{
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _chaseParameterName = "Chase";
    [SerializeField] private string _transformParameterName = "Transform";

    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";
    [SerializeField] private string _boundParameterName = "Bound";

    public int IdleParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int TransformParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int BoundParameterHash { get; private set; }

    public virtual void Initialize()
    {
        Logger.Log("기본 애니메이션 데이터 초기화");

        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
        TransformParameterHash = Animator.StringToHash(_transformParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
        BoundParameterHash = Animator.StringToHash(_boundParameterName);
    }
}
