using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CalculateDamage : MonoBehaviourPun
{
    public float damage = default;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject != photonView.IsMine) && collision != null)
        {
            Debug.Log("스킬 데미지 스크립트 실행");
            collision.transform.GetComponent<Unit>().CurrentUnitStat.OnDamaged(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject != photonView.IsMine) && other != null && other.transform.GetComponent<Unit>())
        {
            Debug.Log("트리거 스킬 데미지 스크립트 실행");
            other.transform.GetComponent<Unit>().CurrentUnitStat.OnDamaged(damage);
        }
    }
}
