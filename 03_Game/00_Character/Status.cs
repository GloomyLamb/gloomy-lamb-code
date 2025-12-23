using UnityEngine;

public class Status
{
    public int MaxHp => _maxHp;
    private int _maxHp;

    public int Hp => _hp;
    private int _hp;

    public Status(int maxHp)
    {
        _maxHp = maxHp;
        _hp = maxHp;
    }


    public int AddHp(int hp)
    {
        _hp += hp;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        return _hp;
    }
}