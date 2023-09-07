using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// ������ �Ӽ��� ��� ���� ������
/// </summary>
public enum ItemContainer
{
    typeNumber, // 0: ����, 1: ��ȭ
    job, // Ŭ����
    name, // ������ �̸�
    attackDamage, // ���ݷ�
    attackSpeed, // ���� �ӵ�
    armor, // ����
    magicResistance, // ��������
    health, // ü��
    abilityHaste, // ��ų ����
    lifeSteal // ����� ���
}

public class SB_ItemDataReader : MonoBehaviour
{
    List<string[]> m_value = new List<string[]>();

    /// <summary>
    /// itemInfo csv�� �а� ������ �и��մϴ�.
    /// </summary>
    void Start()
    {
        string filePath = "Assets/Solbin/SB_csv/itemInfo.csv";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath); // �� �б�

            for (int i =1; i < lines.Length; i++)
            {
                m_value.Add(lines[i].Split(",")); // ��ǥ �и�: ù��° ǰ�� ��� ����
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + filePath);
        }
    }

    /// <summary>
    /// Start()���� �и��� csv ������ ���������� ��ȯ�մϴ�.
    /// </summary>
    void AddItem()
    {
        for (int i = 0; i < m_value.Count; i++) // Index 0���� ������ �׸� ����
        {

        }
    }
}
