//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor;
//using UnityEngine;

//public class SB_CaitylnE : MonoBehaviour
//{
//    Transform caityln;

//    Animator caitylnAnimator; // 캐릭터 
//    Animator armorAnimator; // 총알

//    GameObject eAttackPrefab; // q스킬 프리팹
//    GameObject eAttack; // q스킬 총알

//    bool isAttack = false;
//    bool bulletFire = false; // 총알 발사
//    bool knockback = false; // 넉백

//    Camera camera;
//    Vector3 targetPosition; // 공격이 향할 마우스 포지션

//    private void Start()
//    {
//        caityln = transform;
//        caitylnAnimator = caityln.GetComponent<Animator>();

//        eAttackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
//            ("Assets/Solbin/SB_Prefabs/Attack_E.prefab", typeof(GameObject));

//        eAttack = Instantiate(eAttackPrefab);
//        eAttack.transform.position = new Vector3(caityln.position.x, 2.5f, caityln.position.z);
//        eAttack.transform.rotation = caityln.rotation;

//        armorAnimator = eAttack.transform.GetComponent<Animator>();

//        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();
//        Debug.Assert(camera != null);
//    }

//    public void SkillE()
//    {
//        if (!isAttack)
//        {
//            int layerMask = 1 << LayerMask.NameToLayer("Floor");

//            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
//            {
//                targetPosition = hit.point;
//            }

//            StartCoroutine(Fire());
//        }
//    }

//    private IEnumerator Fire()
//    {
//        SB_CaitylnMoving.skillAct = true;

//        isAttack = true; // 중복 키 입력 X

//        Invoke("UnfoldNet", 0.5f); // 총알 펼치기 (애니)
//        caitylnAnimator.SetTrigger("PressE");
//        knockback = true;
//        Vector3 dir = targetPosition - caityln.position;
//        dir.y = 0f;

//        Quaternion targetRotation = Quaternion.LookRotation(dir); // 목표 방향

//        caityln.transform.rotation = targetRotation;
//        eAttack.transform.rotation = targetRotation;
//        Vector3 netPos = caityln.position;
//        netPos.y += 2f;
//        eAttack.transform.position = netPos;
//        eAttack.transform.localScale = new Vector3(2f, 2f, 2f);
//        bulletFire = true; // 총알 발사
//        knockback = true; // 넉백

//        yield return new WaitForSeconds(caitylnAnimator.GetCurrentAnimatorClipInfo(0).Length);
//        caitylnAnimator.SetTrigger("PressE_Idle");
//        isAttack = false;
//        SB_CaitylnMoving.skillAct = false;
//    }

//    private void UnfoldNet()
//    {
//        armorAnimator.enabled = true;
//    }

//    private void FixedUpdate()
//    {
//        if (bulletFire)
//        {
//            eAttack.transform.Translate(Vector3.forward * Time.deltaTime * 20f);

//            if (Vector3.Distance(eAttack.transform.position, caityln.position) > 15f)
//            {
//                bulletFire = false;
//                eAttack.transform.rotation = caityln.rotation;
//                armorAnimator.Rebind(); // 총알 애니메이션 되감기
//                armorAnimator.enabled = false;

//                eAttack.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
//                eAttack.transform.position = new Vector3(0, 0, -10);
//            }
//        }

//        if (knockback)
//        {
//            knockback = false;

//            // 넉백
//            float duration = 0.6f; // 넉백 지속 시간
//            Vector3 targetPosition = caityln.transform.position - caityln.transform.forward * 3.0f; // 목표 위치

//            float elapsedTime = 0f;

//            while (elapsedTime < duration)
//            {
//                elapsedTime += Time.deltaTime;
//                float t = Mathf.Clamp01(elapsedTime / duration); // 보간에 사용할 시간 비율
//                caityln.transform.position = Vector3.Lerp(caityln.transform.position, targetPosition, t);
//            }
//        }
//    }


//}

