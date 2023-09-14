using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템(비전 탐지기) 설치 가능 사정거리
/// </summary>

public class SB_FirstArcaneRange : MonoBehaviour
{
    GameObject firstRange; // 비전탐지기 설치 가능 범위 오브젝트
    bool activeFalse = false;

    // Start is called before the first frame update
    void Start()
    {
        firstRange = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha4)) // 범위 활성화
            firstRange.SetActive(true);
        else
            firstRange.SetActive(false);
    }


    public void ActiveFalse()
    {
        activeFalse = true;
    }
}
