using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 12f;    // WASD 이동 속도 
    public float fallSpeed = 15f;    // 낙하 속도    -기획서 기준-
    private int playerStage = 0;
    private Rigidbody rb;
    public FallCameraController cameraController;
    [SerializeField] private GameObject cab3D;
    [SerializeField] private GameObject cube2_5D;
    [SerializeField] private GameObject plane2D;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb. useGravity = false; // 중력 사용하지않음. 인스펙터에서 설정해도되긴하는데 그냥 써놓음.
        UpdatePlayerStage();

    }
    void Update()
    {
        // 테스트용 T키 → 단계적 캐릭터 전환
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerStage < 2)
            {
                playerStage++;
                UpdatePlayerStage();
                cameraController?.SwitchMode();
                Debug.Log("테스트: playerStage = " + playerStage);
            }
        }
    }
    private void FixedUpdate()  
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");     // WASD 입력값인데 프로토타입 코드 그냥 가져옴. 

        Vector3 move = (forward * vertical + right * horizontal) * moveSpeed;
        Vector3 fall = Vector3.down * fallSpeed;
        rb.velocity=( move + fall);
    }
     private void OnCollisionEnter(Collision collision)
    {
        if (playerStage >= 2) return; // 이미 2D면 더 이상 안 바뀜
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("오브젝트 충돌");
            if(cameraController != null)
            {
                cameraController.SwitchMode(); // 충돌시 카메라 모드 변경
                playerStage++;
                UpdatePlayerStage();
            }
            
        }
    }
    private void UpdatePlayerStage()
    {
        cab3D.SetActive(playerStage == 0);
        cube2_5D.SetActive(playerStage == 1);
        plane2D.SetActive(playerStage == 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && playerStage < 2)
        {
            playerStage = 2;
            UpdatePlayerStage();
        }
    }
}
