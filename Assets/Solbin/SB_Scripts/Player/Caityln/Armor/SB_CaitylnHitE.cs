using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 케이틀린 e스킬 충돌 판정, 사거리 이펙트
/// </summary>

public class SB_CaitylnHitE : MonoBehaviourPun
{
    GameObject blueEffect; // E스킬 충돌 시 생기는 이펙트
    bool oneCheck = false; // 중복 작동 금지
    Vector3 targetPoint; // 충돌 시 위치
    GameObject caityln;

    private void Start()
    {
        Vector3 objectPool = new Vector3(0, 0, -10);
        string blueEffectPath = "Prefabs/Caityln/eEffect";

        GameObject[] caitylnObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in caitylnObjects)
        {
            if (obj.name == "Caityln" && obj.GetComponent<PhotonView>().IsMine)
            {
                caityln = obj;

                Debug.Assert(caityln != null);
                break;
            }
        }

        blueEffect = PhotonNetwork.Instantiate(blueEffectPath, objectPool, Quaternion.identity);
        blueEffect.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        blueEffect.transform.position = new Vector3(0, 0, -10);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !oneCheck && !other.GetComponent<PhotonView>().IsMine)
        {
            other.gameObject.GetComponent<Unit>().CurrentUnitStat.OnDamaged(10);

            oneCheck = true;

            targetPoint = transform.position;

            if (oneCheck)
            {
                StartCoroutine(GetEffect());
            }
        }
    }

    private IEnumerator GetEffect()
    {
        Debug.Assert(blueEffect);
        Debug.Assert(caityln);

        blueEffect.transform.rotation = caityln.transform.rotation;
        blueEffect.transform.position = targetPoint;
        blueEffect.transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(2); // 이펙트 유지 시간
        blueEffect.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        blueEffect.transform.position = new Vector3(0, 0, -10);

        oneCheck = false;
    }
}
