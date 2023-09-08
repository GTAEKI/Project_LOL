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
        m_legendContainer = GameObject.Find("Container_Legend").transform; // ���� ������ ���
        Debug.Assert(m_legendItemPrefab != null);
    }

    void Start()
    {
        /// <summary>
        /// itemInfo csv�� �а� ������ �и�, ��ũ���ͺ� ������Ʈ�� �Ӽ��� �Ҵ��մϴ�.
        /// </summary>

        string filePath = "Assets/Solbin/SB_csv/itemInfo.csv";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            int itemAccount = lines.Length - 1; // ��� ���� ǰ�� ����
            string[] value = new string[itemAccount];
            GameObject[] newLegendItem = new GameObject[itemAccount];

            for (int i = 0; i < itemAccount; i++) // ������ ���� ����
            {
                newLegendItem[i] = Instantiate(m_legendItemPrefab);
                newLegendItem[i].transform.SetParent(m_legendContainer.transform, false);
            }

            for (int i = 1; i < lines.Length; i++) // ��ǥ �и�(��� ����) & ������ �Ӽ� ����
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

                // �̹��� ������ ����
                newLegendItem[i - 1].GetComponent<SB_ItemProperty>().ChangeImg(value[3]);
            }
        }
        else
        {
            Debug.LogError("csv Missing: " + filePath);
        }
    }
}
