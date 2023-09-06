using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SB_ShopSystem : MonoBehaviour
{
    private bool m_isShop = false;
    private RectTransform m_shopTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_shopTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!m_isShop)
                OpenShop();
            else
                ExitShop();
        }
    }

    /// <summary>
    /// P입력을 받아 상점창 열기
    /// 노솔빈_230906
    /// </summary>
    void OpenShop()
    {
        Vector2 onMonitor = new Vector2(-200, 0);

        m_shopTransform.anchoredPosition = onMonitor;
        m_isShop = true;
    }

    /// <summary>
    /// P입력을 받아 상점창 닫기
    /// 노솔빈_230906
    /// </summary>
    public void ExitShop()
    {
        Vector2 outMonitor = new Vector2(-1500, 0);

        m_shopTransform.anchoredPosition = outMonitor;
        m_isShop = false;
    }
}
