using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SB_GragasE : MonoBehaviour
{
    Animator animator;
    Camera camera;

    Vector3 targetPosition;
    Vector3 gragasDes;

    bool isAttack = false;
    bool apply = false;

    private List<Rigidbody> rbList = new List<Rigidbody>();
    private List<Vector3> dirList = new List<Vector3>();
    private List<Vector3> originPosList = new List<Vector3>();
    private List<GameObject> targetList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();
    }

    public void SkillE()
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

                gragasDes = transform.position + transform.forward * 7f;

                StartCoroutine(Crit());
            }
        }
    }

    private IEnumerator Crit()
    {
        SB_GragasMoving.gragasSkill = true;
        animator.SetTrigger("PressE");
        isAttack = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SB_GragasMoving.gragasSkill = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isAttack && other.name !="Gragas")
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            Vector3 originPos = other.gameObject.transform.position;
            Vector3 gragasPos = transform.position;
            gragasPos.y = originPos.y;
            Vector3 dir = other.gameObject.transform.position - gragasPos;
            dir = dir.normalized;
            GameObject target = other.gameObject;

            if (playerRigidbody != null)
            {
                originPosList.Add(originPos);
                rbList.Add(playerRigidbody);
                dirList.Add(dir);
                targetList.Add(target);

                apply = true;

                KnockBack();
            }
        }

        if (other.name == "Dummy" && isAttack && other.name != "Gragas")
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            Vector3 originPos = other.gameObject.transform.position;
            Vector3 gragasPos = transform.position;
            gragasPos.y = originPos.y;
            Vector3 dir = other.gameObject.transform.position - gragasPos;
            dir = dir.normalized;
            GameObject target = other.gameObject;

            if (playerRigidbody != null)
            {
                originPosList.Add(originPos);
                rbList.Add(playerRigidbody);
                dirList.Add(dir);
                targetList.Add(target);

                apply = true;

                KnockBack();
            }
        }
    }

    private void KnockBack()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = dirList[i] * 10f;
        }

        ApplyDamage();
    }

    private void ApplyDamage()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            targetList[i].GetComponent<Unit>().CurrentUnitStat.OnDamaged(10f);
        }
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, gragasDes, Time.deltaTime * 20f);

            if (Vector3.Distance(transform.position, gragasDes) <= 0.1f)
            {
                isAttack = false;
                animator.SetTrigger("Back Idle");
            } 
        }

        if (apply)
        {
            for (int i = 0; i < rbList.Count; i++)
            {
                if (Vector3.Distance(originPosList[i], targetList[i].transform.position) > 6f) // 6 초과 넉백되면
                {
                    rbList[i].velocity = Vector3.zero; // 넉백 종료
                    apply = false;
                }
            }
        }
    }   
}
