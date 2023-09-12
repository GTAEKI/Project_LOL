using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ���� Ŭ����
/// ��μ�_230911
/// </summary>
public class CurrentUnitStat
{
    public UnitStat UnitStat { private set; get; }       // ���� ���� ������

    public float Hp { private set; get; } = 0f;

    /// <summary>
    /// ���� ���� �����͸� �����ϴ� ������
    /// ��μ�_230911
    /// </summary>
    /// <param name="unitStat"></param>
    public CurrentUnitStat(UnitStat unitStat)
    {
        UnitStat = unitStat;
    }

    /// <summary>
    /// ü�� ���� �Լ�
    /// ��μ�_230911
    /// </summary>
    /// <param name="value">��ġ��</param>
    public void SettingHp(float value) => Hp = value;

    /// <summary>
    /// ü�� ȸ�� �Լ�
    /// ��μ�_230911
    /// </summary>
    /// <param name="value">ȸ����</param>
    public void OnHeal(float value)
    {
        Hp += value;
        
        if(Hp >= UnitStat.Hp)
        {
            Hp = UnitStat.Hp;
        }
    }

    /// <summary>
    /// ������ �ο� �Լ�
    /// ��μ�_230911
    /// </summary>
    /// <param name="value">������</param>
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
    /// �ӽ� ������
    /// ��μ�_230908
    //인덱스 번호
    public int indexnumber { get; private set; }
    //챔피언 한글이름
    public string name { get; private set; }
    //챔피언 영문이름
    public string EnglishName { get; private set; }
    //챔피언 레벨
    public int Level { get; private set; }
    //1레벨 체력
    public float Hp { get; private set; }
    //1레벨 마나
    public float Mp { get; private set; }
    //1레벨 공격력
    public float Atk { get; private set; }
    //1레벨 주문력
    public float Apk { get; private set; }
    //1레벨 방어력
    public float Defence { get; private set; }
    //1레벨 마법저항력
    public float MDefence { get; private set; }
    //1레벨 공격속도
    public float AtkSpeed { get; private set; }
    //1레벨 주문가속
    public float SkillBoost { get; private set; }
    //1레벨 크리티컬확률
    public float CriticalPer { get; private set; }
    //1레벨 이동속도
    public float MoveMentSpeed { get; private set; }
    //1레벨 체력회복(5초당)
    public float HpRecovery { get; private set; }
    //1레벨 마나회복(5초당)
    public float MpRecovery { get; private set; }
    //1레벨 방어구관통력
    public float ArmorPenetration { get; private set; }
    //1레벨 방어구관통력(퍼센트)
    public float ArmorPenetrationPer { get; private set; }
    //1레벨 마법관통력
    public float MagicPenetration { get; private set; }
    //1레벨 마법관통력(퍼센트)
    public float MagicPenetrationPer { get; private set; }
    //1레벨 평타기반흡혈퍼센트
    public float AttakBloodSucking { get; private set; }
    //1레벨 모든흡혈퍼센트
    public float SkillBloodSucking { get; private set; }
    //1레벨 사정거리
    public float AttackRange { get; private set; }
    //1레벨 강인함
    public float Tenacity { get; private set; }

    //--------레벨에따른 성장 스텟---------
    //성장체력
    public float Growthhp { get; private set; }
    //성장마나
    public float Growthmp { get; private set; }
    //성장공격력
    public float Growthatk { get; private set; }
    //성장방어력
    public float Growthdefence { get; private set; }
    //성장마법저항력
    public float GrowthmDefence { get; private set; }
    //성장공격속도
    public float GrowthatkSpeed { get; private set; }
    //성장이동속도
    public float GrowthmoveMentSpeed { get; private set; }
    //성장체력회복(5초당)
    public float GrowthhpRecovery { get; private set; }
    //성장마나회복(5초당)
    public float GrowthmpRecovery { get; private set; }
    //성장사정거리
    public float GrowthattackRange { get; private set; }

    /// <summary>
    /// 임시 생성자
    /// 김민섭_230908
    /// </summary>
    public UnitStat(int hp, float movementSpeed)
    {
        Hp = hp;
        MovementSpeed = movementSpeed;
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
    /// �ִ� ü�� ���� �Լ�
    /// ��μ�_230911
    /// </summary>
    /// <param name="value">��ġ��</param>
    public void SettingMaxHp(float value) => Hp = value;

    /// <summary>
    /// �ִ� ü�� ���� �Լ�
    /// ��μ�_230911
    /// </summary>
    /// <param name="value">������</param>
    public void OnChangeMaxHp(float value)
    {
        Hp += value;
    }
}
