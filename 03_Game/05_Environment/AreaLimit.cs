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
    
    public Vector3 GetNextPosition(Vector3 currentPosition, Vector3 nextPosition)
    {
        Vector3 curXZ = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 nextXZ = new Vector3(nextPosition.x, 0f, nextPosition.z);

        Vector3 dir = nextXZ - thisPosXZ;

        if ( dir.magnitude <= _limitDistance)
            return nextPosition;
        
        Vector3 canMove = Vector3.ProjectOnPlane( nextXZ - curXZ, dir.normalized);  // 벽방향 없애기
        Vector3 calcedNextDir = curXZ + canMove;
        Vector3 calcedNextPos = thisPosXZ + (calcedNextDir - thisPosXZ).normalized * _limitDistance;
        return new Vector3(calcedNextPos.x, nextPosition.y, calcedNextPos.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, LimitDistance);
    }
}
