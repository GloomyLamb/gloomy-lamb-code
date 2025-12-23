using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

// public enum CameraViewType
// {
//     TpsFixed,
//     TpsFixedRotatable,
//     TpsFollow,
//     TpsFollowRotatable,
//     TpsAutoRotateFollow,
//     TpsRotatableFollow,
//     FirstPerson,
// }

[Flags]
public enum CameraControlOption
{
    None = 0,
    RotationPitch = 1 << 1,
    RotationYaw = 1 << 2,
    Zoom = 1 << 3,
    RotationUsingRightMouse = 1 << 4,
}

// todo : 카메라 매니저의 카메라를 채가야 할 듯 함...
// 카메라 메니저에 자식으로 붙일까? 카메라 매니저에서 Forward 데리고 올 수 있도록해도 좋을 것 같다..?
// 아니면 얘를 따로 등록해야하나...?
public class CameraController : MonoBehaviour
{
    public static CameraController Instance => instance;
    private static CameraController instance;

    // [Header("조작 설정")]
    // [SerializeField] private CameraViewType cameraViewType = CameraViewType.TpsAutoRotateFollow;

    [SerializeField] private CameraControlOption camControlOption = CameraControlOption.None;

    [Header("카메라 조작을 위한 pivot")]
    [SerializeField] private Transform lookPivot;

    [Header("pivot이 따라다닐 target")]
    public Transform target;

    [Header("회전 설정")]
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

    public CinemachineVirtualCamera VirtaulCamera => virtualCamera;

    private float curRotX;
    private float curRotY;
    private float curZoomValue;


    private Cinemachine3rdPersonFollow tpsCinemachine;


    /// <summary>
    /// 카메라 조작을 위한 현재 게임 view 로 볼때 foward
    /// </summary>
    public Transform CamTransform => virtualCamera.transform;
    //public Vector3 Forward => rotPivot.forward;

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

        curZoomValue = maxZoom;

        // test
        SetControlCinemachine(virtualCamera);
        //SwitchCameraControl(cameraViewType);
    }

    public void SetControlCinemachine(CinemachineVirtualCamera _virtualCamera)
    {
        if (virtualCamera != null)
            virtualCamera.Priority = 0;

        virtualCamera = _virtualCamera;
        virtualCamera.Priority = 1;
        tpsCinemachine = virtualCamera?.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }


    // public void SwitchCameraControl(CameraViewType cameraView)
    // {
    //     cameraViewType = cameraView;
    //     switch (cameraViewType)
    //     {
    //         case CameraViewType.TpsFollow:
    //         case CameraViewType.TpsFixed:
    //         case CameraViewType.TpsAutoRotateFollow:
    //             InputManager.Instance.LockInput(InputType.Camera);
    //             break;
    //
    //         case CameraViewType.TpsRotableFixed:
    //         case CameraViewType.TpsRotatableFollow:
    //         case CameraViewType.FirstPerson:
    //             InputManager.Instance.UseInput(InputType.Camera);
    //             curRotX = rotPivot.transform.eulerAngles.x;
    //             curRotY = rotPivot.transform.eulerAngles.y;
    //             curZoomValue = (maxZoom + minZoom) / 2;
    //             break;
    //     }
    // }

    public void SetTarget(Transform _target)
    {
        target = _target;
        curRotY = target.eulerAngles.y;
    }

    public void SetControlOption(CameraControlOption option, bool useOption = true)
    {
        // None 만 함수 빼는 게 더 예쁠 듯
        if (CameraControlOption.None == option &&  useOption)
        {
            camControlOption = option;
            return;
        }

        if (useOption)
        {
            camControlOption |= option;
        }
        else
        {
            camControlOption &= ~option;
        }
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
        if ((camControlOption.HasFlag(CameraControlOption.RotationUsingRightMouse) &&
             isRightMouseClicked == false)) return;

        // todo: Mac 에서 받아지는 값 다른지 가영님 PC로 로그찍고 확인해보기 
        Vector2 axis = InputManager.Instance.GetAxis(InputType.Camera, InputActionName.Look);

        if (camControlOption.HasFlag(CameraControlOption.RotationPitch))
        {
            curRotX += (-axis.y * Time.deltaTime * lookSensitivity);
            curRotX = Mathf.Clamp(curRotX, limitMinX, limitMaxX);
        }

        if (camControlOption.HasFlag(CameraControlOption.RotationYaw))
        {
            curRotY += (axis.x * Time.deltaTime * lookSensitivity);
        }
    }

    public void InputZoom()
    {
        if (camControlOption.HasFlag(CameraControlOption.Zoom))
        {
            if (InputManager.Instance.IsPressed(InputType.Camera, InputActionName.Zoom))
            {
                float f = InputManager.Instance.GetFloat(InputType.Camera, InputActionName.Zoom) * -1f;
                f = f * 0.01f;
                curZoomValue = Mathf.Clamp(curZoomValue + (f * zoomSpeed), minZoom, maxZoom);
            }
        }
    }

    private void LateUpdate()
    {
        lookPivot.position = target != null ? target.position : Vector3.zero;
        lookPivot.rotation = Quaternion.Euler(curRotX, curRotY, 0);

        float calcMaxValue = Mathf.Lerp(minVerticalLength, maxVerticalLength, curZoomValue / (maxZoom - minZoom));
        // switch (cameraViewType)
        // {
        //     // case CameraViewType.TpsRotableFixed:
        //     // case CameraViewType.TpsRotatableFollow:
        //     // case CameraViewType.FirstPerson:
        //         float calcMaxValue = Mathf.Lerp(minVerticalLength, maxVerticalLength, curZoomValue / (maxZoom - minZoom));
        //         tpsCinemachine.VerticalArmLength = Mathf.Lerp(calcMaxValue, minVerticalLength, curRotX / (limitMaxX - limitMinX));
        //         break;
        // }


        if (tpsCinemachine == null) return;
        if (camControlOption.HasFlag(CameraControlOption.Zoom))
        {
            tpsCinemachine.CameraDistance = curZoomValue;
        }

        if (camControlOption.HasFlag(CameraControlOption.RotationPitch))
        {
            tpsCinemachine.VerticalArmLength = Mathf.Lerp(calcMaxValue, minVerticalLength, curRotX / (limitMaxX - limitMinX));
        }
    }
}