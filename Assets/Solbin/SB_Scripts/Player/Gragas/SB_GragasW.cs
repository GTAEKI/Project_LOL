using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasW : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SkillW()
    {
        StartCoroutine(Drinking());
    }

    private IEnumerator Drinking()
    {
        SB_GragasMoving.gragasSkill = true;
        animator.SetTrigger("PressW_1");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger("PressW_2");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Back Idle");
        SB_GragasMoving.gragasSkill = false;
    }

}
