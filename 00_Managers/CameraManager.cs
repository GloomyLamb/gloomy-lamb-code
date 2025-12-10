using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// todo: enum은 따로 관리
public enum VCType
{
    // 미니게임 - Fall
    FallVC3D,
    FallVC25D,
    FallVC2D,

    // 테스트
    TestVC1,
    TestVC2,
    TestVC3,
}

public class CameraManager : MonoBehaviour
{
    // todo: 제네릭 매니저 들어오면 싱글톤 처리하기

    [Header("가상 카메라")]
    [SerializeField] private CinemachineVirtualCamera _curVirtualCam;
    [SerializeField] private CinemachineFreeLook _curFreeLookCam;
    [SerializeField] private List<CinemachineInfo> _sceneCams;      // 씬에서 사용하는 카메라 리스트

    private Dictionary<VCType, CinemachineInfo> _camDict = new();   // 씬에서 사용하는 카메라 딕셔너리

    // todo: 플레이어 캐싱해두기 (현재는 테스트용)
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;

    #region 초기화
    private void Awake()
    {
        // todo: cur cam 있으면 가져오고, 없으면 만들기
        if (_curVirtualCam == null)
        {
            _curVirtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        if (_curFreeLookCam == null)
        {
            _curFreeLookCam = FindObjectOfType<CinemachineFreeLook>();
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

        if (switchedCamInfo.follow != null)
        {
            _curVirtualCam.Follow = switchedCamInfo.follow;
        }
        else
        {
            _curVirtualCam.Follow = _player;
        }

        if (switchedCamInfo.lookAt != null)
        {
            _curVirtualCam.LookAt = switchedCamInfo.lookAt;
        }
        else
        {
            _curVirtualCam.LookAt = _player;
        }

        switch (switchedCamInfo.cinemachineType)
        {
            case CinemachineType.Virtual:
                SetVirtualCamera(switchedCamInfo._virtualCam);
                break;
            case CinemachineType.FreeLook:
                // todo: free look 로직 짜기
                break;
            default:
                break;
        }
    }

    public void SwitchTo(CinemachineVirtualCamera virtualCam)
    {
        _curVirtualCam = virtualCam;
    }

    public void SwitchTo(CinemachineFreeLook freeLookCam)
    {
        _curFreeLookCam = freeLookCam;
    }

    public void SwitchFollow(Transform target)
    {
        _curFreeLookCam.Follow = target;
        _curVirtualCam.Follow = target;
    }

    public void SwitchFollowLook(Transform target)
    {
        _curFreeLookCam.LookAt = target;
        _curVirtualCam.LookAt = target;
    }
    #endregion

    #region Virtual Camera 관리
    /// <summary>
    /// 가상 카메라 값 세팅하기
    /// </summary>
    /// <param name="info"></param>
    private void SetVirtualCamera(CinemachineVirtualInfo info)
    {
        // Lens Setting
        LensSettings lensSettings = _curVirtualCam.m_Lens;
        lensSettings.FieldOfView = info.fieldOfView;
        lensSettings.ModeOverride = info.lensMode;
        _curVirtualCam.m_Lens = lensSettings;

        // Body Setting
        switch (info.bodyType)
        {
            case VCBodyType.ThirdPersonFollow:
                SetBodyThirdPersonFollow(info.bodyThridPersonFollow);
                break;
            case VCBodyType.Transposer:
                SetBodyTransposer(info.bodyTransposer);
                break;
        }

        // Aim Setting
        switch (info.aimType)
        {
            case VCAimType.Composer:
                SetAimComposer(info.aimComposer);
                break;
            case VCAimType.HardLookAt:
                SetAimHardLookAt();
                break;
        }
    }
    #endregion

    #region Body 관리
    /// <summary>
    /// 시네머신 바디 - 3 person follow 카메라 세팅
    /// </summary>
    /// <param name="info"></param>
    private void SetBodyThirdPersonFollow(Body3PersonFollow info)
    {
        Cinemachine3rdPersonFollow body;
        CinemachineComponentBase component = _curVirtualCam
            .GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as Cinemachine3rdPersonFollow))
        {
            _curVirtualCam.AddCinemachineComponent<Cinemachine3rdPersonFollow>();
            body = _curVirtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
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
    }

    /// <summary>
    /// 시네머신 바디 - Transposer 카메라 세팅
    /// </summary>
    /// <param name="info"></param>
    private void SetBodyTransposer(BodyTransposer info)
    {
        CinemachineTransposer body;
        CinemachineComponentBase component = _curVirtualCam
            .GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineTransposer))
        {
            _curVirtualCam.AddCinemachineComponent<CinemachineTransposer>();
            body = _curVirtualCam.GetCinemachineComponent<CinemachineTransposer>();
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
    }
    #endregion

    #region Aim 관리
    private void SetAimComposer(AimComposer info)
    {
        CinemachineComposer aim;
        CinemachineComponentBase component = _curVirtualCam
            .GetCinemachineComponent(CinemachineCore.Stage.Aim);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineComposer))
        {
            _curVirtualCam.AddCinemachineComponent<CinemachineComposer>();
            aim = _curVirtualCam.GetCinemachineComponent<CinemachineComposer>();
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
    }

    private void SetAimHardLookAt()
    {
        CinemachineHardLookAt aim;
        CinemachineComponentBase component = _curVirtualCam
            .GetCinemachineComponent(CinemachineCore.Stage.Aim);

        // 컴포넌트가 null이거나 해당 클래스가 아닐 경우
        if (component == null || !(component as CinemachineHardLookAt))
        {
            _curVirtualCam.AddCinemachineComponent<CinemachineHardLookAt>();
            aim = _curVirtualCam.GetCinemachineComponent<CinemachineHardLookAt>();
        }
        else
        {
            aim = (CinemachineHardLookAt)component;
        }
    }
    #endregion

    #region 테스트 코드
    public void Test_PushCamera()
    {
        List<CinemachineInfo> infos = new()
        {
            FindTestSO("TestVC1"),
            FindTestSO("TestVC2"),
            FindTestSO("TestVC3")
        };
        Register(infos);
    }

    public void Test_SwitchCamera1()
    {
        SwitchTo(VCType.TestVC1);
    }

    public void Test_SwitchCamera2()
    {
        SwitchTo(VCType.TestVC2);
    }

    public void Test_SwitchCamera3()
    {
        SwitchTo(VCType.TestVC3);
    }

    public void Test_ResetCamera()
    {
        _sceneCams.Clear();
        _camDict.Clear();
    }

    private CinemachineInfo FindTestSO(string name)
    {
        Logger.Log($"so 데이터 찾기: {name}");
        string[] guids = AssetDatabase.FindAssets($"{name} t:CinemachineInfo");

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var def = AssetDatabase.LoadAssetAtPath<CinemachineInfo>(path);
        return def;
    }
    #endregion
}