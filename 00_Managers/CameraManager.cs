using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // todo: 제네릭 매니저 들어오면 싱글톤 처리하기

    [Header("가상 카메라")]
    [SerializeField] private CinemachineVirtualCamera _curCam;              // 현재 카메라
    [SerializeField] private List<CinemachineVirtualCamera> _sceneCams;    // 씬에서 사용하는 카메라 리스트

    private Dictionary<string, CinemachineVirtualCamera> _camDict = new();

    #region 초기화
    /// <summary>
    /// [public] 씬이 변경될 때마다 씬에 등록되어 있는 카메라 정보를 등록합니다.
    /// </summary>
    /// <param name="sceneCams"></param>
    public void Register(List<CinemachineVirtualCamera> sceneCams)
    {
        _sceneCams = sceneCams;
        _curCam = _sceneCams[0];

        _camDict = _sceneCams.ToDictionary(cam => cam.name, cam => cam);
    }
    #endregion

    #region [public] 카메라 관리
    /// <summary>
    /// [public] 카메라 이름을 받아서 해당 카메라를 가장 위에 띄웁니다.
    /// name은 카메라 오브젝트 이름입니다.
    /// </summary>
    /// <param name="name"></param>
    public void SwitchTo(string camName)
    {
        if (!_camDict.TryGetValue(camName, out var switchedCam))
        {
            Logger.LogWarning($"카메라 {camName} 없음");
            return;
        }

        foreach (CinemachineVirtualCamera cam in _camDict.Values)
        {
            cam.Priority = Define.InactivePriority;         // 모든 카메라 우선순위 초기화
        }

        switchedCam.Priority = Define.ActivePriority;   // 선택한 카메라 우선순위 지정
        _curCam = switchedCam;                          // 현재 카메라 변경
    }

    /// <summary>
    /// [public] 카메라의 Follow 타겟을 지정합니다.
    /// </summary>
    /// <param name="camName"></param>
    /// <param name="target"></param>
    public void SetFollowTarget(string camName, Transform target)
    {
        if (!_camDict.TryGetValue(camName, out var cam))
        {
            Logger.Log($"카메라 {camName} 없음");
            return;
        }
        cam.Follow = target;
    }

    /// <summary>
    /// [public] 카메라의 LookAt 타켓을 지정합니다.
    /// </summary>
    /// <param name="camName"></param>
    /// <param name="target"></param>
    public void SetLookAtTarget(string camName, Transform target)
    {
        if (!_camDict.TryGetValue(camName, out var cam))
        {
            Logger.Log($"카메라 {camName} 없음");
            return;
        }
        cam.LookAt = target;
    }
    #endregion
}
