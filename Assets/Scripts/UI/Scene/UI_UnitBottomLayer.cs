using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitBottomLayer : UI_Scene
{
    protected enum Images
    {
        Img_IconActiveQ,Img_IconActiveW,Img_IconActiveE,Img_IconActiveR,
        Img_IconPassive,
        Img_IconSpellD,Img_IconSpellF,
        Img_Hpbar, Img_Mpbar,
        Img_Portrait,

        Img_IconActiveQBG, Img_IconActiveWBG, Img_IconActiveEBG, Img_IconActiveRBG,
        Img_IconPassiveBG
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

        Text_Gold
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

    protected Define.UnitName targetUnit;     // 타겟 유닛

    public void SetTarget(Define.UnitName targetUnit) => this.targetUnit = targetUnit;
    public Define.UnitName GetTarget => targetUnit;

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

        SetSkillIcon();
        SetFrame();

        // 골드 이벤트 처리
        SB_ButtonSystem.returnItem += new EventHandler(ReturnMoney);
        SB_ButtonSystem.buyItem += new EventHandler(SpendMoney);
        SB_ButtonSystem.sellItem += new EventHandler(SellMoney);
    }

    /// <summary>
    /// 스킬 아이콘 넣어주는 함수
    /// 배경택_230918
    /// </summary>
    /// <param name="name"></param>
    public void SetSkillIcon()
    {
        string unitName = targetUnit.ToString().ToLower();

        // 패시브
        GetImage((int)Images.Img_IconPassive).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_passive");
        GetImage((int)Images.Img_IconPassiveBG).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_passive");

        // 액티브 Q
        GetImage((int)Images.Img_IconActiveQ).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_q");
        GetImage((int)Images.Img_IconActiveQBG).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_q");

        // 액티브 W
        GetImage((int)Images.Img_IconActiveW).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_w");
        GetImage((int)Images.Img_IconActiveWBG).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_w");

        // 액티브 E
        GetImage((int)Images.Img_IconActiveE).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_e");
        GetImage((int)Images.Img_IconActiveEBG).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_e");

        // 액티브 R
        GetImage((int)Images.Img_IconActiveR).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_r");
        GetImage((int)Images.Img_IconActiveRBG).sprite = Managers.Sprite.GetSkillIcon(targetUnit, $"{unitName}_r");
    }

    public void SetFrame()
    {
        string unitName = targetUnit.ToString().ToLower();

        // 초상화
        GetImage((int)Images.Img_Portrait).sprite = Managers.Sprite.GetFrame(targetUnit, $"{unitName}_circle");
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

    #region 골드 처리 함수

    /// <summary>
    /// 보유 골드 세팅 함수
    /// 김민섭_230926
    /// </summary>
    /// <param name="value">세팅값</param>
    public void SetGold(int value)
    {
        GetTMP((int)Texts.Text_Gold).text = value.ToString();
    }

    /// <summary>
    /// 아이템 구매시 골드 소모
    /// 노솔빈
    /// </summary>
    private void SpendMoney(object sender, EventArgs e)
    {
        //m_gold -= 3000;
        //m_goldText.text = m_gold.ToString();
    }

    /// <summary>
    /// 아이템 되돌리기 & 판매 시 골드 회복
    /// 노솔빈
    /// </summary>
    private void ReturnMoney(object sender, EventArgs e)
    {
        //m_gold += 3000;
        //m_goldText.text = m_gold.ToString();
    }

    /// <summary>
    /// 아이템 되팔기
    /// 노솔빈
    /// </summary>
    private void SellMoney(object sender, EventArgs e)
    {
        //m_gold += Mathf.RoundToInt(3000 * 0.7f);
        //m_goldText.text = m_gold.ToString();
    }

    public void ReturnMoney()
    {
        
    }

    #endregion
}
