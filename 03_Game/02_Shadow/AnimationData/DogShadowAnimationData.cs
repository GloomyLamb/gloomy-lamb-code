using UnityEngine;

[System.Serializable]
public class DogShadowAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _chaseParameterName = "Chase";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _transformParameterName = "Transform";

    [SerializeField] private string _skillParameterName = "@Skill";
    [SerializeField] private string _biteParameterName = "Bite";
    [SerializeField] private string _barkParameterName = "Bark";

    [SerializeField] private string _battleParameterName = "@Battle";
    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";

    public int GroundParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int TransformParameterHash { get; private set; }

    public int SkillParameterHash { get; private set; }
    public int BiteParameterHash { get; private set; }
    public int BarkParameterHash { get; private set; }

    public int BattleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        TransformParameterHash = Animator.StringToHash(_transformParameterName);

        SkillParameterHash = Animator.StringToHash(_skillParameterName);
        BiteParameterHash = Animator.StringToHash(_biteParameterName);
        BarkParameterHash = Animator.StringToHash(_barkParameterName);

        BattleParameterHash = Animator.StringToHash(_battleParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
    }
}
