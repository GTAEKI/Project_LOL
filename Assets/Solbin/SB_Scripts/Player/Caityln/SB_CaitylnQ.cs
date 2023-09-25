using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SB_CaitylnQ : MonoBehaviour
{
    Transform caityln;

    Animator caitylnAnimator; // 캐릭터 
    Animator armorAnimator; // 총알

    GameObject qAttackPrefab; // q스킬 프리팹
    GameObject qAttack; // q스킬 총알

    bool isAttack = false;
    bool bulletFire = false; // 총알 발사

    Camera camera;
    Vector3 targetPosition; // 공격이 향할 마우스 포지션

    private void Start()
    {
        caityln = transform;
        caitylnAnimator = caityln.GetComponent<Animator>();

        qAttackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Solbin/SB_Prefabs/Attack_Q.prefab", typeof(GameObject));

        qAttack = Instantiate(qAttackPrefab);
        qAttack.transform.position = new Vector3(caityln.position.x, 2.5f, caityln.position.z);
        qAttack.transform.rotation = caityln.rotation;

        armorAnimator = qAttack.transform.GetComponent<Animator>();

        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        Debug.Assert(camera != null);
    }

    public void SkillQ()
    {
        if (!isAttack)
        {
            SB_CaitylnMoving.skillAct = true;

            transform.position = caityln.position; // 이동 중일때를 고려하여 케이틀린 정지

            int layerMask = 1 << LayerMask.NameToLayer("Floor"); 

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                targetPosition = hit.point;
            }

            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        isAttack = true; // 중복 키 입력 X

        caitylnAnimator.SetTrigger("PressQ");
        
        Vector3 dir = targetPosition - caityln.position;
        dir.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(dir); // 목표 방향

        yield return new WaitForSeconds(caitylnAnimator.GetCurrentAnimatorClipInfo(0).Length * 0.5f);

        caityln.transform.rotation = targetRotation;
        qAttack.transform.rotation = targetRotation;
        qAttack.transform.position = caityln.position;
        qAttack.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        bulletFire = true; // 총알 발사
        Invoke("UnfoldBullet", 0.5f); // 총알 펼치기 (애니)
        yield return new WaitForSeconds(caitylnAnimator.GetCurrentAnimatorClipInfo(0).Length);

        if (Input.GetMouseButtonDown(1))
        {
            yield break;
        }
        caitylnAnimator.SetTrigger("PressQ_Idle");

        isAttack = false;
        SB_CaitylnMoving.skillAct = false;
    }

    private void UnfoldBullet()
    {
        armorAnimator.enabled = true;
    }

    private void Update()
    {
        if (isAttack && Input.GetMouseButtonDown(1)) // q 중 이동
        {
            isAttack = false;
            StopCoroutine(Fire());
            caitylnAnimator.SetTrigger("PressQ_Run");
            SB_CaitylnMoving.skillAct = false;
        }
    }

    private void FixedUpdate()
    {
        if (bulletFire) // 총알 발사
        {
            qAttack.transform.position = new Vector3(qAttack.transform.position.x, 2.5f, qAttack.transform.position.z);
            qAttack.transform.Translate(Vector3.forward * Time.deltaTime * 20f);

            if (Vector3.Distance(qAttack.transform.position, caityln.position) > 15f)
            {
                bulletFire = false;
                qAttack.transform.rotation = caityln.rotation;
                armorAnimator.Rebind(); // 총알 애니메이션 되감기
                armorAnimator.enabled = false;

                qAttack.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                qAttack.transform.position = new Vector3(0, 0, -10);
                qAttack.transform.position = new Vector3(caityln.position.x, 2.5f, caityln.position.z);
            }
        }
        
    }


}
