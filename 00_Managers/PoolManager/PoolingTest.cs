using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolingTest : MonoBehaviour
{
    private void Start()
    {
        // 사용할 Pool 을 Start 때 알려주기.
        PoolManager.Instance.UsePool(PoolType.TestObjectPool);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = PoolManager.Instance.Spawn(PoolType.TestObjectPool);
            StartCoroutine(DeactiveObjectRoutine(go));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Scene 전환 테스트
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    IEnumerator DeactiveObjectRoutine(GameObject go)
    {
        yield return new WaitForSeconds(1);
        go?.SetActive(false);
    }
}
