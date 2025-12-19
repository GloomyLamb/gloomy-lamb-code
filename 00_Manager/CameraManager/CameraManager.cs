using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
#endif

// todo: enum은 따로 관리
public enum VCType
{
    None,

    // 미니게임 - Fall
    FallVC3D,
    FallVC25D,
    FallVC2D,

    // 테스트
    TestVC1,
    TestVC2,
    TestVC3,
}

public class CameraManager : GlobalSingletonManager<CameraManager>
{
    [Header("Virtual Camera")]
    // 시네머신의 부드러운 카메라 이동을 이용하기 위해 2개의 카메라 사용
    [SerializeField] private CinemachineVirtualCamera _curVirtualCam1;
    [SerializeField] private CinemachineVirtualCamera _curVirtualCam2;
    [SerializeField] private bool _firstVirtualCam = false;     // 사용 중인 카메라 확인하기

    private VCType _curType = VCType.None;

    // 외부 카메라를 사용할 경우 캐싱
    [SerializeField] private CinemachineVirtualCamera _externalVirtualCam;
    private bool _useExternal = false;

    [Header("Free Look Camera")]
    [SerializeField] private CinemachineFreeLook _curFreeLookCam;

    [SerializeField] private List<CinemachineInfo> _sceneCams;      // 씬에서 사용하는 카메라 리스트
    private Dictionary<VCType, CinemachineInfo> _camDict = new();   // 씬에서 사용하는 카메라 딕셔너리

    // todo: 플레이어 캐싱해두기 (현재는 테스트용)
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;

    #region 초기화
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 파라미터 초기화
    /// </summary>
    protected override void Init()
    {
        base.Init();

        // todo: cur cam 있으면 가져오고, 없으면 만들기
        if (_curVirtualCam1 == null)
        {
            var go = new GameObject("Virtual Camera 1");
            go.AddComponent<CinemachineVirtualCamera>();
            _curVirtualCam1 = go.GetComponent<CinemachineVirtualCamera>();
        }

        if (_curVirtualCam2 == null)
        {
            var go = new GameObject("Virtual Camera 2");
            go.AddComponent<CinemachineVirtualCamera>();
            _curVirtualCam2 = go.GetComponent<CinemachineVirtualCamera>();
        }

        if (_curFreeLookCam == null)
        {
            var go = new GameObject("FreeLook Camera");
            go.AddComponent<CinemachineFreeLook>();
            _curFreeLookCam = go.GetComponent<CinemachineFreeLook>();
        }

        if (_player == null)
        {
            _player = FindObjectOfType<CapsuleCollider>().transform;
        }

        if (_camera == null)
        {
            _camera = Camera.main.transform;
        }
    }

    /// <summary>
    /// [public] 씬이 변경될 때마다 씬에 등록되어 있는 카메라 정보를 등록합니다.
    /// </summary>
    /// <param name="sceneCams"></param>
    public void Register(List<CinemachineInfo> sceneCams)
    {
        _sceneCams = sceneCams;
        _camDict = _sceneCams.ToDictionary(cam => cam.type, cam => cam);    // 딕셔너리 초기화

        if (_sceneCams.Count == 0)
        {
            Logger.LogWarning("씬 카메라 리스트 없음");
            return;
        }

        SwitchTo(_sceneCams[0].type);
    }
    #endregion

    #region 카메라 관리
    /// <summary>
    /// [public] 카메라 타입을 받아서 해당 설정을 적용합니다.
    /// </summary>
    /// <param name="camType">커스텀한 카메라의 타입</param>
    public void SwitchTo(VCType camType)
    {
        if (!_camDict.TryGetValue(camType, out var switchedCamInfo))
        {
            Logger.LogWarning($"카메라 {camType} 없음");
            return;
        }

        if (_curType == camType)    // 동일한 타입의 카메라 사용
        {
            Logger.Log("이미 사용 중인 카메라 입니다.");
            return;
        }

        CheckNullOfFollowNLookAt(switchedCamInfo);

        // Virtual Camera vs FreeLook
        switch (switchedCamInfo.cinemachineType)
        {
            case CinemachineType.Virtual:
                Logger.Log("Virtual Camera 변경");
                SwitchTo(SetVirtualCamera(switchedCamInfo._virtualCam), false);
                break;
            case CinemachineType.FreeLook:
                Logger.Log("FreeLook Camera 변경");
                SetPriority(CinemachineType.FreeLook);
                // todo: free look 로직 짜기
                break;
            default:
                break;
        }

        _curType = camType;         // 타입 캐싱
    }

