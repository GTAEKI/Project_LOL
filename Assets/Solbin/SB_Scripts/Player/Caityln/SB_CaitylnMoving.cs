using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Photon.Pun;

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

    private PhotonView pv;

    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Caityln]);
        unitSkill = new UnitSkill(Define.UnitName.Caityln);

        base.Init();

        animator = gameObject.GetComponent<Animator>();
        caitylnQ = gameObject.transform.GetComponent<SB_CaitylnQ>();
        caitylnW = gameObject.transform.GetComponent<SB_CaitylnW>();
        caitylnE = gameObject.transform.GetComponent<SB_CaitylnE>();
        caitylnR = gameObject.transform.GetComponent<SB_CaitylnR>();

        pv = GetComponent<PhotonView>();
    }

    [PunRPC]
    protected override void UpdateMove()
    {
        if (!skillAct)
        {
            caitylnMoving = true;
            animator.SetBool("Run", true);
            base.UpdateMove();
        }
    }

    [PunRPC]
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
        if (pv.IsMine)
        {
            Debug.Log("Q 입력");

            if (!skillAct)
            {
                CurrentState = Define.UnitState.IDLE;
                caitylnQ.SkillQ();

                base.CastActiveQ();
            }
        }
    }

    protected override void CastActiveW() // 요들잡이 덫 
    {
        if (pv.IsMine)
        {
            if (!skillAct)
            {
                CurrentState = Define.UnitState.IDLE;
                caitylnW.SkillW();

                base.CastActiveW();
            }
        }
    }

    protected override void CastActiveE() // 90구경 투망
    {
        if (pv.IsMine)
        {
            if (!skillAct)
            {
                CurrentState = Define.UnitState.IDLE;
                caitylnE.SkillE();

                base.CastActiveE();
            }
        }
    }

    protected override void CastActiveR() // 비장의 한 발
    {
        if (pv.IsMine)
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
}
