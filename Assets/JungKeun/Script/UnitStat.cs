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
    /// </summary>
    /// <param name="movementSpeed"></param>
    public UnitStat(float movementSpeed)
    {
        MoveMentSpeed = movementSpeed;
    }
}
