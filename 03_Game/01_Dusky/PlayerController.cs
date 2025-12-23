using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")] 
    
    [Header("Ground 설정")] 
    [SerializeField] private float playerScale = 0.02f;
    [SerializeField] public float groundRayDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;

    private Vector3 _inputForward;
    
    
    public event Action<Vector3> OnInputMoveStartAction;
    public event Action<Vector3> OnInputMoveAction;
    public event Action<Vector3> OnInputMoveEndAction;
    public event Action OnInputAttackAction;
    public event Action OnInputJumpAction;
    

    private void Awake()
    {
        
    }
    
    private void Start()
    {
        InputManager.Instance.BindInputEvent(InputType.Player, InputMapName.Default, InputActionName.Jump, OnJump);
    }
    
    private void Update()
    {
        InputMove();
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_inputForward), Time.deltaTime * 10);
    }

    
    private void InputMove()
    {
        Vector2 inputValue = InputManager.Instance.GetAxis(InputType.Player, InputActionName.Move);
        
        Vector3 inputDir = new Vector3(inputValue.x, 0f, inputValue.y);
        
        // todo : CameraManager에서 현재 카메라의 Transform이나 forward를 받아와야 함. 
        Quaternion yawRotation = Quaternion.Euler(0f, CameraController.Instance.CamTransform.eulerAngles.y, 0f);    // 투영하던 건 쉽게 Quaternion으로 변경
        
        Vector3 moveDir = yawRotation * inputDir;
        
        if (moveDir.magnitude > 0.1f)
        {
            _inputForward = moveDir;
            // Vector3 newPosition = rb.position + forward * (moveStatusData.MoveSpeed * Time.deltaTime);
            // rb.MovePosition(newPosition);
        }
    }


    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (CanJump())
            {
                //jumpDelay = false;
                //StartCoroutine(JumpDelayRoutine());
                //rb.AddForce(Vector3.up * moveStatusData.JumpForce, ForceMode.Impulse);
            }
        }
    }
    
    public bool CanJump()
    {
        //if (jumpDelay == false) return false;
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

    // IEnumerator JumpDelayRoutine()
    // {
    //     yield return new WaitForSeconds(moveStatusData.JumpDelayTime);
    //     jumpDelay = true;
    // }

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

        Vector3 topStart = transform.position + (transform.up * 0.5f) + (_inputForward * 0.5f);
        Vector3 topEnd = topStart + (_inputForward * 0.1f);
        Vector3 bottomStart = transform.position + (_inputForward * 0.5f);
        Vector3 bottomEnd = bottomStart + (_inputForward * 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topStart, topEnd);
        Gizmos.DrawLine(bottomStart, bottomEnd);
    }
#endif
    
}