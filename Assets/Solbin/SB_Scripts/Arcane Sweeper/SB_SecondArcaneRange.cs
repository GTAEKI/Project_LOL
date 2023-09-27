using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템(비전 탐지기) 효과 사정거리
/// </summary>

public class SB_SecondArcaneRange : MonoBehaviour
{
    //GameObject secondRange; // 비전탐지기 영향 범위 오브젝트
    //GameObject arcaneSweeper; // 비전탐지기

    //// Start is called before the first frame update
    //void Start()
    //{
    //    secondRange = transform.GetChild(0).gameObject;
    //    arcaneSweeper = secondRange.transform.GetChild(0).gameObject;
    //}

    //// Update is called once per frame  
    //void Update()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) &&
    //        !SB_UseArcaneSweeper.arcaneUsed)
    //    {
    //        if (Vector3.Distance(hit.point, transform.position) < 6) // 12는 매직넘버
    //        {
    //            Vector3 rangePosition = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
    //            Vector3 localPosition = transform.InverseTransformPoint(rangePosition);
    //            secondRange.transform.localPosition = localPosition;
    //        }
    //    }
    //}
}
