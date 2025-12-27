using UnityEngine;


public class HowlWind : PoolObject
{
    [Header("설정")]
    [SerializeField] float _width = 2f;
    [SerializeField] private float _maxRadius = 3f;
    [SerializeField] float _expandSpeed = 5f;
    [SerializeField] private float heightOffset = 0f;
    
    [Header("판정 높이")]
    [SerializeField] float checkHeight = 0.5f;
    
    [Header("데미지")]
    [SerializeField]
    private float _damage = 10f;
    
    [Header("라인 그리기")]
    [SerializeField] LineRenderer line;
    [SerializeField] int lineSegment = 64;

    float _radius = 0f;
    
    
    private void Start()
    {
        if (line != null)
            line.positionCount = lineSegment;

        
    }

    protected override void OnEnableInternal()
    {
        _radius = 0;
    }


    void Update()
    {
        DrawRing();

        _radius += _expandSpeed * Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position + (Vector3.up * heightOffset), _radius + (_width / 2));


        foreach (var hit in hits)
        {
            if (hit is MeshCollider) continue;  // closestPoint가 MeshCollider 몬쓴다고 워닝 떠서 넘김!
            
            Vector3 pos = hit.transform.position;
            float dist = Vector3.Distance(this.transform.position + (Vector3.up * heightOffset), hit.transform.position);

            if (dist >= (_radius - _width / 2) && dist <= (_radius + _width / 2))
            {
                float hitY = hit.ClosestPoint(transform.position).y;
                if (hitY >= this.transform.position.y - (checkHeight) &&
                    hitY <= this.transform.position.y + (checkHeight))
                {
                    Player player = hit.transform.GetComponent<Player>();
                    player?.Damage(_damage);
                    // Debug.Log(
                    //     $"{hit.name} : {hitY}, {this.transform.position.y - (checkHeight / 2)}, {this.transform.position.y + (checkHeight / 2)}");
                }
            }
        }

        if (_radius >= _maxRadius)
        {
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void DrawRing()
    {
        float centerRadius = _radius;
        line.widthMultiplier = _width;

        float angleStep = 360f / lineSegment;
        Vector3 center = transform.position + (Vector3.up * heightOffset);

        for (int i = 0; i < lineSegment; i++)
        {
            float angle = angleStep * i;
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            line.SetPosition(i, center + dir * centerRadius);
        }
    }


    void OnValidate()
    {
        if (line == null) return;
        DrawRing();
    }


#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Vector3 center = transform.position;

        Gizmos.color = Color.red;
        DrawCircle(center, (_maxRadius) + (_width / 2));
    }

    void DrawCircle(Vector3 center, float radius)
    {
        float angleStep = 360f / lineSegment;

        Vector3 dir = Quaternion.AngleAxis(0, Vector3.up) * Vector3.forward;
        Vector3 prevPoint = center + dir * radius;

        for (int i = 1; i <= lineSegment; i++)
        {
            float angle = angleStep * i;

            dir = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            Vector3 point = center + dir * radius;

            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, point);
            }

            prevPoint = point;
        }
    }

#endif
}