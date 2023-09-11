using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // 추가 >> 폴리곤 메쉬를 구축할 때 정점을 조금 더 뒤로 밀어서 모서리를 보게 하는 방법
    public float maskCutawayDst = 0.1f;

    //시야 영역의 반지름과 시야 각도
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    // 마스크 2종
    public LayerMask targetMask, obstacleMask;

    // Target mask에 ray hit된 transform을 보관하는 리스트
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    // 얻어낸 정점 리스트로 폴리곤 메쉬를 만들어 저장할 변수명
    Mesh viewMesh;
    public MeshFilter viewMeshFilter;


    // 두 점을 간선으로 저장하기 위한 Edge struct
    public struct Edge
    {
        public Vector3 PointA, PointB;
        public Edge(Vector3 _PointA, Vector3 _PointB)
        {
            PointA = _PointA;
            PointB = _PointB;
        }
    }

    // 중간점 ray의 길이 임계치를 정할 변수 edgeDstThreshold와
    // 이진 탐색 반복 횟수 edgeResolveIterations를 public으로 선언
    // 사실 실수 값에 대해 이진 탐색을 진행할 때는 100번이면 충분하다고 알려져 있으므로 변수를 따로 두지 않고 100을 넣어도 된다.
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    Edge FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceed = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if(newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceed)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new Edge(minPoint, maxPoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 얻어낸 정점 리스트로 폴리곤 메쉬를 만들어 저장할 변수명 초기화
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    // y축 오일러 각을 3차원 방향 벡터로 변환한다.
    // 원본과 구현이 살짝 다름에 주의. 결과는 같다.
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

    void DrawFieldOfView()
    {
        //// 샘플링할 점과 샘플링으로 나뉘어지는 각의 크기를 구함
        //int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        //float stepAngleSize = viewAngle / stepCount;

        //// 샘플링한 점으로 향하는 좌표를 계산해 stepCount 만큼의 반직선을 쏨
        //for(int i = 0; i <= stepCount; i++)
        //{
        //    float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
        //    // 여기서는 색상을 초록으로 결정했다.
        //    Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.green);
        //}



        //// Debug.DrawLine 대신 ViewCast로 폴리곤을 구성할 정점을 얻어
        //// viewPoints라는 Vector3리스트에 정점들을 넣을 수 있도록 한다.
        //// 해당내용을 DrawFieldOfView에 반영한 부분       
        //int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        //float stepAngleSize = viewAngle / stepCount;
        //List<Vector3> viewPoints = new List<Vector3>();

        //for(int i = 0; i <= stepCount; i++)
        //{
        //    float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;

        //    ViewCastInfo newViewCast = ViewCast(angle);
        //    viewPoints.Add(newViewCast.point);
        //}

        //int vertexCount = viewPoints.Count + 1;
        //Vector3[] vertices = new Vector3[vertexCount];
        //int[] triangles = new int[(vertexCount - 2) * 3];
        //vertices[0] = Vector3.zero;

        //for(int i = 0; i < vertexCount -1; i++)
        //{
        //    vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
        //    if(i < vertexCount - 2)
        //    {
        //        triangles[i * 3] = 0;
        //        triangles[i * 3 + 1] = i + 1;
        //        triangles[i * 3 + 2] = i + 2;
        //    }
        //}
        //viewMesh.Clear();
        //viewMesh.vertices = vertices;
        //viewMesh.triangles = triangles;
        //viewMesh.RecalculateNormals();

        //DrawFieldOfView에서 반복마다 FindEdge를 호출하여 정점 보간을 하도록 메서드를 고친다.
        // 최종 DrawFieldOfView 코드
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo prevViewCast = new ViewCastInfo();

        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            //i 가 0이면 prevViewCast에 아무 값이 없어 정점 보간을 할 수 없으므로 건너 뛴다.
            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewCast.dst) > edgeDstThreshold;

                //둘 중 한 raycast가 장애물을 만나지 않았거나 두 raycast가 서로 다른 장애물에 hit 된 것이라면 (edgeDstThresholdExceed 여부로 계산)
                if(prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);

                    // zero가 아닌 정점을 추가함
                    if(e.PointA != Vector3.zero)
                    {
                        viewPoints.Add(e.PointA);
                    }

                    if(e.PointB != Vector3.zero)
                    {
                        viewPoints.Add(e.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            prevViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount -1; i++)
        {
            //모서리 안보이는 계산
            //vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            //모서리 보이는 계산
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * maskCutawayDst;
            if(i<vertexCount - 2)
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

    private void LateUpdate()
    {
        DrawFieldOfView();
    }
}
