using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 골드 관리를 위한 클래스
/// </summary>

public class SB_Gold : MonoBehaviour
{
    int m_gold;
    TMP_Text m_goldText;

    // Start is called before the first frame update
    void Start()
    {
        m_gold = 1000; // 임시값
        m_goldText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_goldText.text = m_gold.ToString();
    }
}
