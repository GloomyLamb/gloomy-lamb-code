using UnityEngine;

[System.Serializable]
public class ShadowAnimationData
{
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _changeParameterName = "Change";

    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";

    public int IdleParameterHash { get; private set; }
    public int ChangeParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public virtual void Initialize()
    {
        Logger.Log("기본 애니메이션 데이터 초기화");

        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        ChangeParameterHash = Animator.StringToHash(_changeParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
    }
}
