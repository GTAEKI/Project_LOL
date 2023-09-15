using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 현재 유닛 스탯 클래스
/// 김민섭_230911
/// </summary>
public class CurrentUnitStat
{
    public UnitStat UnitStat { private set; get; }       // 기본 베이스 유닛 스탯

    public float Hp { private set; get; } = 0f;
    public float Mp { private set; get; } = 0f;
    public float HpRecovery { private set; get; } = 0f;
    public float MpRecovery { private set; get; } = 0f;

    /// <summary>
    /// 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙占싶몌옙 占쏙옙占쏙옙占싹댐옙 占쏙옙占쏙옙占쏙옙
    /// 占쏙옙關占?230911
    /// </summary>
    /// <param name="unitStat"></param>
    public CurrentUnitStat(UnitStat unitStat)
    {
        UnitStat = unitStat;
    }

    #region 세팅 함수

    /// <summary>
    /// 체력 세팅 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingHp(float value) => Hp = value;

    /// <summary>
    /// 마나 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingMp(float value) => Mp = value;

    /// <summary>
    /// 체력자연회복량 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingHpRecovery(float value) => HpRecovery = value;

    /// <summary>
    /// 마나자연회복량 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingMpRecovery(float value) => MpRecovery = value;

    /// <summary>
    /// 체력 관련 스탯 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="hp">최대 체력</param>
    /// <param name="recovery">자연 회복량</param>
    public void SettingHpGroup(float hp, float recovery)
    {
        SettingHp(hp);
        SettingHpRecovery(recovery);

        UI_UnitBottomLayer bottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
        if(bottomLayer != null)
        {
            bottomLayer.SetGaugeBar(UI_UnitBottomLayer.GaugeType.Hp, hp, UnitStat.Hp, recovery);
        }
    }

    /// <summary>
    /// 마나 관련 스탯 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="mp">최대 마나</param>
    /// <param name="recovery">자연 회복량</param>
    public void SettingMpGroup(float mp, float recovery)
    {
        SettingMp(mp);
        SettingMpRecovery(recovery);

        UI_UnitBottomLayer bottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
        if (bottomLayer != null)
        {
            bottomLayer.SetGaugeBar(UI_UnitBottomLayer.GaugeType.Mp, mp, UnitStat.Mp, recovery);
        }
    }

    #endregion

    /// <summary>
    /// 체력 회복 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">회복량</param>
    public void OnHeal(float value)
    {
        Hp += value;
        
        if(Hp >= UnitStat.Hp)
        {
            Hp = UnitStat.Hp;
        }
    }

    /// <summary>
    /// 데미지 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">占쏙옙占쏙옙占쏙옙</param>
    public void OnDamaged(float value)
    {
        Hp -= value;

        if(Hp <= 0)
        {
            Hp = 0;
        }
    }
}


public class UnitStat
{
    /// <summary>
    /// 占쌈쏙옙 占쏙옙占쏙옙占쏙옙
    /// 占쏙옙關占?230908
    //?몃뜳??踰덊샇
    public static int PlayerCharacterId = -1;

    public int indexnumber { get; private set; }
    //梨뷀뵾???쒓??대쫫
    public string name { get; private set; }
    //梨뷀뵾???곷Ц?대쫫
    public string EnglishName { get; private set; }
    //梨뷀뵾???덈꺼
    public int Level { get; private set; }
    //1?덈꺼 泥대젰
    public float Hp { get; private set; }
    //1?덈꺼 留덈굹
    public float Mp { get; private set; }
    //1?덈꺼 怨듦꺽??
    public float Atk { get; private set; }
    //1?덈꺼 二쇰Ц??
    public float Apk { get; private set; }
    //1?덈꺼 諛⑹뼱??
    public float Defence { get; private set; }
    //1?덈꺼 留덈쾿???젰
    public float MDefence { get; private set; }
    //1?덈꺼 怨듦꺽?띾룄
    public float AtkSpeed { get; private set; }
    //1?덈꺼 二쇰Ц媛??
    public float SkillBoost { get; private set; }
    //1?덈꺼 ?щ━?곗뺄?뺣쪧
    public float CriticalPer { get; private set; }
    //1?덈꺼 ?대룞?띾룄
    public float MoveMentSpeed { get; private set; }
    //1?덈꺼 泥대젰?뚮났(5珥덈떦)
    public float HpRecovery { get; private set; }
    //1?덈꺼 留덈굹?뚮났(5珥덈떦)
    public float MpRecovery { get; private set; }
    //1?덈꺼 諛⑹뼱援ш??듬젰
    public float ArmorPenetration { get; private set; }
    //1?덈꺼 諛⑹뼱援ш??듬젰(?쇱꽱??
    public float ArmorPenetrationPer { get; private set; }
    //1?덈꺼 留덈쾿愿?듬젰
    public float MagicPenetration { get; private set; }
    //1?덈꺼 留덈쾿愿?듬젰(?쇱꽱??
    public float MagicPenetrationPer { get; private set; }
    //1?덈꺼 ?됲?湲곕컲?≫삁?쇱꽱??
    public float AttakBloodSucking { get; private set; }
    //1?덈꺼 紐⑤뱺?≫삁?쇱꽱??
    public float SkillBloodSucking { get; private set; }
    //1?덈꺼 ?ъ젙嫄곕━
    public float AttackRange { get; private set; }
    //1?덈꺼 媛뺤씤??
    public float Tenacity { get; private set; }

