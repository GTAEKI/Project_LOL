using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SB_GragasMoving : Unit
{
    Animator animator;

    GameObject barrelQPrefab;
    GameObject barrelQ;

    SB_GragasQ gragasQ; // Q스킬
    SB_GragasW gragasW; // W스킬
    SB_GragasE gragasE; // E스킬 
    SB_GragasR gragasR; // R스킬 

    public static bool gragasSkill = false;
    public static bool gragasMoving = false;

    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Gragas]);
        unitSkill = new UnitSkill(Define.UnitName.Gragas);

        animator = GetComponent<Animator>();
        gragasQ = transform.GetComponent<SB_GragasQ>();
        gragasW = transform.GetComponent<SB_GragasW>();
        gragasE = transform.GetComponent<SB_GragasE>();
        gragasR = transform.GetComponent<SB_GragasR>();

        base.Init();   
    }

    protected override void UpdateMove()
    {
        gragasMoving = true;
        animator.SetBool("Run", true);
        base.UpdateMove();
    }

    protected override void UpdateIdle()
    {
        gragasMoving = false;
        animator.SetBool("Run", false);
        base.UpdateIdle();
    }

    protected override void CastActiveQ() // 술통 굴리기
    {
        if (!gragasSkill)
        {
            CurrentState = Define.UnitState.IDLE;
            gragasQ.SkillQ();

            base.CastActiveQ();
        }
    }

    protected override void CastActiveW() // 취중 분노
    {
        if (!gragasSkill)
        {
            CurrentState = Define.UnitState.IDLE;
            gragasW.SkillW();

            base.CastActiveW();
        }
    }

    protected override void CastActiveE() // 몸통 박치기
    {
        if (!gragasSkill)
        {
            CurrentState = Define.UnitState.IDLE;
            gragasE.SkillE();

            base.CastActiveE();
        }
    }

    protected override void CastActiveR() // 술통 폭발
    {
        if (!gragasSkill)
        {
            CurrentState = Define.UnitState.IDLE;
            gragasR.SkillR();

            base.CastActiveR();
        }
    }
}
