using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit
{
    public override void Init()
    {
        Debug.Log("�̰� ����ƺ��.");

        unitStat = new CharacterStat();
        unitStat.moveMentSpeed = 5f;
    }
}
