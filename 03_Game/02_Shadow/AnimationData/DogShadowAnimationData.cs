using UnityEngine;

[System.Serializable]
public class DogShadowAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _chaseParameterName = "Chase";
    [SerializeField] private string _transformParameterName = "Transform";

    [SerializeField] private string _skillParameterName = "@Skill";
    [SerializeField] private string _biteParameterName = "Bite";
    [SerializeField] private string _barkParameterName = "Bark";

    public int GroundParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int TransformParameterHash { get; private set; }

    public int SkillParameterHash { get; private set; }
    public int BiteParameterHash { get; private set; }
    public int BarkParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        ChaseParameterHash = Animator.StringToHash(_chaseParameterName);
        TransformParameterHash = Animator.StringToHash(_transformParameterName);

        SkillParameterHash = Animator.StringToHash(_skillParameterName);
        BiteParameterHash = Animator.StringToHash(_biteParameterName);
        BarkParameterHash = Animator.StringToHash(_barkParameterName);
    }
}
