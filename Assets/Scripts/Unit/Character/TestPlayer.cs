using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Unit
{
    public override void Init()
    {
        base.Init();

        Debug.Log("�׽�Ʈ �÷��̾ �����Ǿ����ϴ�.");

        unitStat = new UnitStat(5f);
    }
}
