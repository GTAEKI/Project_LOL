using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 케이틀린 e스킬 충돌 판정, 사거리 이펙트
/// </summary>

public class SB_CaitylnHitE : MonoBehaviour
{
    public GameObject blueEffectPrefab; // 충돌 시 이펙트
    GameObject blueEffect;
    bool oneCheck = false; // 중복 작동 금지
    Vector3 targetPoint; // 충돌 시 위치
    GameObject caityln;

    private void Start()
    {
        caityln = GameObject.Find("Caityln");
        Debug.Assert(caityln != null);
        blueEffect = Instantiate (blueEffectPrefab);
        blueEffect.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        blueEffect.transform.position = new Vector3(0, 0, -10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !oneCheck && other.name != "Caityln")
        {
            other.gameObject.GetComponent<Unit>().CurrentUnitStat.OnDamaged(8);

            oneCheck = true;

            targetPoint = transform.position;
            if (oneCheck)
                StartCoroutine(GetEffect());
        }
    }

    private IEnumerator GetEffect()
    {
        blueEffect.transform.rotation = caityln.transform.rotation;
        blueEffect.transform.position = targetPoint;
        blueEffect.transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(2); // 이펙트 유지 시간
        blueEffect.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        blueEffect.transform.position = new Vector3(0, 0, -10);

        oneCheck = false;
    }
}
