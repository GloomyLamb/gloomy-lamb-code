using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayer : Player
{
    // 설정
    // 필요할 때 SO로 나누기
    [Header("플레이어 이동 설정")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    [Header("Ground 설정")]
    [SerializeField] private float playerScale = 0.02f;
    [SerializeField] public float groundRayDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;
    
    // 상태
    private bool jumpDelay = true;
    
    // 필요 컴포넌트
    private Transform cam;
    Rigidbody rb;

    protected  override void Init()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        
        input.BindInputEvent(InputMapName.Default,InputActionName.Jump,OnJump);
    }

    private void Start()
    {
        InputManager.Instance.UseInput(input);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputDir = input.GetAxis(InputMapName.Default,InputActionName.Move);
    
        Vector3 camForwardFlat = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, camForwardFlat).normalized;
        Vector3 moveDir = (camForwardFlat * inputDir.y + right * inputDir.x).normalized;
        forward = moveDir;
        
        if (moveDir.magnitude > 0.1f)
        {
            Vector3 newPosition = rb.position + forward * (moveSpeed * Time.deltaTime);

            rb.MovePosition(newPosition); 
            rb.MoveRotation(Quaternion.LookRotation(forward, Vector3.up));
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
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
        yield return new WaitForSeconds(0.2f);
        jumpDelay = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + (transform.forward * playerScale), transform.position + (transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.forward * playerScale), transform.position + (-transform.forward * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (transform.right * playerScale), transform.position + (transform.right * playerScale) + (-transform.up * groundRayDistance));
        Gizmos.DrawLine(transform.position + (-transform.right * playerScale), transform.position + (-transform.right * playerScale) + (-transform.up * groundRayDistance));

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
