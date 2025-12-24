using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HowlWind : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float innerRadius = 2f;
    [SerializeField] float outerRadius = 3f;
    [SerializeField] float expandSpeed = 5f;

    [Header("판정 범위")]
    [SerializeField] float halfHeight = 0.5f;

    [Header("라인 그리기")]
    [SerializeField] LineRenderer line;
    [SerializeField] int lineSegment = 64;
    
    
    void Update()
    {
        outerRadius += expandSpeed * Time.deltaTime;
        innerRadius += expandSpeed * Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position, outerRadius);

        Vector3 center = transform.position;

        foreach (var hit in hits)
        {
            Vector3 pos = hit.transform.position;

            // float yDelta = Mathf.Abs(pos.y - center.y);
            // if (yDelta > halfHeight)
            //     continue;

            Vector2 c = new Vector2(center.x, center.z);
            Vector2 p = new Vector2(pos.x, pos.z);
            float dist = Vector2.Distance(c, p);

            if (dist >= innerRadius && dist <= outerRadius)
            {
                Debug.Log(hit.name);
            }
        }
    }
    
    void DrawRing()
    {
        float centerRadius = (innerRadius + outerRadius) * 0.5f;
        line.widthMultiplier = outerRadius - innerRadius;

        float angleStep = 2f * Mathf.PI / lineSegment;
        Vector3 center = transform.position;

        for (int i = 0; i < lineSegment; i++)
        {
            float angle = angleStep * i;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * centerRadius,
                0f,
                Mathf.Sin(angle) * centerRadius
            );

            line.SetPosition(i, center + pos);
        }
    }


#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        int segmentCount = 32;
        Vector3 center = transform.position;

        Gizmos.color = Color.red;
        DrawCircle(center + Vector3.up * halfHeight, innerRadius, segmentCount);
        DrawCircle(center - Vector3.up * halfHeight, innerRadius, segmentCount);

        Gizmos.color = Color.yellow;
        DrawCircle(center + Vector3.up * halfHeight, outerRadius, segmentCount);
        DrawCircle(center - Vector3.up * halfHeight, outerRadius, segmentCount);
    }

    void DrawCircle(Vector3 center, float radius, int segments)
    {
        float angleStep = Mathf.PI * 2f / segments;
        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = angleStep * i;

            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius) + center;

            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, point);
            }

            prevPoint = point;
        }
    }

#endif
}