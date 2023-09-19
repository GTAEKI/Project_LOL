using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SB_PlayerMoving : Unit
{
    public override void Init()
    {
        unitStat = new UnitStat(1000, 5f);

        base.Init();
    }
}
