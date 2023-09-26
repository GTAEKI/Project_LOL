//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class SB_GragasMoving : Unit
//{
//    Animator animator;

//    GameObject barrelQPrefab;
//    GameObject barrelQ;

//    SB_GragasQ gragasQ;

//    public override void Init()
//    {
//        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
//        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

//        animator = GetComponent<Animator>();

//        base.Init();   
//    }

//    protected override void UpdateMove()
//    {
//        animator.SetBool("Run", true);
//        base.UpdateMove();
//    }

//    protected override void UpdateIdle()
//    {
//        animator.SetBool("Run", false);
//        base.UpdateIdle();
//    }

//    protected override void CastActiveQ() // 술통 굴리기
//    {
//        base.CastActiveQ();
//    }

//    protected override void CastActiveW() // 취중 분노
//    {
//        base.CastActiveW();
//    }

//    protected override void CastActiveE() // 몸통 박치기
//    {
//        base.CastActiveE();
//    }

//    protected override void CastActiveR() // 술통 폭발
//    {
//        base.CastActiveR();
//    }
//}
