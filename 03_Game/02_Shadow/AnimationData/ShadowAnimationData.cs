using UnityEngine;

[System.Serializable]
public class ShadowAnimationData
{
    [SerializeField] private string _idleParameterName = "Idle";

    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";

    public int IdleParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public virtual void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(_idleParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
    }
}
