using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_MainContent : MonoBehaviour
{
    private GameObject m_legendContainer;
    private int m_legendIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_legendContainer = transform.GetChild(1).gameObject; // ���� ������ ��� & ����
        m_legendIndex = m_legendContainer.transform.childCount;

        Debug.Log(m_legendIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
