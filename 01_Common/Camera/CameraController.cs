using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// TpsCameraController 등으로 바꿔야할 것 같음
// abstract 하나 만들어야할듯!
public class CameraController : MonoBehaviour
{
    // todo : 카메라 매니저 들어오면 수정하기 
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private float lookSensitivity = 5;
    protected InputHandler input;
    
    CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbital;

    private float camCurRotX;
    private float camCurRotY;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        orbital = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        input = new InputHandler(inputAction, InputType.Camera);
    }

    private void Start()
    {
        
    }

    private void Update()
    {

        Vector2 axis = input.GetAxis(InputMapName.Default, InputActionName.Look);
        
        camCurRotX += (axis.y * Time.deltaTime * lookSensitivity);
        
        camCurRotY += (axis.x * Time.deltaTime* lookSensitivity);

        //Debug.Log(camCurRotX);
        orbital.m_Heading.m_Bias = camCurRotY;

    }
    
    

}
