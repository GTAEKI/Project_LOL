using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit
{
    public override void Init()
    {
        Debug.Log("이건 허수아비다.");

        unitStat = new CharacterStat();
        unitStat.moveMentSpeed = 5f;
    }
}
