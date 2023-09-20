using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void TargetsVisibilityChange(List<Transform> newTargets);

[ExecuteInEditMode]
public class FieldOfView : MonoBehaviour
{
    public float viewRadius;             // 시야 반경
    [Range(0, 360)]
    public float viewAngle;             // 시야 각도
    public float viewDepth;             // 시야 깊이
    public LayerMask targetMask;        // 타겟 마스크
    public LayerMask obstacleMask;      // 장애물 마스크

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();  // 보이는 타겟 리스트

    public int meshResolution;          // 메시 해상도
    public int edgeResolveIterations;   // 가장자리 해결 반복 횟수
    public float edgeDstThreshold;      // 가장자리 거리 임계값

    public MeshFilter viewMeshFilter;   // 시야 메시 필터
    public bool debug;                  // 디버그 모드
    Mesh viewMesh;

    public static event TargetsVisibilityChange OnTargetsVisibilityChange;  // 타겟 가시성 변경 이벤트

    public FogProjector fogProjector;   // 안개 프로젝터
    public float updateDistance = 1;
    Vector3 lastUpdatePos;

    void OnEnable()
    {
        viewMesh = new Mesh { name = "View Mesh" };
        viewMeshFilter.mesh = viewMesh;

        fogProjector = fogProjector ?? FindObjectOfType<FogProjector>();

        // 타겟 검색을 지연시키는 코루틴 시작
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void LateUpdate()
    {
        DrawFieldOfView();

        // 위치 업데이트 감지 및 안개 업데이트
        if (Vector3.Distance(transform.position, lastUpdatePos) > updateDistance || Time.time < .5f)
        {
            lastUpdatePos = transform.position;
            fogProjector?.UpdateFog();
        }
    }

    // 보이는 타겟 찾기
    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        // 시야 내의 모든 타겟을 검색
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // 시야 각도 내에 있는지 확인
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // 장애물이 없는지 검사
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        // 타겟 가시성 변경 이벤트 발생
        if (OnTargetsVisibilityChange != null) OnTargetsVisibilityChange(visibleTargets);
    }

    // 시야 메시 그리기
    void DrawFieldOfView()
    {
        float stepAngleSize = viewAngle / meshResolution;
        List<Vector3> viewPoints = new List<Vector3>();
        ObstacleInfo oldObstacle = new ObstacleInfo();

        for (int i = 0; i <= meshResolution; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ObstacleInfo newObstacle = FindObstacles(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldObstacle.dst - newObstacle.dst) > edgeDstThreshold;

                // 이전 장애물과 현재 장애물이 다를 때 또는 장애물을 만나는 거리가 임계값을 초과할 때 가장자리 찾기
                if (oldObstacle.hit != newObstacle.hit ||
                    (oldObstacle.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldObstacle, newObstacle);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newObstacle.point);
            oldObstacle = newObstacle;
        }

        int vertexCount = viewPoints.Count + 1;
        var vertices = new Vector3[vertexCount];
        var triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    // 장애물의 가장자리 찾기
    EdgeInfo FindEdge(ObstacleInfo minObstacle, ObstacleInfo maxObstacle)
    {
        float minAngle = minObstacle.angle;
        float maxAngle = maxObstacle.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ObstacleInfo newObstacle = FindObstacles(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minObstacle.dst - newObstacle.dst) > edgeDstThreshold;

            if (newObstacle.hit == minObstacle.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newObstacle.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newObstacle.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    // 특정 각도에서의 장애물 찾기
    ObstacleInfo FindObstacles(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (DebugRayCast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ObstacleInfo(true, hit.point + hit.normal * -viewDepth, hit.distance, globalAngle);
        }

        // 장애물이 없을 경우 시야 최대 범위까지 지점 반환
        return new ObstacleInfo(false, transform.position + dir * (viewRadius - viewDepth), viewRadius, globalAngle);
    }

    // 디버그 레이캐스트 및 레이캐스트 결과 반환
    bool DebugRayCast(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDistance, int mask)
    {
        if (Physics.Raycast(origin, direction, out hit, maxDistance, mask))
        {
            if (debug)
                Debug.DrawLine(origin, hit.point);
            return true;
        }
        if (debug)
            Debug.DrawLine(origin, origin + direction * maxDistance);
        return false;
    }

    // 각도를 벡터로 변환
    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // 장애물 정보 구조체
    public struct ObstacleInfo
    {
        public bool hit;          // 장애물에 부딪힌 경우 true
        public Vector3 point;     // 부딪힌 지점
        public float dst;         // 거리
        public float angle;       // 각도

        public ObstacleInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    // 가장자리 정보 구조체
    public struct EdgeInfo
    {
        public Vector3 pointA;    // 첫 번째 가장자리 지점
        public Vector3 pointB;    // 두 번째 가장자리 지점

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
