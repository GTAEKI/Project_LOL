using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasAutoAttack : MonoBehaviour
{
    GameObject gragas;
    GameObject enemy;
    bool trace = false;
    Vector3 targetPos;
    Animator animator;
    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        gragas = transform.parent.gameObject;
        animator = transform.parent.GetComponent<Animator>();
    }

    //private void OnTriggerStay(Collider other) // 테스트를 위해 임시 주석 처리
    //{
    //    if (!SB_GragasMoving.gragasMoving && !SB_GragasMoving.gragasSkill)
    //    {
    //        if (other.tag == "Player" && other.name != "Gragas")
    //        {
    //            enemy = other.gameObject;
    //            targetPos = enemy.transform.position;
    //            gragas.transform.LookAt(targetPos);

    //            trace = true;
    //        }

    //        if (other.name == "Dummy" && other.name != "Gragas") // 테스트용
    //        {
    //            enemy = other.gameObject;
    //            targetPos = enemy.transform.position;
    //            gragas.transform.LookAt(targetPos);

    //            trace = true;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player" && other.name != "Gragas")
    //    {
    //        enemy = null; // 범위에서 벗어났다면 공격 대상에서 제외
    //        trace = false;
    //    }

    //    if (other.name == "Dummy" && other.name != "Gragas") // 테스트용
    //    {
    //        enemy = null;
    //        trace = false;
    //    }
    //}

    private void FixedUpdate()
    {
        if (trace)
        {
            if (Vector3.Distance(gragas.transform.position, targetPos) > 3)
            {
                animator.SetTrigger("Back Run");
                gragas.transform.Translate(Vector3.forward * Time.deltaTime * 8f);
            }

            else if (Vector3.Distance(gragas.transform.position, targetPos) <= 3)
            {
                trace = false;

                if (!isAttack)
                {
                    StartCoroutine(AutoAttack());
                }
            }
        }
    }

    private IEnumerator AutoAttack()
    {
        isAttack = true;
        animator.SetTrigger("Auto Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (enemy != null)
        {
            enemy.transform.GetComponent<Unit>().CurrentUnitStat.OnDamaged(6.4f);
        }
        animator.SetTrigger("Back Idle");
        yield return new WaitForSeconds(0.5f); // 지나치게 빠른 연속 공격 X

        isAttack = false;
    }


}
