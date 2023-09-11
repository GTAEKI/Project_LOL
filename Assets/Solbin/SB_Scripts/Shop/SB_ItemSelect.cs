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
    Canvas m_canvas;
    TMP_Text m_largeItemInfo;
    GameObject m_smallItemInfo;
    SB_ItemProperty m_itemProperty;
    Button m_buyButton;

    SB_ButtonSystem m_buttonSystem;

    bool hoverMouse = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        m_largeItemInfo = GameObject.Find("Explain Window").GetComponent<TMP_Text>();
        m_smallItemInfo = GameObject.Find("Item Info");
        m_itemProperty = transform.GetComponent<SB_ItemProperty>();

        m_buttonSystem = GameObject.Find("Background").transform.GetComponent<SB_ButtonSystem>();

    }

    void Update()
    {
        // 인포창이 마우스를 따라다니도록
        if (hoverMouse)
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);




        }
    }    

    /// <summary>
    /// 아이콘 위: 아이템 이름 출력
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverMouse = true;
    }

    /// <summary>
    /// 아이콘 바깥: 아이콘 이름 지우기
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverMouse = false;
    }

    /// <summary>
    /// 아이템 클릭, 선택된 아이템의 이름 전달
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        m_largeItemInfo.text = m_itemProperty.name; // 아이템 이름을 정보 창에 띄운다.
        m_buttonSystem.ActiveBuyButton(gameObject);
    }
}
