using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Unit
{
    public override void Init()
    {
        Debug.Log("�׽�Ʈ �÷��̾ �����Ǿ����ϴ�.");

        unitStat = new CharacterStat
        {
            moveMentSpeed = 5f
        };
    }
}