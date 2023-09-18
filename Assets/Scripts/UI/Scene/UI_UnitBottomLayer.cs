using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitBottomLayer : UI_Scene
{
    private enum Images
    {
        Img_IconActiveQ,Img_IconActiveW,Img_IconActiveE,Img_IconActiveR,
        Img_IconPassive,
        Img_IconSpellD,Img_IconSpellF,
        Img_Hpbar, Img_Mpbar,
        Img_Portrait
    }

    private enum Texts
    {
        Text_HpBarexplanation, Text_MpBarexplanation,
        Text_Atk, Text_Ap,
        Text_Def, Text_MDef,
        Text_AtkSpeed, Text_CriticalChance,
        Text_SkillBoost, Text_Movement,
        Text_HpRegen, Text_MpRegen,
        Text_Vamp, Text_AttackRange,
        Text_ArmorPenetration, Text_MagicPenetration,
        Text_AllVamp, Text_Tenacity,

        Text_ActiveQCooltime,
        Text_ActiveWCooltime,
        Text_ActiveECooltime,
        Text_ActiveRCooltime,
        Text_PassiveCooltime,
    }

    private enum GameObjects
    {
        MainStatSystem, OtherStatSystem
    }

    public enum GaugeType
    {
        Hp = 7, Mp
    }

    public enum CooltimeType
    {
        ActiveQ, ActiveW, ActiveE, ActiveR, Passvie
    }

    public override void Init()
    {
        if (isInit) return;
        isInit = true;

        base.Init();

        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        for(int i = (int)Texts.Text_ActiveQCooltime; i <= (int)Texts.Text_PassiveCooltime; i++)
        {
            GetTMP(i).text = "";
        }

        //for(int i = (int)Images.Img_IconPassive; i <= (int)Images.Img_IconActiveR; i++)
        //{
        //    GetImage(i).fillAmount = 0f;
        //}
    }

    /// <summary>
    /// 게이지바 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="curr">현재값</param>
    /// <param name="max">최대값</param>
    /// <param name="recovery">회복량</param>
    public void SetGaugeBar(GaugeType type, float curr, float max, float recovery)
    {
        if (!isInit) Init();

        GetImage((int)type).fillAmount = curr / max;
        GetTMP((int)type - 7).text = $"{curr}/{max} (+{string.Format("{0:N1}", recovery)})";
    }

    /// <summary>
    /// 스탯 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="currStat">현재 스탯</param>
    public void SetMainStatusText(CurrentUnitStat currStat)
    {
        if (!isInit) Init();

        GetTMP((int)Texts.Text_Atk).text = ((int)currStat.Atk).ToString();
        GetTMP((int)Texts.Text_Ap).text = ((int)currStat.Apk).ToString();
        GetTMP((int)Texts.Text_Def).text = ((int)currStat.Defence).ToString();
        GetTMP((int)Texts.Text_MDef).text = ((int)currStat.MDefence).ToString();
        GetTMP((int)Texts.Text_AtkSpeed).text = string.Format("{0:N2}", currStat.AtkSpeed);
        GetTMP((int)Texts.Text_CriticalChance).text = currStat.CriticalPer.ToString();
        GetTMP((int)Texts.Text_SkillBoost).text = ((int)currStat.SkillBoost).ToString();
        GetTMP((int)Texts.Text_Movement).text = ((int)currStat.MoveMentSpeed).ToString();
    }

    public void SetOtherStatusText(CurrentUnitStat currStat)
    {
        if (!isInit) Init();

        GetTMP((int)Texts.Text_HpRegen).text = string.Format("{0:N1}", currStat.HpRecovery);
        GetTMP((int)Texts.Text_MpRegen).text = string.Format("{0:N1}",currStat.MpRecovery);
        GetTMP((int)Texts.Text_Vamp).text = ((int)currStat.AttakBloodSucking).ToString();
        GetTMP((int)Texts.Text_AttackRange).text = ((int)currStat.AttackRange).ToString();
        GetTMP((int)Texts.Text_ArmorPenetration).text = ((int)currStat.ArmorPenetration).ToString();
        GetTMP((int)Texts.Text_MagicPenetration).text = ((int)currStat.MagicPenetration).ToString();
        GetTMP((int)Texts.Text_AllVamp).text = ((int)currStat.SkillBloodSucking).ToString();
        GetTMP((int)Texts.Text_Tenacity).text = ((int)currStat.Tenacity).ToString();
    }

    public void SetCooltime(CooltimeType type, float curr, float max)
    {
        GetImage((int)type).fillAmount = curr / max;
        GetTMP((int)type + 18).text = string.Format("{0:N1}", curr);

        if(GetImage((int)type).fillAmount <= 0f)
        {
            GetTMP((int)type + 18).text = "";
        }
    }

    public float GetCooltime(CooltimeType type) => GetImage((int)type).fillAmount;
}
