using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SB_CaitylnMoving : Unit
{
    Animator animator;
    public static bool caitylnMoving = false; // 이동 시 자동 평타 종료
    public static bool normalAct = false; // 평타 중 개입 금지
    public static bool skillAct = false; // 스킬 애니메이션 중 개입 금지

    SB_CaitylnQ caitylnQ; // Q
    SB_CaitylnW caitylnW; // W
    SB_CaitylnE caitylnE; // E
    SB_CaitylnR caitylnR; // R

    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

        base.Init();

        animator = GetComponent<Animator>();
        caitylnQ = transform.GetComponent<SB_CaitylnQ>();
        caitylnW = transform.GetComponent<SB_CaitylnW>();
        caitylnE = transform.GetComponent<SB_CaitylnE>();
        caitylnR = transform.GetComponent<SB_CaitylnR>();
    }

    protected override void UpdateMove()
    {
        if (!skillAct)
        {
            caitylnMoving = true;
            animator.SetBool("Run", true);
            base.UpdateMove();
        }
    }

    protected override void UpdateIdle()
    {
        if (!skillAct && !normalAct)
        {
            caitylnMoving = false;
            animator.SetBool("Run", false);
            base.UpdateIdle();
        }
    }

    protected override void CastActiveQ() // 필트오버 피스메이커
    {
        
        if (!skillAct)
        {
            CurrentState = Define.UnitState.IDLE;
            caitylnQ.SkillQ();

            base.CastActiveQ();
        }
    }

    protected override void CastActiveW() // 요들잡이 덫 
    {
        if (!skillAct)
        {
            CurrentState = Define.UnitState.IDLE;
            caitylnW.SkillW();

            base.CastActiveW();
        }
    }

    protected override void CastActiveE() // 90구경 투망
    {
        if (!skillAct)
        {
            CurrentState = Define.UnitState.IDLE;
            caitylnE.SkillE();

            base.CastActiveE();
        }
    }

    protected override void CastActiveR() // 비장의 한 발
    {
        if (!skillAct)
        {
            CurrentState = Define.UnitState.IDLE;
            caitylnR.SkillR();

            // 스킬 구현
            base.CastActiveR();
        }
    }
}
