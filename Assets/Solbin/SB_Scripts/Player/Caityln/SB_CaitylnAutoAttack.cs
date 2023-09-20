using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 케이틀린: 평타
/// </summary>

public class SB_CaitylnAutoAttack : MonoBehaviour
{
    GameObject caityln; // 케이틀린
    Animator animator;
    Vector3 enemyPoint;
    Vector3 targetPoint; // 총알 발사 위치

    bool getTarget = false; // 적이 범위 내에 있는지 체크
    bool isAttack = false; // 공격 진행 중 
    bool bulletFire = false; // 총알 이동

    GameObject autoAttackPrefab; // 자동 평타 총알 프리팹
    GameObject autoAttack; // 자동 평타 총알

    // Start is called before the first frame update
    void Start()
    {
        caityln = transform.parent.gameObject;
        animator = caityln.GetComponent<Animator>();

        autoAttackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Solbin/SB_Prefabs/Auto Attack.prefab", typeof(GameObject));

        autoAttack = Instantiate(autoAttackPrefab);
        autoAttack.transform.SetParent(caityln.transform);
        autoAttack.transform.position = new Vector3(caityln.transform.position.x, 2.5f, caityln.transform.position.z);

    }

    /// <summary>
    /// 적이 범위 내 있음
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") // 평타
        {
            enemyPoint = other.transform.position;

            if (!animator.GetBool("Run") && !isAttack) // 만약 달리는 중이 아니라면
            {
                FindTarget(); // 적 봄
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!animator.GetBool("Run") && other.tag == "Player") // 적이 범위에서 벗어나면
        {
            animator.SetBool("Auto Attack", false);
        }
    }

    /// <summary>
    /// 범위 내 적을 바라봄. 
    /// </summary>
    private void FindTarget()
    {
        Vector3 dir = enemyPoint - caityln.transform.position;
        dir.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(dir); // 목표 방향
        caityln.transform.rotation = targetRotation;

        StartCoroutine(AutoAttack());
    }

    /// <summary>
    /// 범위 내 적에 자동 평타
    /// </summary>
    private IEnumerator AutoAttack()
    {
        isAttack = true;
        animator.SetBool("Auto Attack", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.1f);

        autoAttack.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        targetPoint = enemyPoint;
        targetPoint.y = 2.5f;

        bulletFire = true; // 총알 발사

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.9f);
        isAttack = false; // 다시 평타
    }

    private void Update()
    {
        if (SB_CaitylnMoving.caitylnMoving) // 이동 중이면
        {
            animator.SetBool("Auto Attack", false); // 자동 평타 종료 
        }
    }

    /// <summary>
    /// 총알 이동
    /// </summary>
    private void FixedUpdate()
    {
        if (Vector3.Distance(autoAttack.transform.position, targetPoint) > 0.1f && bulletFire)
        {
            autoAttack.transform.position =
                Vector3.MoveTowards(autoAttack.transform.position, targetPoint, Time.deltaTime * 15f);

        }
        else if (Vector3.Distance(autoAttack.transform.position, targetPoint) <= 0.1f && bulletFire)
        {
            bulletFire = false;
            autoAttack.transform.position = new Vector3(caityln.transform.position.x, 2.5f, caityln.transform.position.z);
            autoAttack.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        }
    }
}
