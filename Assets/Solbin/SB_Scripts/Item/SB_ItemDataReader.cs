using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

/// <summary>
/// 아이템의 정보 csv로 읽어와 property에 할당 
/// </summary>
public class SB_ItemDataReader : MonoBehaviour
{
    Transform m_legendContainer;
    Transform m_mythContainer;
    private GameObject m_itemPrefab;

    private void Awake()
    {
        m_legendContainer = GameObject.Find("Container_Legend").transform; // 전설 아이템 목록
        m_mythContainer = GameObject.Find("Container_Myth").transform; // 신화 아이템 목록

        //m_itemPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
            //("Assets/Solbin/SB_Prefabs/Item Select.prefab", typeof(GameObject));
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
            GameObject[] legendItemImg = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newLegendItem[i] = Instantiate(m_itemPrefab);
                newLegendItem[i].transform.SetParent(m_legendContainer.transform, false);

                legendItemImg[i] = newLegendItem[i].transform.GetChild(1).gameObject;
            }

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
            {
                value = lines[i].Split(",");

                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().name = value[2];
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().englishName = value[3];
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().attackDamage = int.Parse(value[5]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[6]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().magicResistance =
                    int.Parse(value[8]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().health = int.Parse(value[9]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[10]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().lifeSteal = int.Parse(value[11]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().criticalStrikeChance = int.Parse(value[12]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[13]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().lethality = int.Parse(value[14]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().armorPenetration = int.Parse(value[15]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[16]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().abilityPower = int.Parse(value[17]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().mana = int.Parse(value[18]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().basicHealthRegenaration = int.Parse(value[20]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().basicManaRegenaration = int.Parse(value[21]);
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().healthAndShieldPower = int.Parse(value[22]);

                // 이미지 삽입을 위함
                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().ChangeItemImg(value[3]);
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
            GameObject[] mythItemImg = new GameObject[itemAccount]; 

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newMythItem[i] = Instantiate(m_itemPrefab);
                newMythItem[i].transform.SetParent(m_mythContainer.transform, false);

                mythItemImg[i] = newMythItem[i].transform.GetChild(1).gameObject;
            }

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
            {
                value = lines[i].Split(",");

                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().name = value[2];
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().englishName = value[3];
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().attackDamage = int.Parse(value[5]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[6]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().magicResistance =
                    int.Parse(value[8]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().health = int.Parse(value[9]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[10]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().lifeSteal = int.Parse(value[11]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().criticalStrikeChance = int.Parse(value[12]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[13]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().lethality = int.Parse(value[14]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().armorPenetration = int.Parse(value[15]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[16]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().abilityPower = int.Parse(value[17]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().mana = int.Parse(value[18]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().basicHealthRegenaration = int.Parse(value[20]);
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().basicManaRegenaration = int.Parse(value[21]);
                //newMythItem[i - 1].GetComponent<SB_ItemProperty>().healthAndShieldPower = int.Parse(value[22]);

                // 이미지 삽입을 위함
                mythItemImg[i - 1].GetComponent<SB_ItemProperty>().ChangeMythImg(value[3]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + mythItemPath);
        }
    }
}