using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 골드 관리를 위한 클래스
/// </summary>

public class SB_Gold : MonoBehaviour
{
    public int m_gold;
    TMP_Text m_goldText;

    // Start is called before the first frame update
    void Start()
    {
        m_goldText = GetComponent<TMP_Text>();
        m_gold = 100000; // 임시값
        m_goldText.text = m_gold.ToString();
    }

    /// <summary>
    /// 아이템 구매시 골드 소모
    /// </summary>
    public void SpendMoney()
    {
        m_gold -= 3000;
        m_goldText.text = m_gold.ToString();
    }

    /// <summary>
    /// 아이템 되돌리기 & 판매 시 골드 회복
    /// </summary>
    public void ReturnMoney()
    {
        m_gold += 3000;
        m_goldText.text = m_gold.ToString();
    }
}
