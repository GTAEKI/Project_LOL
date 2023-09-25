using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 유도 화살, 기본 공격시 화살이 상대를 따라감
/// 230923 _ 배경택
/// </summary>
public class GuidedArrow : MonoBehaviour
{
    public GameObject enemy;
    public int speed = 10;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if( enemy != null)
        {
            transform.position = Vector3.Lerp(transform.position, enemy.transform.position, speed * Time.deltaTime); 
        }
    }
}
