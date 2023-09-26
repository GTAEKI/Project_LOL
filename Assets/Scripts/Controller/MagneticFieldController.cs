using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldController : MonoBehaviour
{
    private float moveMaxHeight;            // 이동 최대치
    private float nextTargetHeight;         // 다음 이동 높이
    private int magneticCount = 0;          // 자기장 스택
    private float moveTime = 0f;            // 이동 시간
    private float moveTimeMax = 3f;         // 이동하는데 걸리는 시간

    public int MagneticCount
    {
        get => magneticCount;
        set
        {
            magneticCount = value;

            if(magneticCount <= FULLSTACK)
            {   // 자기장 최대 스택이 아닐 경우
                nextTargetHeight = moveMaxHeight * magneticCount;
                StartCoroutine(CoroutineMove());
            }
        }
    }

    private const int FULLSTACK = 4;                // 최대 스택 
    private const float HEIGHTMAX = 28f;            // 최대 높이

    /// <summary>
    /// 자기장 생성 함수
    /// 김민섭_230926
    /// </summary>
    public void Generate()
    {
        transform.localPosition = Vector3.zero;                 // 자기장 위치 초기화
        moveMaxHeight = HEIGHTMAX / FULLSTACK;                  // 이동 최대치
        nextTargetHeight = moveMaxHeight * magneticCount;       // 다음 이동 높이
        gameObject.SetActive(true);

        StartCoroutine(CoroutineMove());
    }

    /// <summary>
    /// 자기장 이동 코루틴 함수
    /// 김민섭_230926
    /// </summary>
    private IEnumerator CoroutineMove()
    {
        moveTime = 0f;

        while(true)
        {
            float t = moveTime / moveTimeMax;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, nextTargetHeight, 0f), t);

            moveTime += Time.deltaTime * 0.01f;
            yield return null;

            float distance = nextTargetHeight - transform.localPosition.y;
            if (distance <= 0.01f)
            {
                MagneticCount++;
                yield break;
            }
        }
    }

    /// <summary>
    /// 자기장 종료 함수
    /// 김민섭_230926
    /// </summary>
    public void Clear()
    {
        MagneticCount = 0;
        gameObject.SetActive(false);
    }
}
