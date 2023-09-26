using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

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
    //bool trace = false; // 적을 쫓는 중

    GameObject enemy; // 타겟팅 한 적 챔피언
    GameObject targetEnemy; // 적 저장

    Vector3 beforPos;
    Vector3 afterPos;

    public GameObject autoAttackPrefab;
    GameObject autoAttack;

    SB_CaitylnHitAA hitAA;

    // Start is called before the first frame update
    void Start()
    {
        caityln = transform.parent.gameObject;
        animator = caityln.GetComponent<Animator>();

        autoAttack = Instantiate(autoAttackPrefab);
        autoAttack.transform.position = new Vector3(0, 0, -10);
        autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();

        hitAA = autoAttack.transform.GetChild(0).GetComponent <SB_CaitylnHitAA>();
    }

    /// <summary>
    /// 적이 범위 내 있음
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (!SB_CaitylnMoving.skillAct)
        {
            if (other.tag == "Player") // 평타
            {
                enemy = (GameObject)other.gameObject; // 인식한 적을 담는 임시 컨테이너
                hitAA.GetPlayerName(enemy);
                enemyPoint = enemy.transform.position;
                getTarget = true;

                if (!animator.GetBool("Run") && !isAttack) // 이동, 쫓는 중, 공격 중이 아니라면
                {
                    FindTarget(); // 적 봄
                }
            }
        }

        if (SB_CaitylnMoving.caitylnMoving)
        {
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy = (GameObject)other.gameObject;
            getTarget = false;
            //animator.SetBool("Auto Attack", false);
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
        autoAttack.transform.rotation = targetRotation;

        StartCoroutine(AutoAttack());
    }

    /// <summary>
    /// 범위 내 적에 자동 평타
    /// </summary>
    private IEnumerator AutoAttack()
    {
        SB_CaitylnMoving.normalAct = true;

        isAttack = true;

        targetEnemy = enemy;

        animator.SetBool("Auto Attack", true);

        Vector3 aaPos = caityln.transform.position;
        aaPos.z += 1f;
        aaPos.x += 0.6f;
        aaPos.y += 3f;
        Vector3 bulletPos = caityln.transform.position + caityln.transform.forward * 2f + 
            caityln.transform.up * 3f - caityln.transform.right * 0.5f;
        autoAttack.transform.position = bulletPos;

        yield return null;
        targetPoint = enemyPoint;
        targetPoint.y = 2.5f;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.15f);
        autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
        autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.75f);
        animator.SetBool("Auto Attack", false);
        autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
        isAttack = false; // 다시 평타
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R)) // 마우스 오른쪽 버튼 => 케이틀린이 이동 중
        {
            SB_CaitylnMoving.normalAct = false;
            animator.SetBool("Auto Attack", false);
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
            getTarget = false;
        }
            
        if (SB_CaitylnMoving.caitylnMoving) // 자의로 이동 중이면
        {
            //trace = false;
            animator.SetBool("Auto Attack", false); // 자동 평타 종료 

            if (!getTarget)
            {
                SB_CaitylnMoving.normalAct = false;
                targetEnemy = null;
            }
        }

        if (SB_CaitylnMoving.skillAct || SB_CaitylnMoving.caitylnMoving)
        {
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            autoAttack.transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
        }
    }
}
