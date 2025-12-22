using UnityEngine;


public class StatusData : ScriptableObject
{
    [SerializeField] private int _maxHp;

    public Status GetNewStatus()
    {
        // 지금은 hp만 있다
        return  new Status(_maxHp);
    }
}