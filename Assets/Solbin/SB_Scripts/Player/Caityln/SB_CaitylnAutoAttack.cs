using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 케이틀린: 평타
/// </summary>

public class SB_CaitylnAutoAttack : MonoBehaviour
{
    GameObject caityln; // 케이틀린
    Animator animator;
    Vector3 enemyPoint;

    bool getTarget = false; // 적이 범위 내에 있는지 체크
    bool isAttack = false; // 공격 진행 중 

    // Start is called before the first frame update
    void Start()
    {
        caityln = transform.parent.gameObject;
        animator = caityln.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 적이 범위 내 있음
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        int hitLayer = other.gameObject.layer;
        if (hitLayer == LayerMask.NameToLayer("Target")) // 평타
        {
            Debug.Log("적 감지");
            enemyPoint = other.transform.position;

            if (!animator.GetBool("Run") && !isAttack) // 만약 달리는 중이 아니라면
            {
                FindTarget(); // 적 봄
                StartCoroutine(AutoAttack()); // 평타
            }
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

        float rotationSpeed = 10f; // 회전 속도
        caityln.transform.rotation = // 부드럽게 회전
            Quaternion.Slerp(caityln.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 범위 내 적에 자동 평타
    /// </summary>
    private IEnumerator AutoAttack()
    {
        isAttack = true;
        animator.SetTrigger("Auto Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttack = false; // 다시 평타
    }
}