    /// <summary>
    /// [public] 파라미터를 현재 보는 카메라로 설정합니다.
    /// </summary>
    /// <param name="virtualCam"></param>
    /// <param name="useExternal">카메라 매니저와 연결되어 있는 가상 카메라를 사용하지 않고 외부에서 사용하는 경우</param>
    public void SwitchTo(CinemachineVirtualCamera virtualCam, bool useExternal = true)
    {
        // todo: 외부에서 들어오는 카메라라면 기존 카메라랑 연결이 끊길 것으로 예상
        // 노는 카메라 캐싱하는 방법 필요
        if (_useExternal != useExternal && !useExternal)
        {
            _externalVirtualCam = null;
        }
        _useExternal = useExternal;

        if (useExternal)
        {
            _externalVirtualCam = virtualCam;
            SetPriority(CinemachineType.Virtual);
            _curType = VCType.None;
            return;
        }

        SetPriority(CinemachineType.Virtual);
        if (_firstVirtualCam)
        {
            Logger.Log("Virtual Camera 2번으로 변경");
            _curVirtualCam2 = virtualCam;
            _firstVirtualCam = false;
        }
        else
        {
            Logger.Log("Virtual Camera 1번으로 변경");
            _curVirtualCam1 = virtualCam;
            _firstVirtualCam = true;
        }
    }

    /// <summary>
    /// [public] 파라미터를 현재 보는 카메라로 설정합니다.
    /// </summary>
    /// <param name="freeLookCam"></param>
    public void SwitchTo(CinemachineFreeLook freeLookCam)
    {
        SetPriority(CinemachineType.FreeLook);
        Logger.Log("FreeLook Camera로 변경");
        _curFreeLookCam = freeLookCam;
    }

    /// <summary>
    /// [public] Follow 타겟을 변경합니다.
    /// </summary>
    /// <param name="target"></param>
    public void SwitchFollow(Transform target)
    {
        _curFreeLookCam.Follow = target;
        _curVirtualCam1.Follow = target;
        _curVirtualCam2.Follow = target;
    }

    /// <summary>
    /// [public] LookAt 타겟을 변경합니다.
    /// </summary>
    /// <param name="target"></param>
    public void SwitchFollowLook(Transform target)
    {
        _curFreeLookCam.LookAt = target;
        _curVirtualCam1.LookAt = target;
        _curVirtualCam2.LookAt = target;
    }

    /// <summary>
    /// info의 follow, lookAt에 값이 없으면 기본값을 넣어줍니다.
    /// </summary>
    /// <param name="info"></param>
    private void CheckNullOfFollowNLookAt(CinemachineInfo info)
    {
        if (_firstVirtualCam)
        {
            _curVirtualCam2.Follow = info.follow != null ? info.follow : _player;
            _curVirtualCam2.LookAt = info.lookAt != null ? info.lookAt : _player;
        }
        else
        {
            _curVirtualCam1.Follow = info.follow != null ? info.follow : _player;
            _curVirtualCam1.LookAt = info.lookAt != null ? info.lookAt : _player;
        }
    }

    /// <summary>
    /// 카메라 타입에 맞게 우선순위를 설정해줍니다.
    /// </summary>
    /// <param name="type"></param>
    private void SetPriority(CinemachineType type)
    {
        if (_useExternal) // 외부 카메라 사용 중
        {
            _externalVirtualCam.Priority = Define.ActivePriority;
            _curVirtualCam1.Priority = Define.InactivePriority;
            _curVirtualCam2.Priority = Define.InactivePriority;
            _curFreeLookCam.Priority = Define.InactivePriority;
            return;
        }

        // 현재 카메라가 1번째일 경우 2번째 카메라 수정
        // 현재 카메라가 2번째일 경우 1번째 카메라 수정
        if (type == CinemachineType.Virtual)
        {
            if (_firstVirtualCam)
            {
                Logger.Log("Virtual Camera 2번으로 우선순위 변경");
                _curVirtualCam1.Priority = Define.InactivePriority;
                _curVirtualCam2.Priority = Define.ActivePriority;
            }
            else
            {
                Logger.Log("Virtual Camera 1번으로 우선순위 변경");
                _curVirtualCam1.Priority = Define.ActivePriority;
                _curVirtualCam2.Priority = Define.InactivePriority;
            }
            _curFreeLookCam.Priority = Define.InactivePriority;
        }
        else if (type == CinemachineType.FreeLook)
        {
            _curVirtualCam1.Priority = Define.InactivePriority;
            _curVirtualCam2.Priority = Define.InactivePriority;
            _curFreeLookCam.Priority = Define.ActivePriority;
        }
    }
    #endregion

