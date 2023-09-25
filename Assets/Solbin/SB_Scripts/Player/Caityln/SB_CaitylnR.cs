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
    bool fireBullet = false; // 총알 발사
    bool traceBullet = false; // 유도탄 기능

    Vector3 targetPosition; // 타겟팅 위치 (적 위치)
    GameObject enemy; // 적

    GameObject rEffectPrefab; // 타겟팅 이펙트 프리팹
    GameObject rEffect; // 타겟팅 이펙트
    GameObject rBulletPrefab;
    GameObject rBullet;

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

        rBulletPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Solbin/SB_Prefabs/Attack_R.prefab", typeof(GameObject));
        rBullet = Instantiate(rBulletPrefab);
        rBullet.transform.position = new Vector3(0, 0, -10);
    }

    public void SkillR()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Player" && hit.collider.gameObject.name != "Caityln")
            {
                enemy = hit.collider.gameObject;
                targetPosition = hit.point;

                if (!isAttack)
                {
                    transform.GetChild(2).GetComponent<SB_CaitylnAutoAttack>().enabled = false;
                    animator.SetBool("Auto Attack", false);

                    targetPosition = enemy.transform.position;

                    StartCoroutine(Targeting());
                }
            }
        }
    }

    private IEnumerator Targeting()
    {
        isAttack = true;

        SB_CaitylnMoving.skillAct = true;
        animator.SetTrigger("PressR");

        yield return new WaitForSeconds(0.8f);
        animator.enabled = false;
        drawLaser = true;
        yield return new WaitForSeconds(1);
        animator.enabled = true;
        drawLaser = false;
        // 발사 시점 
        Vector3 bulletPos = caityln.position;
        //bulletPos.z += 2f;
        //bulletPos.y = 2.5f;
        rBullet.transform.position = bulletPos; 
        rBullet.transform.GetComponent<ParticleSystem>().Play();
        fireBullet = true;
        Invoke("TraceBullet", 1f);

        animator.SetTrigger("PressR_Idle");
        rEffect.transform.position = new Vector3(0, 0, -10);

        isAttack = false;
        SB_CaitylnMoving.skillAct = false;
    }

    private void TraceBullet()
    {
        traceBullet = true;
    }

    private void Update()
    {
        if (isAttack) // 만약 타겟팅 중이라면
        {
            targetPosition = enemy.transform.position;

            Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            transform.LookAt(lookAtPosition);

            if (Input.GetMouseButtonDown(1))
            {
                isAttack = false;
                drawLaser = false;
                StopCoroutine(Targeting());
                animator.enabled = true;
                animator.SetTrigger("PressR_Run");

                SB_CaitylnMoving.skillAct = false;
            }
        }

        if (drawLaser) // 조준 레이저
        {
            lineRenderer.enabled = true;

            Vector3 targetEffectPos = targetPosition;
            targetEffectPos.y = 4.8f;
            rEffect.transform.position = targetEffectPos;

            Vector3 lagerPos = targetPosition;
            lagerPos.y = 1f; // 레이저 고도

            Vector3 caitylnPos = transform.position + transform.forward * 2.5f;
            caitylnPos.y = 2.5f;

            Vector3 direction = lagerPos - caitylnPos;
            Ray ray = new Ray(caitylnPos, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Player")
            {
                lineRenderer.SetPosition(0, new Vector3(caitylnPos.x + 0.15f, caitylnPos.y, caitylnPos.z));

                Vector3 laserDes = new Vector3(hit.point.x, 2.5f, hit.point.y);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
            lineRenderer.enabled = false;
        

        if (fireBullet) // 총알 발사
        {
            targetPosition = enemy.transform.position;

            if (!traceBullet)
            {
                rBullet.transform.Translate(transform.forward * Time.deltaTime * 30f);
            }

            if (traceBullet)
            {
                Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                transform.LookAt(lookAtPosition);

                rBullet.transform.LookAt(lookAtPosition);

                rBullet.transform.Translate(transform.forward * Time.deltaTime * 20f);
            }
        }
    }
}
