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
        indexnumber = baseStat.index;
        name = baseStat.name_ko;
        EnglishName = baseStat.name_en;
        Hp = baseStat.maxHp;
        Mp = baseStat.maxMp;
        Atk = baseStat.strength;
        Defence = baseStat.physicalDefense;
        MDefence = baseStat.magicDefense;
        AtkSpeed = baseStat.attackSpeed;
        MoveMentSpeed = baseStat.movementSpeed;
        HpRecovery = baseStat.hpRecovery;
        MpRecovery = baseStat.mpRecovery;
        AttackRange = baseStat.attackRange;
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

/// <summary>
/// 현재 유닛 스탯 클래스
/// 김민섭_230911
/// </summary>
public class CurrentUnitStat
{
    private float atk;
    private float apk;
    private float defence;
    private float mdefence;
    private float atkSpeed;
    private float skillBoost;
    private float criticalPer;
    private float movementSpeed;
    private float hpRecovery;
    private float mpRecovery;
    private float armorPenetration;
    private float magicPenetration;
    private float attackbloodSucking;
    private float skillbloodSucking;
    private float attackRange;
    private float tenacity;

    public UnitStat UnitStat { private set; get; }       // 기본 베이스 유닛 스탯

    public int Level { get; private set; }
    public float Hp { get; private set; }
    public float Mp { get; private set; }
    public float Atk { get => atk; private set { atk = value; RefreshStatusUI(); } }
    public float Apk { get => apk; private set { apk = value; RefreshStatusUI(); } }
    public float Defence { get => defence; private set { defence = value; RefreshStatusUI(); } }
    public float MDefence { get => mdefence; private set { mdefence = value; RefreshStatusUI(); } }
    public float AtkSpeed { get => atkSpeed; private set { atkSpeed = value; RefreshStatusUI(); } }
    public float SkillBoost { get => skillBoost; private set { skillBoost = value; RefreshStatusUI(); } }
    public float CriticalPer { get => criticalPer; private set { criticalPer = value; RefreshStatusUI(); } }
    public float MoveMentSpeed { get => movementSpeed; private set { movementSpeed = value; RefreshStatusUI(); } }
    public float HpRecovery { get => hpRecovery; private set { hpRecovery = value; RefreshStatusUI(); } }
    public float MpRecovery { get => mpRecovery; private set { mpRecovery = value; RefreshStatusUI(); } }
    public float ArmorPenetration { get => armorPenetration; private set { armorPenetration = value; RefreshStatusUI(); } }
    public float ArmorPenetrationPer { get; private set; }
    public float MagicPenetration { get => magicPenetration; private set { magicPenetration = value; RefreshStatusUI(); } }
    public float MagicPenetrationPer { get; private set; }
    public float AttakBloodSucking { get => attackbloodSucking; private set { attackbloodSucking = value; RefreshStatusUI(); } }
    public float SkillBloodSucking { get => skillbloodSucking; private set { skillbloodSucking = value; RefreshStatusUI(); } }
    public float AttackRange { get => attackRange; private set { attackRange = value; RefreshStatusUI(); } }
    public float Tenacity { get => tenacity; private set { tenacity = value; RefreshStatusUI(); } }

    /// <summary>
    /// 유닛 기본 스탯을 가져와서 세팅하는 생성자
    /// 김민섭_230911
    /// </summary>
    /// <param name="unitStat">유닛 기본 스탯</param>
    public CurrentUnitStat(UnitStat unitStat)
    {
        UnitStat = unitStat;

        atk = UnitStat.Atk;
        apk = UnitStat.Apk;
        defence = UnitStat.Defence;
        mdefence = UnitStat.MDefence;
        atkSpeed = UnitStat.AtkSpeed;
        skillBoost = UnitStat.SkillBoost;
        criticalPer = UnitStat.CriticalPer;
        movementSpeed = UnitStat.MoveMentSpeed;

        armorPenetration = UnitStat.ArmorPenetration;
        magicPenetration = UnitStat.MagicPenetration;
        attackbloodSucking = UnitStat.AttakBloodSucking;
        skillbloodSucking = UnitStat.SkillBloodSucking;
        attackRange = UnitStat.AttackRange;
        tenacity = UnitStat.Tenacity;

        SettingHpGroup(unitStat.Hp, unitStat.HpRecovery);
        SettingMpGroup(unitStat.Mp, unitStat.MpRecovery);

        RefreshStatusUI();
    }

    /// <summary>
    /// 스탯 관련 UI 초기화 함수
    /// 김민섭_230917
    /// </summary>
    private void RefreshStatusUI()
    {
        UI_UnitBottomLayer bottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
        bottomLayer?.SetMainStatusText(this);
        bottomLayer?.SetOtherStatusText(this);
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
        if (bottomLayer != null)
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

    /// <summary>
    /// 공격력 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="atk">세팅값</param>
    public void SettingAtk(float atk) => Atk = atk;

    /// <summary>
    /// 주문력 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="apk">세팅값</param>
    public void SettingApk(float apk) => Apk = apk;

    /// <summary>
    /// 물리방어력 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="def">세팅값</param>
    public void SettingDef(float def) => Defence = def;

    /// <summary>
    /// 마법방어력 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="mdef">세팅값</param>
    public void SettingMDef(float mdef) => MDefence = mdef;

    /// <summary>
    /// 공격속도 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="atkSpeed">세팅값</param>
    public void SettingAtkSpeed(float atkSpeed) => AtkSpeed = atkSpeed;

    /// <summary>
    /// 치명타율 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="criPer">세팅값</param>
    public void SettingCriPer(float criPer) => CriticalPer = criPer;

    /// <summary>
    /// 스킬 가속 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="skillBoost">세팅값</param>
    public void SettingSkillBoost(float skillBoost) => SkillBoost = skillBoost;

    /// <summary>
    /// 이동속도 세팅 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="movement">세팅값</param>
    public void SettingMovement(float movement) => MoveMentSpeed = movement;

    #endregion

    /// <summary>
    /// 체력 회복 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">회복량</param>
    public void OnHeal(float value)
    {
        Hp += value;

        if (Hp >= UnitStat.Hp)
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

        if (Hp <= 0)
        {
            Hp = 0;
            
        }
        
    }
}