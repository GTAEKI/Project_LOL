using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitStat
{
    //1���� ü��
    public float Hp { get; private set; }
    //1���� ����
    public float Mp { get; private set; }
    //1���� ���ݷ�
    public float Atk { get; private set; }
    //1���� �ֹ���
    public float Apk { get; private set; }
    //1���� ����
    public float Defence { get; private set; }
    //1���� �������׷�
    public float MDefence { get; private set; }
    //1���� ���ݼӵ�
    public float AtkSpeed { get; private set; }
    //1���� �ֹ�����
    public float SkillBoost { get; private set; }
    //1���� ũ��Ƽ��Ȯ��
    public float CriticalPer { get; private set; }
    //1���� �̵��ӵ�
    public float MoveMentSpeed { get; private set; }
    //1���� ü��ȸ��(5�ʴ�)
    public float HpRecovery { get; private set; }
    //1���� ����ȸ��(5�ʴ�)
    public float MpRecovery { get; private set; }
    //1���� �������
    public float ArmorPenetration { get; private set; }
    //1���� �������(�ۼ�Ʈ)
    public float ArmorPenetrationPer { get; private set; }
    //1���� ���������
    public float MagicPenetration { get; private set; }
    //1���� ���������(�ۼ�Ʈ)
    public float MagicPenetrationPer { get; private set; }
    //1���� ��Ÿ��������ۼ�Ʈ
    public float AttakBloodSucking { get; private set; }
    //1���� ��������ۼ�Ʈ
    public float SkillBloodSucking { get; private set; }
    //1���� �����Ÿ�
    public float AttackRange { get; private set; }
    //1���� ������
    public float Tenacity { get; private set; }

    //--------���������� ���� ����---------
    //����ü��
    public float Growthhp { get; private set; }
    //���帶��
    public float Growthmp { get; private set; }
    //������ݷ�
    public float Growthatk { get; private set; }
    //�������
    public float Growthdefence { get; private set; }
    //���帶�����׷�
    public float GrowthmDefence { get; private set; }
    //������ݼӵ�
    public float GrowthatkSpeed { get; private set; }
    //�����̵��ӵ�
    public float GrowthmoveMentSpeed { get; private set; }
    //����ü��ȸ��(5�ʴ�)
    public float GrowthhpRecovery { get; private set; }
    //���帶��ȸ��(5�ʴ�)
    public float GrowthmpRecovery { get; private set; }
    //��������Ÿ�
    public float GrowthattackRange { get; private set; }

    /// <summary>
    /// �ӽ� ������
    /// ��μ�_230908
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
    public UnitStat(float movementSpeed)
    {
        MoveMentSpeed = movementSpeed;
    }
}
