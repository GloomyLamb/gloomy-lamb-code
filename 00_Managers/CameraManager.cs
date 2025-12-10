using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// todo: enum은 따로 관리
public enum VCType
{
    // 미니게임 - Fall
    FallVC3D,
    FallVC25D,
    FallVC2D,
}

public class CameraManager : MonoBehaviour
{
    // todo: 제네릭 매니저 들어오면 싱글톤 처리하기

    [Header("가상 카메라")]
    [SerializeField] private CinemachineVirtualCamera _curVirtualCam;       // 현재 카메라
    [SerializeField] private CinemachineFreeLook _curFreeLookCam;           // 현재 카메라
    [SerializeField] private List<CinemachineInfo> _sceneCams;              // 씬에서 사용하는 카메라 리스트

    private Dictionary<VCType, CinemachineInfo> _camDict = new();

    #region 초기화
    private void Awake()
    {
        // todo: cur cam 있으면 가져오고, 없으면 만들기
    }

    /// <summary>
    /// [public] 씬이 변경될 때마다 씬에 등록되어 있는 카메라 정보를 등록합니다.
    /// </summary>
    /// <param name="sceneCams"></param>
    public void Register(List<CinemachineInfo> sceneCams)
    {
        _sceneCams = sceneCams;
        _camDict = _sceneCams.ToDictionary(cam => cam.type, cam => cam);    // 딕셔너리 초기화

        if (_sceneCams.Count > 0)
        {
            // todo: 카메라 설정 지정
        }
    }
    #endregion

    #region 카메라 관리
    /// <summary>
    /// [public] 카메라 타입을 받아서 해당 설정을 적용합니다.
    /// </summary>
    /// <param name="camType">커스텀한 카메라의 타입</param>
    public void SwitchTo(VCType camType)
    {
        if (!_camDict.TryGetValue(camType, out var switchedCam))
        {
            Logger.LogWarning($"카메라 {camType} 없음");
            return;
        }

        switch (switchedCam.cinemachineType)
        {
            case CinemachineType.Virtual:
                SetVirtualCamera(switchedCam);
                break;
            case CinemachineType.FreeLook:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 가상 카메라 값 세팅하기
    /// </summary>
    /// <param name="info"></param>
    private void SetVirtualCamera(CinemachineInfo info)
    {
        if (info.follow != null)
        {
            _curVirtualCam.Follow = info.follow;
        }
        if (info.lookAt != null)
        {
            _curVirtualCam.LookAt = info.lookAt;
        }

        // Lens Setting
        LensSettings lensSettings = _curVirtualCam.m_Lens;
        lensSettings.FieldOfView = info.fieldOfView;
        lensSettings.ModeOverride = info.lensMode;
        _curVirtualCam.m_Lens = lensSettings;

        // Body Setting
        switch (info.bodyType)
        {
            case VCBodyType.ThirdPersonFollow:
                SetThirdPersonFollow(info.bodyThridPersonFollow);
                break;
            case VCBodyType.Transposer:
                SetTransposer(info.bodyTransposer);
                break;
        }


    }

    /// <summary>
    /// 시네머신 바디 타입 3인칭 추적 카메라 세팅
    /// </summary>
    /// <param name="info"></param>
    private void SetThirdPersonFollow(Body3PersonFollow info)
    {
        Cinemachine3rdPersonFollow body = new();

        // Rig Setting
        body.Damping = info.dampingValue;
        body.ShoulderOffset = info.shoulderOffset;
        body.CameraSide = info.cameraSide;
        body.CameraDistance = info.cameraDistance;

        // Obstacles Setting
        body.CameraCollisionFilter = info.cameraCollisionFilter;
        body.IgnoreTag = info.ignoreTag;
    }

    private void SetTransposer(BodyTransposer info)
    {

    }
    #endregion
}
