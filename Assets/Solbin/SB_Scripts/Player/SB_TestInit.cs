using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_TestInit : Unit
{
    public override void Init()
    {
        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Caityln]);
        unitSkill = new UnitSkill(Define.UnitName.Caityln);

        base.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Gragas")
        {
            Debug.Log("그라거스와 충돌");
        }
    }
}