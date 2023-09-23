using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit
{
    public override void Init()
    {
        Debug.Log("허수아비 생성");

        unitStat = new UnitStat(
            Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);


        base.Init();
    }
}
