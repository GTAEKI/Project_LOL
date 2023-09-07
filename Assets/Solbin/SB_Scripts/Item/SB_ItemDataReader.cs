using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 아이템 속성을 담기 위한 열거형
/// </summary>
public enum ItemContainer
{
    typeNumber, // 0: 전설, 1: 신화
    job, // 클래스
    name, // 아이템 이름
    attackDamage, // 공격력
    attackSpeed, // 공격 속도
    armor, // 방어력
    magicResistance, // 마법방어력
    health, // 체력
    abilityHaste, // 스킬 가속
    lifeSteal // 생명력 흡수
}

public class SB_ItemDataReader : MonoBehaviour
{
    List<string[]> m_value = new List<string[]>();

    /// <summary>
    /// itemInfo csv를 읽고 정보를 분리합니다.
    /// </summary>
    void Start()
    {
        string filePath = "Assets/Solbin/SB_csv/itemInfo.csv";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath); // 줄 읽기

            for (int i =1; i < lines.Length; i++)
            {
                m_value.Add(lines[i].Split(",")); // 쉼표 분리: 첫번째 품목 헤더 무시
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + filePath);
        }
    }

    /// <summary>
    /// Start()에서 분리한 csv 정보를 아이템으로 변환합니다.
    /// </summary>
    void AddItem()
    {
        for (int i = 0; i < m_value.Count; i++) // Index 0부터 아이템 항목 시작
        {

        }
    }
}
