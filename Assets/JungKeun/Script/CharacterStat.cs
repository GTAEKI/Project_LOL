using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStat
{
    //1레벨 체력
    public float hp;
    //1레벨 마나
    public float mp;
    //1레벨 공격력
    public float atk;
    //1레벨 주문력
    public float apk;
    //1레벨 방어력
    public float defence;
    //1레벨 마법저항력
    public float mDefence;
    //1레벨 공격속도
    public float atkSpeed;
    //1레벨 주문가속
    public float skillBoost;
    //1레벨 크리티컬확률
    public float criticalPer;
    //1레벨 이동속도
    public float moveMentSpeed;
    //1레벨 체력회복(5초당)
    public float hpRecovery;
    //1레벨 마나회복(5초당)
    public float mpRecovery;
    //1레벨 방어구관통력
    public float armorPenetration;
    //1레벨 방어구관통력(퍼센트)
    public float armorPenetrationPer;
    //1레벨 마법관통력
    public float magicPenetration;
    //1레벨 마법관통력(퍼센트)
    public float magicPenetrationPer;
    //1레벨 평타기반흡혈퍼센트
    public float attakBloodSucking;
    //1레벨 모든흡혈퍼센트
    public float skillBloodSucking;
    //1레벨 사정거리
    public float attackRange;
    //1레벨 강인함
    public float tenacity;

    //--------레벨에따른 성장 스텟---------
    //성장체력
    public float growthhp;
    //성장마나
    public float growthmp;
    //성장공격력
    public float growthatk;
    //성장방어력
    public float growthdefence;
    //성장마법저항력
    public float growthmDefence;
    //성장공격속도
    public float growthatkSpeed;
    //성장이동속도
    public float growthmoveMentSpeed;
    //성장체력회복(5초당)
    public float growthhpRecovery;
    //성장마나회복(5초당)
    public float growthmpRecovery;
    //성장사정거리
    public float growthattackRange;

}
