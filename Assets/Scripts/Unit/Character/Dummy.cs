using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit
{
    public override void Init()
    {
        Debug.Log("이건 허수아비다.");

        unitStat = new UnitStat(10000, 0f);

        base.Init();
    }
}
