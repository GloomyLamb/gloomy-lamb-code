using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstcleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint;           // 천장 위치
    public float spawnRangeX = 5f;
    public float spawnRangeZ = 5f;

    [Header("Spawn Timing")]
    public float obstacleInterval = 0.6f;  // 최소 생성 간격
    private float timer;

    [Header("Falling Speed")]
    public float obstacleFallingMinSpeed = 5f;
    public float obstacleFallingMaxSpeed = 12f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= obstacleInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float rx = Random.Range(-spawnRangeX, spawnRangeX);
        float rz = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 spawnPos = new Vector3(
            spawnPoint.position.x + rx,
            spawnPoint.position.y,
            spawnPoint.position.z + rz
        );

        GameObject obj = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Rigidbody로 낙하 속도 적용
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomSpeed = Random.Range(obstacleFallingMinSpeed, obstacleFallingMaxSpeed);
            rb.velocity = Vector3.down * randomSpeed;
        }


    }
}
