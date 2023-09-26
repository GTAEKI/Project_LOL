using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SB_GragasQ : MonoBehaviour
{
    public GameObject q1Prefab;
    GameObject q1;
    public GameObject q2Prefab;
    GameObject q2;

    Camera camera;
    Vector3 targetPosition;
    Animator animator;
    Animator q1Animator;
    Animator q2Animator;

    bool isAttack = false;
    Vector3 barrelDes; // q1의 목적지

    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        q1 = Instantiate(q1Prefab);
        q1.transform.position = new Vector3(0, 0, -10);

        q2 = Instantiate(q2Prefab);
        q2.transform.position = new Vector3(0, 0, -10);

        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();

        animator = GetComponent<Animator>();
        q1Animator = q1.GetComponent<Animator>();
        q2Animator = q2.GetComponent<Animator>();
        collider = q2.transform.GetChild(2).GetComponent<Collider>();
    }

    public void SkillQ()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Floor");

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (!SB_GragasMoving.gragasSkill)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && !isAttack)
            {
                targetPosition = hit.point;
                transform.LookAt(targetPosition);
                StartCoroutine(RollingQ());
                SB_GragasMoving.gragasSkill = true;
            }
        }
    }

    private IEnumerator RollingQ()
    {
        isAttack = true;

        q1.transform.rotation = transform.rotation;
        Vector3 firstPos = transform.position + transform.forward * 3.5f;
        firstPos.y = 1.55f;
        q1.transform.position = firstPos;
        barrelDes = q1.transform.position + q1.transform.forward * 7f; // 술통 목적지
        animator.SetTrigger("PressQ");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger("Back Idle");
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            q1Animator.enabled = true;
            q1.transform.position = Vector3.MoveTowards(q1.transform.position, barrelDes, Time.deltaTime * 15f);

            if (Vector3.Distance(q1.transform.position, barrelDes) <= 0.1f)
            {
                isAttack = false;
                q1Animator.enabled = false;
                q1Animator.Rebind();
                q1.transform.position = new Vector3(0, 0, -10);
                q2.transform.position = barrelDes;
                q2Animator.enabled = true;
                StartCoroutine(Bomb());
            }
        }
    }

    private IEnumerator Bomb()
    {
        yield return new WaitForSeconds(q2Animator.GetCurrentAnimatorStateInfo(0).length);
        // 폭발 애니메이션 재생 
        collider.enabled = true;
        q2Animator.enabled = false;
        q2Animator.Rebind();
        q2.transform.position = new Vector3(0, 0, -10);
        // 폭탄 애니메이션 재생 끝날 때까지 대기
        collider.enabled = false;
    }
}
