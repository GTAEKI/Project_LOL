using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class strengthDataReader : MonoBehaviour
{
    Strength[] strength;

    void Start()
    {
        string strengthFilePath = "Assets/JungKeun/JK_csv/StrengthInfo.csv";

        if(File.Exists(strengthFilePath))
        {
            string[] lines = File.ReadAllLines(strengthFilePath);
            int strengthAccount = lines.Length - 1; // 헤더 제외 품목 개수
            string[] value = new string[strengthAccount];
            GameObject[] newstrength = new GameObject[strengthAccount];


            for(int i=1; i< lines.Length; i++)
            {
                value = lines[i].Split(",");
                strength[i].intdexNumber = int.Parse(value[0]);
                strength[i].name = value[1];
                strength[i].englishName = value[2];
                strength[i].Apper = int.Parse(value[3]);
                strength[i].Ap = int.Parse(value[4]);
                strength[i].AttackSpeed = int.Parse(value[5]);
                strength[i].atkper = int.Parse(value[6]);
                strength[i].atk = int.Parse(value[7]);
                strength[i].Hp = int.Parse(value[8]);
                strength[i].SkillBoost = int.Parse(value[9]);
                strength[i].CriticalChance = int.Parse(value[10]);
                strength[i].MovementSpeed = int.Parse(value[11]);
                strength[i].ArmorPenetration = int.Parse(value[12]);
                strength[i].ArmorPenetrationper = int.Parse(value[13]);
                strength[i].MagicPenetration = int.Parse(value[14]);
                strength[i].MagicPenetrationper = int.Parse(value[15]);

            }

        }
        else
        {
            Debug.LogError("csv Missing: " + strengthFilePath);
        }
    }
}
