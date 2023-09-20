using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SB_CaitylnMoving : Unit
{
    Animator animator;
    public static bool caitylnMoving = false;
    SB_CaitylnQ caitylnQ; // Q

    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

        base.Init();

        animator = GetComponent<Animator>();
        caitylnQ = transform.GetChild(2).GetComponent<SB_CaitylnQ>();
    }

    protected override void UpdateMove()
    {
        caitylnMoving = true;

        animator.SetBool("Run", true);

        base.UpdateMove();
    }

    protected override void UpdateIdle()
    {
        caitylnMoving = false;

        animator.SetBool("Run", false);
        base.UpdateIdle();
    }

    protected override void CastActiveQ() // 필트오버 피스메이커
    {
        caitylnQ.SkillQ();

        base.CastActiveQ();
    }

    protected override void CastActiveW() // 요들잡이 덫 
    {
        // 스킬 구현
        base.CastActiveE();
    }

    protected override void CastActiveE() // 90구경 투망
    {
        // 스킬 구현
        base.CastActiveE();
    }

    protected override void CastActiveR() // 비장의 한 발
    {
        // 스킬 구현
        base.CastActiveE();
    }
}
