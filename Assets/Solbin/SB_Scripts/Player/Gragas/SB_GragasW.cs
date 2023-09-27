using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SB_GragasW : MonoBehaviour
{
    Animator animator;
    private bool isAttack = false;
    PhotonView pv;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();    
    }
    public void SkillW()
    {
        StartCoroutine(Drinking());
    }

    private IEnumerator Drinking()
    {
        SB_GragasMoving.gragasSkill = true;
        isAttack = true;
        animator.SetTrigger("PressW_1");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger("PressW_2");
        yield return new WaitForSeconds(1);
        //animator.SetTrigger("Back Idle");
        pv.RPC("AnimationWSync", RpcTarget.All);
        isAttack = false;
        SB_GragasMoving.gragasSkill = false;
    }

    [PunRPC]
    private void AnimationESync()
    {
        animator.SetTrigger("Back Idle");
    }
}
