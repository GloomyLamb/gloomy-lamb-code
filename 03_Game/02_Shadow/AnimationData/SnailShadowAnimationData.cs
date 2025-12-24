using UnityEngine;

[System.Serializable]
public class SnailShadowAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _chaseParameterName = "Chase";

    public int GroundParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
    }
}
