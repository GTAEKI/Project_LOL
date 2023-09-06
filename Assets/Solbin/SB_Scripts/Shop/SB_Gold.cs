using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SB_Gold : MonoBehaviour
{
    int m_gold;
    TMP_Text m_goldText;

    // Start is called before the first frame update
    void Start()
    {
        m_gold = 1000; // ÀÓ½Ã°ª
        m_goldText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_goldText.text = m_gold.ToString();
    }
}
