using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SB_PlayerMoving : Unit
{
    Animator animator;

    private void MyStart()
    {
        animator = GetComponent<Animator>();
    }

    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

        base.Init();
    }

    protected override void UpdateMove()
    {
        MyStart();
        /*animator.SetBool("Run", true);*/ // 거리 체크 불가능

        base.UpdateMove();
    }

    protected override void CastActiveQ()
    {
        // 스킬 구현 
        base.CastActiveQ();
    }
}
