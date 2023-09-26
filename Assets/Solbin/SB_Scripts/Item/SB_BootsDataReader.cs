//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;

//public class SB_BootsDataReader : MonoBehaviour
//{
//    Transform m_sideDownContainer;
//    private GameObject m_itemPrefab; // 아이템 선택 효과 등을 담은 프리팹

//    private void Awake()
//    {
//        m_sideDownContainer = GameObject.Find("Side Shop_Down").transform;

//        m_itemPrefab = (GameObject)AssetDatabase.LoadAssetAtPath
//            ("Assets/Solbin/SB_Prefabs/Item Select.prefab", typeof(GameObject));
//        Debug.Assert(m_itemPrefab != null);
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        string bootsFilePath = "Assets/Solbin/SB_csv/bootsInfo.csv";

//        if (File.Exists(bootsFilePath))
//        {
//            string[] lines = File.ReadAllLines(bootsFilePath);

//            int itemAccount = lines.Length - 1; // 헤더 제외 품목 개수

//            string[] value = new string[itemAccount];
//            GameObject[] newLegendItem = new GameObject[itemAccount];
//            GameObject[] legendItemImg = new GameObject[itemAccount];

//            for (int i = 0; i < itemAccount; i++) // 아이템 슬롯 생성
//            {
//                newLegendItem[i] = Instantiate(m_itemPrefab);
//                newLegendItem[i].transform.SetParent(m_sideDownContainer.transform, false);

//                legendItemImg[i] = newLegendItem[i].transform.GetChild(1).gameObject;
//            }

//            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 아이템 속성 삽입
//            {
//                value = lines[i].Split(",");

//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().typeNumber = int.Parse(value[0]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().name = value[1];
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().englishName = value[2];
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().movementSpeed = int.Parse(value[3]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().abilityHaste = int.Parse(value[4]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().attackSpeed = int.Parse(value[5]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().magicPenetration = int.Parse(value[6]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().armor = int.Parse(value[7]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().magicResistance = int.Parse(value[8]);
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().tenacity = int.Parse(value[9]);

//                // 이미지 삽입을 위함
//                legendItemImg[i - 1].GetComponent<SB_ItemProperty>().ChangeBootsImg(value[2]);
//            }
//        }
//        else
//        {
//            Debug.LogError("csv Missing: " + bootsFilePath);
//        }
//    }
//}
