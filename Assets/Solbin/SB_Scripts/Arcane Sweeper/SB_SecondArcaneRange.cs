using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_SecondArcaneRange : MonoBehaviour
{
    GameObject secondRange; // 비전탐지기 영향 범위 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        secondRange = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -0.03f;

        secondRange.transform.position = mousePosition;
    }
}