    //--------?덈꺼?먮뵲瑜??깆옣 ?ㅽ뀩---------
    //?깆옣泥대젰
    public float Growthhp { get; private set; }
    //?깆옣留덈굹
    public float Growthmp { get; private set; }
    //?깆옣怨듦꺽??
    public float Growthatk { get; private set; }
    //?깆옣諛⑹뼱??
    public float Growthdefence { get; private set; }
    //?깆옣留덈쾿???젰
    public float GrowthmDefence { get; private set; }
    //?깆옣怨듦꺽?띾룄
    public float GrowthatkSpeed { get; private set; }
    //?깆옣?대룞?띾룄
    public float GrowthmoveMentSpeed { get; private set; }
    //?깆옣泥대젰?뚮났(5珥덈떦)
    public float GrowthhpRecovery { get; private set; }
    //?깆옣留덈굹?뚮났(5珥덈떦)
    public float GrowthmpRecovery { get; private set; }
    //?깆옣?ъ젙嫄곕━
    public float GrowthattackRange { get; private set; }

    /// <summary>
    /// 임시 생성자
    /// 김민섭_230908
    /// </summary>
    public UnitStat(MS.Data.UnitBaseStat baseStat)
    {
        Hp = baseStat.maxHp;
        HpRecovery = baseStat.hpRecovery;
        Mp = baseStat.maxMp;
        MpRecovery = baseStat.mpRecovery;
    }

    /// <summary>
    /// 임시 생성자
    /// 김민섭_230908
    /// </summary>
    public UnitStat(int indexnumber, string name, string EnglishName, float Hp, float Mp, float Atk, float Defence, float MDefence, float AtkSpeed, float MoveMentSpeed,
                    float HpRecovery, float MpRecovery,float AttackRange, float Growthhp, float Growthmp, float Growthatk, float Growthdefence, float GrowthatkSpeed,
                    float GrowthmoveMentSpeed, float GrowthhpRecovery, float GrowthmpRecovery, float GrowthattackRange)
    {
        this.indexnumber = indexnumber;
        this.name = name;
        this.EnglishName = EnglishName;
        this.Hp = Hp;
        this.Mp = Mp;
        this.Atk = Atk;
        this.Defence = Defence;
        this.MDefence = MDefence;
        this.AtkSpeed = AtkSpeed;
        this.MoveMentSpeed = MoveMentSpeed;
        this.HpRecovery = HpRecovery;
        this.MpRecovery = MpRecovery;
        this.AttackRange = AttackRange;
        this.Growthhp = Growthhp;
        this.Growthmp = Growthmp;
        this.Growthatk = Growthatk;
        this.Growthdefence = Growthdefence;
        this.GrowthatkSpeed = GrowthatkSpeed;
        this.GrowthmoveMentSpeed = GrowthmoveMentSpeed;
        this.GrowthhpRecovery = GrowthhpRecovery;
        this.GrowthmpRecovery = GrowthmpRecovery;
        this.GrowthattackRange = GrowthattackRange;
    }





    /// <summary>
    /// 占쌍댐옙 체占쏙옙 占쏙옙占쏙옙 占쌉쇽옙
    /// 占쏙옙關占?230911
    /// </summary>
    /// <param name="value">占쏙옙치占쏙옙</param>
    public void SettingMaxHp(float value) => Hp = value;

    /// <summary>
    /// 占쌍댐옙 체占쏙옙 占쏙옙占쏙옙 占쌉쇽옙
    /// 占쏙옙關占?230911
    /// </summary>
    /// <param name="value">占쏙옙占쏙옙占쏙옙</param>
    public void OnChangeMaxHp(float value)
    {
        Hp += value;
    }
}
