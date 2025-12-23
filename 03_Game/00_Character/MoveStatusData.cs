using UnityEngine;

[CreateAssetMenu(fileName = "New MoveStatusData", menuName = "SO/Move Status Data")]
public class MoveStatusData : ScriptableObject
{
    public float MoveSpeed => moveSpeed;
    public float DashSpeed => dashSpeed;
    public float JumpForce => jumpForce;
    public float JumpDelayTime => jumpDelayTime;
    
    
    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 7.5f;  // 지정 속도로 하는게 편하겠죠?
    
    [Header("점프 설정")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpDelayTime = 0.2f;
    
    
}