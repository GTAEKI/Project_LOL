using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 그라가스 Q스킬 넉백, 데미지 처리 
/// </summary>
public class SB_GragasHitQ : MonoBehaviour
{
    private List<Rigidbody> rbList = new List<Rigidbody>();
    private List<Vector3> dirList = new List<Vector3>();
    private List<Vector3> originPosList = new List<Vector3>();
    private List<GameObject> targetList = new List<GameObject>();

    bool apply = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.name != "Gragas")
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            Vector3 originPos = other.gameObject.transform.position;
            Vector3 barrelPos = transform.position;
            barrelPos.y = originPos.y;
            Vector3 dir = other.gameObject.transform.position - barrelPos;
            dir = dir.normalized;
            GameObject target = other.gameObject;

            Debug.LogFormat("Vector: {0}", dir);

            if (playerRigidbody != null)
            {
                originPosList.Add(originPos);
                rbList.Add(playerRigidbody);
                dirList.Add(dir);
                targetList.Add(target);

                apply = true;

                KnockBack();
            }
        }

        if (other.name == "Dummy" && other.name != "Gragas") // 테스트용
        {
            Debug.Log("들어옴");

            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            Vector3 originPos = other.gameObject.transform.position;
            Vector3 barrelPos = transform.position;
            barrelPos.y = originPos.y;
            Vector3 dir = other.gameObject.transform.position - barrelPos;
            dir = dir.normalized;
            GameObject target = other.gameObject;

            Debug.LogFormat("Vector: {0}", dir);

            if (playerRigidbody != null)
            {
                originPosList.Add(originPos);
                rbList.Add(playerRigidbody);
                dirList.Add(dir);
                targetList.Add(target);

                apply = true;

                KnockBack();
            }
        }
    }

    private void KnockBack()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = dirList[i] * 10f;
        }

        ApplyDamage();
    }

    private void ApplyDamage()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            Debug.Log(targetList[i].name);
            targetList[i].GetComponent<Unit>().CurrentUnitStat.OnDamaged(12f);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (Vector3.Distance(originPosList[i], targetList[i].transform.position) > 3f && apply) // 3 이상 넉백되면
            {
                rbList[i].velocity = Vector3.zero; // 넉백 종료
                apply = false;
            }
        }
    }
}
