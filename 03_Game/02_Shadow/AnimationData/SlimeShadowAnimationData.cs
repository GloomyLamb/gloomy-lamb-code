using UnityEngine;

[System.Serializable]
public class SlimeShadowAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _chaseParameterName = "Chase";
    [SerializeField] private string _transformParameterName = "Transform";

    public int GroundParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int TransformParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
        TransformParameterHash = Animator.StringToHash(_transformParameterName);
    }
}
