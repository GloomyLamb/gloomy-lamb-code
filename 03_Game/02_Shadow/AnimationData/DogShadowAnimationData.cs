using UnityEngine;

[System.Serializable]
public class DogShadowAnimationData
{
    [SerializeField] private string _biteParameterName = "Bite";
    [SerializeField] private string _backwardParameterName = "Backward";
    [SerializeField] private string _barkParameterName = "Bark";

    public int BiteParameterHash { get; private set; }
    public int BackwardParameterHash { get; private set; }
    public int BarkParameterHash { get; private set; }

    public void Initialize()
    {
        BiteParameterHash = Animator.StringToHash(_biteParameterName);
        BackwardParameterHash = Animator.StringToHash(_backwardParameterName);
        BarkParameterHash = Animator.StringToHash(_barkParameterName);
    }
}
