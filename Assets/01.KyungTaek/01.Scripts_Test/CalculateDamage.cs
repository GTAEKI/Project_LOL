using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// 투사체가 상대에게 데미지 주는 함수
/// 배경택 _ 230926
/// </summary>
public class CalculateDamage : MonoBehaviourPun
{
    public float damage = default;
    public int ownerNumber = default;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 collision이 비어있지 않다면
        if (collision != null)
        {
            PhotonView photonView = collision.gameObject.GetComponent<PhotonView>();

            // collision에 포톤뷰 컴포넌트가 존재하고, 나 자신이 아니라면
            if (photonView != null && ownerNumber != photonView.Owner.ActorNumber)
            {
                Unit unit = collision.gameObject.GetComponent<Unit>();

                // 충돌 오브젝트가 Unit을 갖고있다면 데미지를 입힌다.
                if (unit != null)
                {
                    unit.CurrentUnitStat.OnDamaged(damage);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌 other가 비어있지 않다면
        if (other != null)
        {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView>();

            // other에 포톤뷰 컴포넌트가 존재하고, 나 자신이 아니라면
            if (photonView != null && ownerNumber != photonView.Owner.ActorNumber)
            {
                Unit unit = other.gameObject.GetComponent<Unit>();

                // other가 Unit을 갖고있다면 데미지를 입힌다.
                if (unit != null)
                {                    
                    unit.CurrentUnitStat.OnDamaged(damage);
                }
            }
        }
    }
}
