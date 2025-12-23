using UnityEngine;

[System.Serializable]
public class IdleShadowAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _chaseParameterName = "Chase";
    [SerializeField] private string _transformParameterName = "Transform";

    [SerializeField] private string _battleParameterName = "@Battle";
    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int TransformParameterHash { get; private set; }

    public int BattleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
        TransformParameterHash = Animator.StringToHash(_transformParameterName);

        BattleParameterHash = Animator.StringToHash(_battleParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
    }
}
