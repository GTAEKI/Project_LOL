using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamBoxHp : UI_Base
{
    private enum Texts
    {
        Text_Nickname,
        Text_Hp
    }

    private bool isInit = false;

    public override void Init()
    {
        if (isInit) return;
        isInit = true;

        Debug.Log("초기화함");

        Bind<TextMeshProUGUI>(typeof(Texts));

        transform.localScale = Vector3.one;
    }

    public void SettingNickname(string nickname)
    {
        Init();

        GetTMP((int)Texts.Text_Nickname).text = nickname;
    }

    public void SettingHp(float hp, float hpmax)
    {
        Init();
        GetTMP((int)Texts.Text_Hp).text = $"체력: {hp}/{hpmax}";
    }
}
