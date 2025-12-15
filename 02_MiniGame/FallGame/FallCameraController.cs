using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class FallCameraController : MonoBehaviour
{
    [Header("Cinemachine Virtual Cameras")]
    public CinemachineVirtualCamera vcam3D;   // 기존 3D 카메라
    public CinemachineVirtualCamera vcam25D;  // 새로 만든 2.5D 카메라
    public CinemachineVirtualCamera vcam2D;   // 새로 만든 2D 카메라

    private int mode = 0;  // 0 = 3D, 1 = 2.5D, 2 = 2D

    void Update()
    {
        // 테스트용 키 입력 (원하는 키로 변경 가능)
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchMode();
        }
    }

    public void SwitchMode()
    {
        // 2D 모드면 전환 중지
        if (mode == 2)
            return;
        mode = (mode + 1) % 3;
        UpdateCameraPriority();
        Debug.Log("Camera Mode Switched → " + mode);
    }

    void UpdateCameraPriority()
    {
        vcam3D.Priority = (mode == 0) ? 20 : 1;
        vcam25D.Priority = (mode == 1) ? 20 : 1;
        vcam2D.Priority = (mode == 2) ? 20 : 1;

        // 2D 모드일 때는 회전이 변하지 않도록 강제 고정
        if (mode == 2)
        {
            Force2DRotation();
        }
    }

    // 2D 모드에서 카메라 각도 강제 보정 (Composer 무력화 효과)
    void Force2DRotation()
    {
        vcam2D.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
