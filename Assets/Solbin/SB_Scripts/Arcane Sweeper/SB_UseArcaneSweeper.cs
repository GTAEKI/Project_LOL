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
    GameObject itemRange; // 아이템 효과 범위

    public UnityEvent activeArcaneSweeper;

    // Start is called before the first frame update
    void Start()
    {
        arcaneSweeper = transform.GetChild(0).gameObject;
        itemRange = transform.parent.GetChild(0).transform.GetChild(0).gameObject; //PlayerRange의 자식

        arcaneSweeper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (itemRange.activeSelf && Input.GetMouseButtonDown(0))
        {
            arcaneSweeper.SetActive(true);
            activeArcaneSweeper.Invoke();
            StartCoroutine(ActiveArcaneSweeper());
        }
    }

    /// <summary>
    /// 5초간 비전 탐지기 구역 활성화
    /// </summary>
    /// <returns></returns>
    IEnumerator ActiveArcaneSweeper()
    {
        arcaneSweeper.transform.localPosition = itemRange.transform.localPosition;
        arcaneSweeper.SetActive(true);
        yield return new WaitForSeconds(5);
        arcaneSweeper.SetActive(false);

        StopCoroutine(ActiveArcaneSweeper());
    }
}
