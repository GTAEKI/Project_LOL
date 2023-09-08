using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SB_ItemSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TMP_Text m_itemInfo;
    SB_ItemProperty m_itemProperty;
    Button m_buyButton;

    SB_ButtonSystem m_buttonSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        m_itemInfo = GameObject.Find("Explain Window").GetComponent<TMP_Text>();
        m_itemProperty = transform.GetComponent<SB_ItemProperty>();

        m_buttonSystem = GameObject.Find("Background").transform.GetComponent<SB_ButtonSystem>();
    }

    /// <summary>
    /// 아이콘 위: 아이템 이름 출력
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 아이콘 바깥: 아이콘 이름 지우기
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 아이템 클릭, 선택된 아이템의 이름 전달
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        m_itemInfo.text = m_itemProperty.name; // 아이템 이름을 정보 창에 띄운다.
        m_buttonSystem.ActiveBuyButton(gameObject);
    }
}
