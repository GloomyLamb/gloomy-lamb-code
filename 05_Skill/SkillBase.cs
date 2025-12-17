using UnityEngine;

/// <summary>
/// 스킬 기본 클래스
/// </summary>
public abstract class SkillBase : MonoBehaviour, IAttackable
{
    // todo: 스킬 중 묶을 수 있는 부분이 있으면 묶기

    protected virtual void Update()
    {

    }

    public abstract void GiveEffect();

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
