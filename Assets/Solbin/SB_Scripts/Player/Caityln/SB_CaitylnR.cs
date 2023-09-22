using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SB_CaitylnR : MonoBehaviour
{
    Transform caityln; // 케이틀린
    Animator animator;

    bool isAttack = false; // 공격을 시작했는가
    bool drawLaser = false; // 레이저 그리기

    Vector3 targetPosition; // 타겟팅 위치 (적 위치)
    GameObject enemy; // 적

    GameObject rEffectPrefab; // 타겟팅 이펙트 프리팹
    GameObject rEffect; // 타겟팅 이펙트

    private Camera camera;
    private LineRenderer lineRenderer;

    private void Start()
    {
        caityln = transform;
        animator = caityln.GetComponent<Animator>();
        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        lineRenderer = caityln.GetComponent <LineRenderer>();
        lineRenderer.enabled = false;

        rEffectPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Solbin/SB_Prefabs/rEffect.prefab", typeof(GameObject));
        rEffect = Instantiate(rEffectPrefab);
        rEffect.transform.position = new Vector3(0, 0, -10);
    }

    public void SkillR()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Player")
        {
            enemy = hit.collider.gameObject;
            targetPosition = hit.point;

            Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            transform.LookAt(lookAtPosition);

            if (!isAttack)
            {
                transform.GetChild(2).GetComponent<SB_CaitylnAutoAttack>().enabled = false;
                animator.SetBool("Auto Attack", false);

                targetPosition = enemy.transform.position;

                StartCoroutine(Targeting());
            }
        }
    }

    private IEnumerator Targeting()
    {
        isAttack = true;

        SB_CaitylnMoving.skillAct = true;
        animator.SetTrigger("PressR");
        Vector3 targetEffectPos = targetPosition;
        targetEffectPos.y = 4.8f;
        rEffect.transform.position = targetEffectPos;

        yield return new WaitForSeconds(0.8f);
        animator.enabled = false;
        drawLaser = true;
        yield return new WaitForSeconds(1);
        animator.enabled = true;
        drawLaser = false;

        animator.SetBool("PressR_Idle", true);
        drawLaser = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("PressR_Idle", false);

        isAttack = false;
        SB_CaitylnMoving.skillAct = false;

        Debug.Log("끝까지 돌았다");

    }

    private void Update()
    {
        if (drawLaser)
        {
            lineRenderer.enabled = true;

            Vector3 lagerPos = targetPosition;
            lagerPos.y = 1f; // 레이저 고도

            Vector3 caitylnPos = transform.position + transform.forward * 2.5f;
            caitylnPos.y = 2.5f;

            Vector3 direction = lagerPos - caitylnPos;
            Ray ray = new Ray(caitylnPos, direction);
            RaycastHit hit;

            Color laserColor = Color.blue;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Player")
            {
                lineRenderer.SetPosition(0, caitylnPos);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.material.color = laserColor;
            }
        }
        else
            lineRenderer.enabled = false;
        
    }
}
