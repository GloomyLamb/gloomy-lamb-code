using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLimit : SceneSingletonManager<AreaLimit>
{
    public float LimitDistance => _limitDistance;
    private float _limitDistance;
    private Vector3 thisPosXZ;
    
    
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        _limitDistance = this.transform.lossyScale.x / 2;
        thisPosXZ = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
    }

    public bool CanMove(Vector3 nextPosition)
    {
        Vector3 nextPosXZ = new Vector3(nextPosition.x, 0f, nextPosition.z);
        if (Vector3.Distance(thisPosXZ, nextPosXZ) >= _limitDistance)
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, LimitDistance);
    }
}
