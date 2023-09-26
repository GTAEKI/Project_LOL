using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasHitR : MonoBehaviour
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
            rbList[i].velocity = dirList[i] * 15f;
        }

        ApplyDamage();
    }

    private void ApplyDamage()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            targetList[i].GetComponent<Unit>().CurrentUnitStat.OnDamaged(25f);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            if (Vector3.Distance(originPosList[i], targetList[i].transform.position) > 8f && apply) // 3 이상 넉백되면
            {
                rbList[i].velocity = Vector3.zero; // 넉백 종료
                apply = false;
            }
        }
    }
}