    #region Virtual Camera 관리
    /// <summary>
    /// 가상 카메라 값 세팅하기
    /// </summary>
    /// <param name="info"></param>
    private CinemachineVirtualCamera SetVirtualCamera(CinemachineVirtualInfo info)
    {
        CinemachineVirtualCamera cam;
        // 현재 카메라가 1번째일 경우 2번째 카메라 수정
        // 현재 카메라가 2번째일 경우 1번째 카메라 수정
        cam = _firstVirtualCam ? _curVirtualCam2 : _curVirtualCam1;

        // Lens Setting
        LensSettings lensSettings = cam.m_Lens;
        lensSettings.FieldOfView = info.fieldOfView;
        lensSettings.ModeOverride = info.lensMode;
        cam.m_Lens = lensSettings;

        // Body Setting
        switch (info.bodyType)
        {
            case VCBodyType.ThirdPersonFollow:
                SetBodyThirdPersonFollow(cam, info.bodyThridPersonFollow);
                break;
            case VCBodyType.Transposer:
                SetBodyTransposer(cam, info.bodyTransposer);
                break;
        }

        // Aim Setting
        switch (info.aimType)
        {
            case VCAimType.Composer:
                SetAimComposer(cam, info.aimComposer);
                break;
            case VCAimType.HardLookAt:
                SetAimHardLookAt(cam);
                break;
        }

        return cam;
    }
    #endregion

    #region Body 관리
    /// <summary>
    /// 시네머신 바디 - 3 person follow 카메라 세팅
    /// </summary>
    /// <param name="info"></param>
    private CinemachineVirtualCamera SetBodyThirdPersonFollow(
        CinemachineVirtualCamera cam,
        Body3PersonFollow info)
    {
        Cinemachine3rdPersonFollow body;
        CinemachineComponentBase component = cam
            .GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as Cinemachine3rdPersonFollow))
        {
            cam.AddCinemachineComponent<Cinemachine3rdPersonFollow>();
            body = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
        else
        {
            body = (Cinemachine3rdPersonFollow)component;
        }

        // Rig Setting
        body.Damping = info.dampingValue;
        body.ShoulderOffset = info.shoulderOffset;
        body.CameraSide = info.cameraSide;
        body.CameraDistance = info.cameraDistance;

        // Obstacles Setting
        body.CameraCollisionFilter = info.cameraCollisionFilter;
        body.IgnoreTag = info.ignoreTag;

        Logger.Log($"{(_firstVirtualCam ? "2번째" : "1번째")} 카메라 Body 변경 완료");
        return cam;
    }

    /// <summary>
    /// 시네머신 바디 - Transposer 카메라 세팅
    /// </summary>
    /// <param name="info"></param>
    private CinemachineVirtualCamera SetBodyTransposer(
        CinemachineVirtualCamera cam,
        BodyTransposer info)
    {
        CinemachineTransposer body;
        CinemachineComponentBase component = cam
            .GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineTransposer))
        {
            cam.AddCinemachineComponent<CinemachineTransposer>();
            body = cam.GetCinemachineComponent<CinemachineTransposer>();
        }
        else
        {
            body = (CinemachineTransposer)component;
        }

        body.m_BindingMode = info.bindingMode;
        body.m_FollowOffset = info.followOffset;
        body.m_XDamping = info.xDaming;
        body.m_YDamping = info.yDaming;
        body.m_ZDamping = info.zDaming;
        body.m_YawDamping = info.yawDaming;

        return cam;
    }
    #endregion

    #region Aim 관리
    private CinemachineVirtualCamera SetAimComposer(
        CinemachineVirtualCamera cam,
        AimComposer info)
    {
        CinemachineComposer aim;
        CinemachineComponentBase component = cam
            .GetCinemachineComponent(CinemachineCore.Stage.Aim);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineComposer))
        {
            cam.AddCinemachineComponent<CinemachineComposer>();
            aim = cam.GetCinemachineComponent<CinemachineComposer>();
        }
        else
        {
            aim = (CinemachineComposer)component;
        }

        aim.m_TrackedObjectOffset = info.trackedObjectOffset;
        aim.m_ScreenX = info.screenX;
        aim.m_ScreenY = info.screenY;
        aim.m_DeadZoneWidth = info.deadZoneWidth;
        aim.m_DeadZoneHeight = info.deadZoneHeight;
        aim.m_DeadZoneWidth = info.deadZoneWidth;
        aim.m_SoftZoneHeight = info.softZoneHeight;
        aim.m_BiasX = info.biasX;
        aim.m_BiasY = info.biasY;
        aim.m_CenterOnActivate = info.centerOnActivate;

        return cam;
    }

    private CinemachineVirtualCamera SetAimHardLookAt(CinemachineVirtualCamera cam)
    {
        CinemachineHardLookAt aim;
        CinemachineComponentBase component = cam
            .GetCinemachineComponent(CinemachineCore.Stage.Aim);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineHardLookAt))
        {
            cam.AddCinemachineComponent<CinemachineHardLookAt>();
            aim = cam.GetCinemachineComponent<CinemachineHardLookAt>();
        }
        else
        {
            aim = (CinemachineHardLookAt)component;
        }

        return cam;
    }
    #endregion
}