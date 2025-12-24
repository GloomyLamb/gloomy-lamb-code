using UnityEngine;

[System.Serializable]
public class DogShadowAnimationData
{
    [SerializeField] private string _skillParameterName = "@Skill";
    [SerializeField] private string _biteParameterName = "Bite";
    [SerializeField] private string _barkParameterName = "Bark";

    public int SkillParameterHash { get; private set; }
    public int BiteParameterHash { get; private set; }
    public int BarkParameterHash { get; private set; }

    public void Initialize()
    {
        SkillParameterHash = Animator.StringToHash(_skillParameterName);
        BiteParameterHash = Animator.StringToHash(_biteParameterName);
        BarkParameterHash = Animator.StringToHash(_barkParameterName);
    }
}
