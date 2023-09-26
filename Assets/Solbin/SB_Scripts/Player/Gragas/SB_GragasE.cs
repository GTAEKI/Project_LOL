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
    }
}
