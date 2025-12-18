using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [Header("이동 설정")] 
    [SerializeField] MoveStatusData moveStatusData;
    
    [Header("Ground 설정")] 
    [SerializeField] private float playerScale = 0.02f;
    [SerializeField] public float groundRayDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;

    // 상태
    private bool jumpDelay = true;
    
    // 캐릭터 방향
    public Vector3 Forward => forward;
    protected Vector3 forward;
    
    // 필요 컴포넌트
    private Transform cam;
    Rigidbody rb;

    private void Awake()
    {
        forward = transform.forward;
        rb = this.GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }
    
    private void Start()
    {
        InputManager.Instance.BindInputEvent(InputType.Player, InputMapName.Default, InputActionName.Jump, OnJump);
        
    }
    
    private void Update()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 10);
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        Vector2 inputDir = InputManager.Instance.GetAxis(InputType.Player, InputActionName.Move);
        
        Vector3 camForwardFlat = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, camForwardFlat).normalized;
        Vector3 moveDir = (camForwardFlat * inputDir.y + right * inputDir.x).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            forward = moveDir;
            Vector3 newPosition = rb.position + forward * (moveStatusData.MoveSpeed * Time.deltaTime);

            rb.MovePosition(newPosition);
        }
    }


    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (CanJump())
            {
                jumpDelay = false;
                StartCoroutine(JumpDelayRoutine());
                rb.AddForce(Vector3.up * moveStatusData.JumpForce, ForceMode.Impulse);
            }
        }
    }
    
    public bool CanJump()
    {
        if (jumpDelay == false) return false;
        if (IsGrounded() == false) return false;
        return true;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right * playerScale) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * playerScale) + (transform.up * 0.05f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], groundRayDistance, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator JumpDelayRoutine()
    {
        yield return new WaitForSeconds(moveStatusData.JumpDelayTime);
        jumpDelay = true;
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + (transform.forward * playerScale),
            transform.position + (transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.forward * playerScale),
            transform.position + (-transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (transform.right * playerScale),
            transform.position + (transform.right * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.right * playerScale),
            transform.position + (-transform.right * playerScale) + (-transform.up * groundRayDistance));

        Vector3 topStart = transform.position + (transform.up * 0.5f) + (forward * 0.5f);
        Vector3 topEnd = topStart + (forward * 0.1f);
        Vector3 bottomStart = transform.position + (forward * 0.5f);
        Vector3 bottomEnd = bottomStart + (forward * 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topStart, topEnd);
        Gizmos.DrawLine(bottomStart, bottomEnd);
    }
#endif
    
}