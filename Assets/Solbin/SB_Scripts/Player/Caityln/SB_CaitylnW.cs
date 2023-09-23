using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SB_CaitylnW : MonoBehaviour
{
    bool isAttack = false;
    bool caitylnMove = false; // 트랩 설치를 위한 케이틀린 이동
    Camera camera;
    Vector3 targetPosition;
    Animator animator;

    GameObject range; // 케이틀린 범위
    GameObject trapPrefab;
    GameObject[] traps; // 트랩

    SphereCollider rangeCollider;

    int trapNumber; // 사용 중 폭탄 인덱스

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        range = transform.GetChild(2).gameObject;

        animator = transform.GetComponent<Animator>();

        trapPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Solbin/SB_Prefabs/Attack_W.prefab", typeof(GameObject));
        traps = new GameObject[3]; // 덫 최대 3개
        for (int i = 0; i < 3; i++)
        {
            traps[i] = Instantiate(trapPrefab);
            traps[i].transform.GetComponent<Animator>().enabled = false;
            traps[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }

        rangeCollider = range.transform.GetComponent<SphereCollider>();
    }

    public void SkillW()
    {
        if (!isAttack) // Range 콜라이더에 충돌한 상태에서 Floor까지 충돌.
        {
            int layerMask = 1 << LayerMask.NameToLayer("Floor");

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.gameObject == range) // 스킬 사용 가능 범위 충돌
                {
                    RaycastHit floorHit;
                    if (Physics.Raycast(ray, out floorHit, Mathf.Infinity, layerMask)) // 맵 바닥 충돌
                    {
                        targetPosition = floorHit.point;

                        Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                        transform.LookAt(lookAtPosition);

                        StartCoroutine(MountingTrap()); // 범위 내 클릭 시 트랩 설치
                    }
                }

                else if (hit.collider.gameObject != range)
                {
                    RaycastHit onlyFloorHit;
                    if (Physics.Raycast(ray, out onlyFloorHit, Mathf.Infinity, layerMask)) // 범위 바깥 클릭
                    {
                        targetPosition = onlyFloorHit.point;

                        Vector3 lookAtPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                        transform.LookAt(lookAtPosition);

                        SB_CaitylnMoving.skillAct = true;
                        caitylnMove = true; // 케이틀린의 범위에 닿을 때까지 이동
                    }
                }
            }
        }
    }

    private IEnumerator MountingTrap() // 트랩 설치
    {
        SB_CaitylnMoving.skillAct = true; // 스킬 실행 중임을 알림

        if (trapNumber < 3)
        {
            traps[trapNumber].transform.position = targetPosition;
            traps[trapNumber].transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            trapNumber++; 
        }
        else if (trapNumber >= 3) // 트랩 넘버가 3을 넘기는 순간
        {
            trapNumber = 0; // 트랩 숫자 초기화
            traps[trapNumber].transform.position = targetPosition;
            traps[trapNumber].transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            trapNumber++;
        }

        animator.SetTrigger("PressW");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        animator.SetTrigger("PressW_Idle");

        SB_CaitylnMoving.skillAct = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            StopCoroutine(MountingTrap());
            animator.SetTrigger("PressW_Run");
            SB_CaitylnMoving.skillAct = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (caitylnMove)
        {
            float speed = 5f;
            float inRange = rangeCollider.radius; // 범위 반지름

            animator.SetBool("Run", true);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, targetPosition) <= inRange)
            {
                animator.SetBool("Run", false);
                caitylnMove = false;
                SB_CaitylnMoving.skillAct = false;

                StartCoroutine(MountingTrap());
            }
        }
    }
}
