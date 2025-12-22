
public class Status
{
    public int MaxHp=>_maxHp;
    private int _maxHp;
    
    public int Hp => _hp;
    private int _hp;
    
    public Status(int maxHp)
    {
        _maxHp = maxHp;
        _hp = maxHp;
    }
}
