using UnityEngine;


[CreateAssetMenu(fileName = "New StatusData", menuName = "SO/Character Status Data")]
public class StatusData : ScriptableObject
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _atk = 50;

    public Status GetNewStatus()
    {
        // 지금은 hp만 있다
        return new Status(_maxHp,_atk);
    }
}