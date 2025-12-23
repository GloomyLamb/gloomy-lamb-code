using UnityEngine;

public class Status
{
    public float MaxHp => _maxHp;
    private float _maxHp;

    public float Hp => _hp;
    private float _hp;

    public Status(float maxHp)
    {
        _maxHp = maxHp;
        _hp = maxHp;
    }


    public float AddHp(float hp)
    {
        _hp += hp;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        return _hp;
    }
}