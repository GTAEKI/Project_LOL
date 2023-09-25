using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasMoving : Unit
{
    Animator animator;
    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

        animator = GetComponent<Animator>();

        base.Init();   
    }

    protected override void UpdateMove()
    {
        animator.SetBool("Run", true);
        base.UpdateMove();
    }

    protected override void UpdateIdle()
    {
        animator.SetBool("Run", false);
        base.UpdateIdle();
    }

    protected override void CastActiveQ() // 필트오버 피스메이커
    {
        Debug.Log("그라거스 Q 확인");
        animator.SetTrigger("PressQ");
        base.CastActiveQ();
    }

    protected override void CastActiveW() // 요들잡이 덫 
    {
        base.CastActiveW();
    }

    protected override void CastActiveE() // 90구경 투망
    {
        base.CastActiveE();
    }

    protected override void CastActiveR() // 비장의 한 발
    {
        base.CastActiveR();
    }
}
