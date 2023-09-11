using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit
{
    public override void Init()
    {
        base.Init();

        Debug.Log("이건 허수아비다.");
    }
}
