using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_CaitylnQ : MonoBehaviour
{
    Animator caitylnAnimator; // 캐릭터 
    Animator armorAnimator; // 총알

    private void Start()
    {
        caitylnAnimator = transform.parent.GetComponent<Animator>();
        armorAnimator = transform.GetComponent<Animator>();
    }

    public void SkillQ()
    {
        Debug.Log("확인 완료");
    }
}
