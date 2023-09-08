using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;

public class SB_ItemDataReader : MonoBehaviour
{
    Transform m_legendContainer;
    public GameObject m_legendItemPrefab;

    private void Awake()
    {
        m_legendContainer = GameObject.Find("Container_Legend").transform; // 전설 아이템 목록
        Debug.Assert(m_legendItemPrefab != null);
    }

    void Start()
    {
        /// <summary>
        /// itemInfo csv를 읽고 정보를 분리, 스크립터블 오브젝트에 속성을 할당합니다.
        /// </summary>

        string filePath = "Assets/Solbin/SB_csv/itemInfo.csv";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            int itemAccount = lines.Length - 1; // 헤더 제외 품목 개수
            string[] value = new string[itemAccount];
            GameObject[] newLegendItem = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newLegendItem[i] = Instantiate(m_legendItemPrefab);
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

                // 이미지 삽입을 위함
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().ChangeImg(value[3]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + filePath);
        }
    }
}
