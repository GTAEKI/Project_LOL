using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;

/// <summary>
/// 아이템의 정보 csv로 읽어와 property에 할당 
/// </summary>
public class SB_ItemDataReader : MonoBehaviour
{
    Transform m_legendContainer;
    Transform m_mythContainer;
    public GameObject m_itemPrefab;

    private void Awake()
    {
        m_legendContainer = GameObject.Find("Container_Legend").transform; // 전설 아이템 목록
        m_mythContainer = GameObject.Find("Container_Myth").transform; // 신화 아이템 목록
        Debug.Assert(m_itemPrefab != null);
    }

    /// <summary>
    /// itemInfo csv를 읽고 정보를 분리, 스크립터블 오브젝트에 속성을 할당합니다.
    /// </summary>
    void Start()
    {
        string itemFilePath = "Assets/Solbin/SB_csv/LegendItemInfo.csv"; // 전설 아이템
        if (File.Exists(itemFilePath))
        {
            string[] lines = File.ReadAllLines(itemFilePath);
            int itemAccount = lines.Length - 1; // 헤더 제외 품목 개수
            string[] value = new string[itemAccount];
            GameObject[] newLegendItem = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newLegendItem[i] = Instantiate(m_itemPrefab);
                newLegendItem[i].transform.SetParent(m_legendContainer.transform, false);
            }

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
            {
                value = lines[i].Split(",");

                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().name = value[2];
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().englishName = value[3];
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().attackDamage = int.Parse(value[5]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[6]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().magicResistance =
                    int.Parse(value[8]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().health = int.Parse(value[9]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[10]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().lifeSteal = int.Parse(value[11]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().criticalStrikeChance = int.Parse(value[12]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[13]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().lethality = int.Parse(value[14]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().armorPenetration = int.Parse(value[15]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[16]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().abilityPower = int.Parse(value[17]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().mana = int.Parse(value[18]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().basicHealthRegenaration = int.Parse(value[20]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().basicManaRegenaration = int.Parse(value[21]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().healthAndShieldPower = int.Parse(value[22]);

                // 이미지 삽입을 위함
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().ChangeItemImg(value[3]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + itemFilePath);
        }

        string mythItemPath = "Assets/Solbin/SB_csv/MythItemInfo.csv"; // 신화 아이템
        if (File.Exists(mythItemPath))
        {
            string[] lines = File.ReadAllLines(mythItemPath);

            int itemAccount = lines.Length - 1; // 헤더 제외 품목 개수
            string[] value = new string[itemAccount];
            GameObject[] newMythItem = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newMythItem[i] = Instantiate(m_itemPrefab);
                newMythItem[i].transform.SetParent(m_mythContainer.transform, false);
            }

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
            {
                value = lines[i].Split(",");

                newMythItem[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().name = value[2];
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().englishName = value[3];
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().attackDamage = int.Parse(value[5]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[6]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().magicResistance =
                    int.Parse(value[8]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().health = int.Parse(value[9]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[10]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().lifeSteal = int.Parse(value[11]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().criticalStrikeChance = int.Parse(value[12]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[13]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().lethality = int.Parse(value[14]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().armorPenetration = int.Parse(value[15]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[16]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().abilityPower = int.Parse(value[17]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().mana = int.Parse(value[18]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().basicHealthRegenaration = int.Parse(value[20]);
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().basicManaRegenaration = int.Parse(value[21]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().healthAndShieldPower = int.Parse(value[22]);

                // 이미지 삽입을 위함
                newMythItem[i - 1].GetComponent<SB_ItemProperty>().ChangeMythImg(value[3]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + mythItemPath);
        }
    }
}