using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class statDataReader : MonoBehaviour
{
    Transform m_statData;
    public GameObject m_statPrefab;
    

    private void Awake()
    {
        m_statData = GameObject.Find("Data_stat").transform; // 스텟목록
        Debug.Assert(m_statPrefab != null);
    }

    void Start()
    {
        /// <summary>
        /// itemInfo csv를 읽고 정보를 분리, 스크립터블 오브젝트에 속성을 할당합니다.
        /// </summary>

        string filePath = "Assets/JungKeun/JK_csv/statInfo.csv";
        Debug.Log("파일 찾기시작");
        if (File.Exists(filePath))
        {
            Debug.Log("파일 찾음");
            string[] lines = File.ReadAllLines(filePath);
            int statAcount = lines.Length - 1; // 헤더 제외 품목 개수
            string[] value = new string[statAcount];
            GameObject[] newStat = new GameObject[statAcount];


            Debug.Log(lines.Length);
            DataManger.unitStatDictionary = new Dictionary<int, UnitStat>();

/*
            for (int i = 0; i < statAcount; i++) // 아이템 슬롯 생성
            {
                newStat[i] = Instantiate(m_statPrefab);
                newStat[i].transform.SetParent(m_statData.transform, false);
            }*/

            for (int i = 1; i < lines.Length; i++) // 쉼표 분리(헤더 제외) & 스텟 속성 삽입
            {
                value = lines[i].Split(",");

                Debug.Log(lines[i]);
                Debug.Log(value.Length);
                UnitStat newUnit = new UnitStat(
                int.Parse(value[0]), value[1], value[2], float.Parse(value[3]),
                float.Parse(value[4]), float.Parse(value[5]), float.Parse(value[6]),
                float.Parse(value[7]), float.Parse(value[8]), float.Parse(value[9]),
                float.Parse(value[10]), float.Parse(value[11]), float.Parse(value[12]),
                float.Parse(value[13]), float.Parse(value[14]), float.Parse(value[15]),
                float.Parse(value[16]), float.Parse(value[17]), float.Parse(value[18]),
                float.Parse(value[19]), float.Parse(value[20]), float.Parse(value[21]));

                DataManger.unitStatDictionary.Add(newUnit.indexnumber, newUnit);

            }
        }
        else
        {
            Debug.LogError("csv Missing: " + filePath);
        }
    }
}
