using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 현재 유닛의 스탯 클래스
/// 김민섭_230911
/// </summary>
public class CurrentUnitStat
{
    public UnitStat UnitStat { private set; get; }       // 원본 스탯 데이터

    public float Hp { private set; get; } = 0f;

    /// <summary>
    /// 원본 스탯 데이터를 세팅하는 생성자
    /// 김민섭_230911
    /// </summary>
    /// <param name="unitStat"></param>
    public CurrentUnitStat(UnitStat unitStat)
    {
        UnitStat = unitStat;
    }

    /// <summary>
    /// 체력 세팅 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingHp(float value) => Hp = value;

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
    /// 데미지 부여 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">데미지</param>
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
    /// <param name="movementSpeed"></param>
    public UnitStat(int hp, float movementSpeed)
    {
        Hp = hp;
        MoveMentSpeed = movementSpeed;
    }

    /// <summary>
    /// 최대 체력 세팅 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">수치값</param>
    public void SettingMaxHp(float value) => Hp = value;

    /// <summary>
    /// 최대 체력 조정 함수
    /// 김민섭_230911
    /// </summary>
    /// <param name="value">조정값</param>
    public void OnChangeMaxHp(float value)
    {
        Hp += value;
    }
}
