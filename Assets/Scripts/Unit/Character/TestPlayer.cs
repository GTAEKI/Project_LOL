using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Unit
{
    public override void Init()
    {
        Debug.Log("테스트 플레이어가 생성되었습니다.");

        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);

        base.Init();
    }
}
