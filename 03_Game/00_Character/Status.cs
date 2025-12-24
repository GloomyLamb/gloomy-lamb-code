using UnityEngine;

public class Status
{
    public float MaxHp => _maxHp;
    private float _maxHp;

    public float Hp => _hp;
    private float _hp;

    public float Atk => _atk;
    private float _atk;
    
    public Status(float maxHp, float atk)
    {
        _maxHp = maxHp;
        _hp = maxHp;
        _atk = atk;
    }


    public float AddHp(float hp)
    {
        _hp += hp;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        return _hp;
    }
}