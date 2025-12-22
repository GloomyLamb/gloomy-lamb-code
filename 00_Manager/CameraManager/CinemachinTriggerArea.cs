using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachinTriggerArea : MonoBehaviour
{
    // todo : CameraManager와 맞추기
    [SerializeField] private CinemachineVirtualCamera areaCamera;
    [SerializeField] private CameraControlOption camControlOption;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            CameraController.Instance.SetControlCinemachine(areaCamera);
            CameraController.Instance.SetControlOption(camControlOption,true);
        }
    }
}