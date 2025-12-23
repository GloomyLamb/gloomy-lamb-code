using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)   // is Trigger 체크박스가 켜져 있어야 작동 트리거체크 
    {
        if(other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeSlowDown();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
