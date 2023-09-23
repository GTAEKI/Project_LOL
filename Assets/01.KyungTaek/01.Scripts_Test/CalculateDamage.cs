using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDamage : MonoBehaviour
{
    public float damage = default;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("스킬 데미지 스크립트 실행");
        collision.transform.GetComponent<Unit>().CurrentUnitStat.OnDamaged(damage);
    }
}
