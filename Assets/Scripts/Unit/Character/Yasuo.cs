using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yasuo : Unit
{
    private int spellQ_Count = 0;       // Q 스택 카운팅

    public override Define.UnitState CurrentState 
    { 
        get => base.CurrentState; 
        set
        {
            base.CurrentState = value;

            Animator anim = GetComponent<Animator>();

            switch(base.CurrentState)
            {
                case Define.UnitState.IDLE: anim?.SetFloat("MovementSpeed", 0f); break;
                case Define.UnitState.MOVE: anim?.SetFloat("MovementSpeed", currentUnitStat.MoveMentSpeed); break;
            }
        }
    }

    public override void Init()
    {
        Debug.Log("야스오가 생성되었습니다.");

        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Yasuo]);
        unitSkill = new UnitSkill(Define.UnitName.Yasuo);

        base.Init();
    }

    protected override void CastActiveQ()
    {
        spellQ_Count++;

        Animator anim = GetComponent<Animator>();
        anim.SetBool("SpellQ", true);
        anim.SetInteger("SpellQ_Count", spellQ_Count);

        Invoke("CancleQ", anim.GetCurrentAnimatorClipInfo(0).Length - 0.1f);
        Debug.Log(anim.GetCurrentAnimatorClipInfo(0).Length - 0.1f);

        base.CastActiveQ();     // 쿨타임
    }

    private void CancleQ()
    {
        Debug.Log("확인");

        Animator anim = GetComponent<Animator>();
        anim.SetBool("SpellQ", false);

        if (spellQ_Count >= 3) spellQ_Count = 0;
    }
}
