//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

///// <summary>
///// 비전 탐지기를 작동시키는 스크립트
///// </summary>

//public class SB_UseArcaneSweeper : MonoBehaviour
//{
//    GameObject arcaneSweeper; // 비전 탐지기
//    public static bool arcaneUsed = false;
//    SB_FieldOfView fieldOfView;
//    public static event EventHandler useArcane;

//    // Start is called before the first frame update
//    void Start()
//    {
//        arcaneSweeper = transform.GetChild(0).gameObject;
//        fieldOfView = transform.GetChild(0).GetComponent<SB_FieldOfView>(); // 아이템 Arcane Sweeper의 시야

//        fieldOfView.enabled = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!arcaneUsed && Input.GetMouseButtonDown(0))
//        {
//            arcaneUsed = true; // Player Range부터 세가지 오브젝트
//            StartCoroutine(ActiveArcaneSweeper());
//        }
//    }

//    /// <summary>
//    /// 5초간 비전 탐지기 구역 활성화
//    /// </summary>
//    /// <returns></returns>
//    IEnumerator ActiveArcaneSweeper()
//    {
//        fieldOfView.enabled = true;

//        useArcane?.Invoke(this, EventArgs.Empty); // 비전 탐지기 사용 이벤트
//        yield return new WaitForSeconds(5);
//        fieldOfView.enabled = false;

//        StopCoroutine(ActiveArcaneSweeper());
//    }
//}
