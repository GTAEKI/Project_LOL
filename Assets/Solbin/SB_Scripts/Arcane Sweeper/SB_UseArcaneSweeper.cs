using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 비전 탐지기를 작동시키는 스크립트
/// </summary>

public class SB_UseArcaneSweeper : MonoBehaviour
{
    GameObject arcaneSweeper; // 비전 탐지기
    public static bool arcaneUsed = false;
    public UnityEvent arcaneEvent;

    // Start is called before the first frame update
    void Start()
    {
        arcaneSweeper = transform.GetChild(0).gameObject;
        arcaneSweeper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!arcaneUsed && Input.GetMouseButtonDown(0))
        {
            arcaneUsed = true; // Player Range부터 세가지 오브젝트
            arcaneEvent.Invoke(); // 쿨타임
            StartCoroutine(ActiveArcaneSweeper());
        }
    }

    /// <summary>
    /// 5초간 비전 탐지기 구역 활성화
    /// </summary>
    /// <returns></returns>
    IEnumerator ActiveArcaneSweeper()
    {
        arcaneSweeper.SetActive(true);
        // 쿨타임 실행
        yield return new WaitForSeconds(5);
        arcaneSweeper.SetActive(false);
       
        StopCoroutine(ActiveArcaneSweeper());
    }
}
