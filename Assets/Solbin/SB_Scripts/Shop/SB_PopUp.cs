using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SB_PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool m_isPop = false;
    private RectTransform m_popupTransform;

    void Start()
    {
        m_popupTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 커서가 올라오면 팝업, 아니면 원래대로
    /// </summary>
    /// <param name="eventData">커서 감지</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_isPop)
            PopUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_isPop && !SB_ItemSelect.hoverMouse) // 아이템 버튼 위 & 상점 UI 위에 존재하면
            Collapse();
    }

    /// <summary>
    /// 팝업과 원위치 구현
    /// </summary>
    void PopUp()
    {
        Vector2 popup = new Vector2(-95, m_popupTransform.anchoredPosition.y);

        m_popupTransform.anchoredPosition = popup;
        m_isPop = true;
    }

    void Collapse()
    {
        Vector2 collapse = new Vector2(25, m_popupTransform.anchoredPosition.y);

        m_popupTransform.anchoredPosition = collapse;
        m_isPop = false;
    }
}
