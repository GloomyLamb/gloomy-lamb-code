using System;
using System.Diagnostics;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public enum CameraControlType
{
    TpsFollow,
    TpsAutoRotateFollow,
    TpsRotatableFollow,
    TpsFixed,
    TpsRotableFixed,
    FirstPerson,
}

// todo : 카메라 매니저의 카메라를 채가야 할 듯 함...
// 카메라 메니저에 자식으로 붙일까? 카메라 매니저에서 Forward 데리고 올 수 있도록해도 좋을 것 같다..?
// 아니면 얘를 따로 등록해야하나...?
public class CameraController : MonoBehaviour
{
    public static CameraController Instance => instance;
    private static CameraController instance;

    [Header("조작 설정")]
    [SerializeField] private CameraControlType cameraControlType = CameraControlType.TpsAutoRotateFollow;

    [Header("카메라 조작을 위한 pivot")]
    [SerializeField] private Transform rotPivot;

    [Header("pivot이 따라다닐 target")]
    public Transform target;

    [Header("회전 설정")]
    [SerializeField] bool useRightClick = false;

    [SerializeField] private float lookSensitivity = 20;
    [SerializeField] private float limitMinX = 0;
    [SerializeField] private float limitMaxX = 90;

    [Header("각도에 따른 카메라 높이 조절")]
    [SerializeField] private float minVerticalLength = 0;

    [SerializeField] private float maxVerticalLength = 3;

    [Header("줌 설정")]
    [SerializeField] private float zoomSpeed = 10f;

    [SerializeField] private float minZoom = 0f;
    [SerializeField] private float maxZoom = 5f;

    [Header("임시 VirtualCamera")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private float curRotX;
    private float curRotY;
    private float curZoomValue;


    private Cinemachine3rdPersonFollow tpsCinemachine;


    /// <summary>
    /// 카메라 조작을 위한 현재 게임 view 로 볼때 foward
    /// </summary>
    public Transform CamTransform => rotPivot;

    public Vector3 Forward => rotPivot.forward;

    private bool isRightMouseClicked = false;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }


    private void Start()
    {
        InputManager.Instance.BindInputEvent(InputType.Camera, InputMapName.Default, InputActionName.RightMouseButton,
            OnClickRightMouseClick);
        InputManager.Instance.UseInput(InputType.Camera);


        // test
        SetControlCinemachine(virtualCamera);
        SwitchCameraControl(cameraControlType);
    }

    public void SetControlCinemachine(CinemachineVirtualCamera _virtualCamera)
    {
        virtualCamera = _virtualCamera;
        tpsCinemachine = virtualCamera?.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }


    public void SwitchCameraControl(CameraControlType _cameraControl)
    {
        cameraControlType = _cameraControl;
        switch (cameraControlType)
        {
            case CameraControlType.TpsFollow:
            case CameraControlType.TpsFixed:
            case CameraControlType.TpsAutoRotateFollow:
                InputManager.Instance.LockInput(InputType.Camera);
                break;

            case CameraControlType.TpsRotableFixed:
            case CameraControlType.TpsRotatableFollow:
            case CameraControlType.FirstPerson:
                InputManager.Instance.UseInput(InputType.Camera);
                curRotX = rotPivot.transform.eulerAngles.x;
                curRotY = rotPivot.transform.eulerAngles.y;
                curZoomValue = (maxZoom + minZoom) / 2;
                break;
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        curRotY = target.eulerAngles.y;
    }


    public void OnClickRightMouseClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRightMouseClicked = true;
        }
        else if (context.canceled)
        {
            isRightMouseClicked = false;
        }
    }

    private void Update()
    {
        InputRotate();
        InputZoom();
    }


    public void InputRotate()
    {
        if (useRightClick && isRightMouseClicked == false) return;
        switch (cameraControlType)
        {
            case CameraControlType.TpsRotableFixed:
            case CameraControlType.TpsRotatableFollow:
            case CameraControlType.FirstPerson:
                Vector2 axis = InputManager.Instance.GetAxis(InputType.Camera, InputActionName.Look);
                curRotX += (-axis.y * Time.deltaTime * lookSensitivity);
                curRotX = Mathf.Clamp(curRotX, limitMinX, limitMaxX);
                curRotY += (axis.x * Time.deltaTime * lookSensitivity);
                break;
        }
    }

    public void InputZoom()
    {
        if (InputManager.Instance.IsPressed(InputType.Camera, InputActionName.Zoom))
        {
            float f = InputManager.Instance.GetFloat(InputType.Camera, InputActionName.Zoom) * -1f;
            f = f * 0.01f;
            curZoomValue = Mathf.Clamp(curZoomValue + (f * zoomSpeed), minZoom, maxZoom);
        }
    }

    private void LateUpdate()
    {
        rotPivot.position = target.position;
        rotPivot.rotation = Quaternion.Euler(curRotX, curRotY, 0);

        switch (cameraControlType)
        {
            case CameraControlType.TpsRotableFixed:
            case CameraControlType.TpsRotatableFollow:
            case CameraControlType.FirstPerson:
                float calcMaxValue = Mathf.Lerp(minVerticalLength, maxVerticalLength, curZoomValue / (maxZoom - minZoom));
                tpsCinemachine.VerticalArmLength = Mathf.Lerp(calcMaxValue, minVerticalLength, curRotX / (limitMaxX - limitMinX));
                break;
        }

        tpsCinemachine.CameraDistance = curZoomValue;
    }
}