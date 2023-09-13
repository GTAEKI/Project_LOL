using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_FirstArcaneRange : MonoBehaviour
{
    GameObject firstRange; // 비전탐지기 설치 가능 범위 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        firstRange = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //설치 범위 활성화 => 해당 부분에 마우스를 가져다 대면 다시 아이템의 효과 범위 활성화
        // => 클릭 시 아이템 적용

        if (Input.GetKey(KeyCode.Alpha4)) // 범위 활성화
            firstRange.SetActive(true);
        else 
            firstRange.SetActive(false);
    }
}
