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
    bool trace = false; // 적을 쫓는 중

    GameObject autoAttackPrefab; // 자동 평타 총알 프리팹
    GameObject autoAttack; // 자동 평타 총알

    GameObject enemy; // 타겟팅 한 적 챔피언
    GameObject targetEnemy; // 적 저장

    Vector3 beforPos;
    Vector3 afterPos;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        caityln = transform.parent.gameObject;
        animator = caityln.GetComponent<Animator>();

        autoAttackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Solbin/SB_Prefabs/Auto Attack.prefab", typeof(GameObject));

        autoAttack = Instantiate(autoAttackPrefab);
        autoAttack.transform.position = new Vector3(caityln.transform.position.x, 2.5f, caityln.transform.position.z);

        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 적이 범위 내 있음
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") // 평타
        {
            enemy = (GameObject)other.gameObject; // 인식한 적을 담는 임시 컨테이너
            enemyPoint = enemy.transform.position;
            getTarget = true;

            if (!animator.GetBool("Run") && !isAttack) // 이동, 쫓는 중, 공격 중이 아니라면
            {
                FindTarget(); // 적 봄
            }
        }

        if (other.tag == "Player" && trace)
        {
            animator.SetBool("Run", false);
            trace = false;
            getTarget = true;

            if (!animator.GetBool("Run") && !isAttack) // 이동, 쫓는 중, 공격 중이 아니라면
            {
                FindTarget(); // 적 봄
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!animator.GetBool("Run") && other.tag == "Player") // 타의로 적이 벗어남 => 타겟팅 적용
        {
            enemy = (GameObject)other.gameObject;
            getTarget = false;
            animator.SetBool("Auto Attack", false);
        }

        if (other.tag == "Player") // 자의로 적에게서 벗어남 => 타겟팅 미적용
        {
            getTarget = false;
            trace = true;
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

        if (!trace) // 적을 쫓는 중이 아니라면 자동 공격
        {
            animator.SetBool("Auto Attack", true);
            StartCoroutine(AutoAttack());
        }
    }

    /// <summary>
    /// 범위 내 적에 자동 평타
    /// </summary>
    private IEnumerator AutoAttack()
    {
        isAttack = true;

        targetEnemy = enemy;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.2f);

        autoAttack.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        targetPoint = enemyPoint;
        targetPoint.y = 2.5f;

        autoAttack.transform.position = caityln.transform.position;
        bulletFire = true; // 총알 발사

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 0.8f);
        isAttack = false; // 다시 평타
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 => 케이틀린이 이동 중
        {
            getTarget = false;
            SB_CaitylnMoving.skillAct = false;
        }

        if (SB_CaitylnMoving.caitylnMoving) // 자의로 이동 중이면
        {
            trace = false;
            animator.SetBool("Auto Attack", false); // 자동 평타 종료 
            bulletFire = false;

            if (!getTarget)
            {
                SB_CaitylnMoving.skillAct = false;
                targetEnemy = null;
            }
        }

        // 아래 코드가 문제
        //if ((getTarget && !SB_CaitylnMoving.caitylnMoving) || targetEnemy == null) // 범위 내 적이 있거나 적 인식X
        //{
        //    animator.SetBool("Run", false);
        //    trace = false; // 추적 해제
        //    SB_CaitylnMoving.skillAct = true;
        //}

    }

    /// <summary>
    /// 총알 이동
    /// </summary>
    private void FixedUpdate()
    {
        if (Vector3.Distance(autoAttack.transform.position, targetPoint) > 0.1f && bulletFire) // 총알 이동
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

        if (!getTarget && targetEnemy != null && !SB_CaitylnMoving.caitylnMoving) // 적이 범위를 벗어나면 쫓음
        {
            isAttack = false; // 공격 해제

            beforPos = targetEnemy.transform.position;
            Invoke("AfterPosCheck", 0.1f);

            SB_CaitylnMoving.skillAct = true;
            trace = true;
            animator.SetBool("Auto Attack", false);
            animator.SetBool("Run", true);

            Vector3 dir = targetEnemy.transform.position - caityln.transform.position;
            dir.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(dir); // 목표 방향
            caityln.transform.rotation = targetRotation;
            autoAttack.transform.rotation = targetRotation;

            caityln.transform.Translate(Vector3.forward * 3f * Time.deltaTime);
        }
    }

    private void AfterPosCheck()
    {
        afterPos = enemy.transform.position;
    }
}
