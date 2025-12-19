using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    // todo : 카메라 매니저와 논의 필요
    // 임시 public! 나중에는 SetTarget 으로 타겟팅해주기
    public Transform target;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputAction;

    [Header("설정")]
    [SerializeField] bool useRightClick = false;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Transform pivot;
    [SerializeField] private float lookSensitivity = 20;
    [SerializeField] private float limitMinX = 0;
    [SerializeField] private float limitMaxX = 30;

    private float camCurRotX;
    private float camCurRotY;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        camCurRotX = transform.eulerAngles.x;
        camCurRotY = transform.eulerAngles.y;

        // todo : CameraManager 에 등록하기
    }

    public void UseInput()
    {
        // todo : CameraManager 한테 쓴다고 쪼르기
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        camCurRotY = target.eulerAngles.y;
    }

    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Camera);
        //InputManager.Instance.HideCursor();
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        pivot.position = target.position;
        Vector2 axis = InputManager.Instance.GetAxis(InputType.Camera, InputActionName.Look);

        camCurRotX += (-axis.y * Time.deltaTime * lookSensitivity);
        camCurRotX = Mathf.Clamp(camCurRotX, limitMinX, limitMaxX);
        camCurRotY += (axis.x * Time.deltaTime * lookSensitivity);

        pivot.rotation = Quaternion.Euler(camCurRotX, camCurRotY, 0);
    }
}