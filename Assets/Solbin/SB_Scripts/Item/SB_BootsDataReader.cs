using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SB_BootsDataReader : MonoBehaviour
{
    Transform m_sideDownContainer;
    public GameObject m_itemPrefab;

    private void Awake()
    {
        m_sideDownContainer = GameObject.Find("Side Shop_Down").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        string bootsFilePath = "Assets/Solbin/SB_csv/bootsInfo.csv";

        if (File.Exists(bootsFilePath))
        {
            string[] lines = File.ReadAllLines(bootsFilePath);

            int itemAccount = lines.Length - 1; // 헤더 제외 품목 개수

            string[] value = new string[itemAccount];
            GameObject[] newLegendItem = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
            {
                newLegendItem[i] = Instantiate(m_itemPrefab);
                newLegendItem[i].transform.SetParent(m_sideDownContainer.transform, false);
            }

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
            {
                value = lines[i].Split(",");

                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().name = value[1];
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().englishName = value[2];
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[3]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[4]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[5]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[6]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().magicResistance = int.Parse(value[8]);
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().tenacity = int.Parse(value[9]);

                // 이미지 삽입을 위함
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().ChangeBootsImg(value[2]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + bootsFilePath);
        }
    }
}
