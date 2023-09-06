using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SB_PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool m_isPop = false;
    private RectTransform m_popupTransform;

    void Start()
    {
        m_popupTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Ŀ���� �ö���� �˾�, �ƴϸ� �������
    /// </summary>
    /// <param name="eventData">Ŀ�� ����</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_isPop)
            PopUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_isPop)
            Collapse();
    }

    /// <summary>
    /// �˾��� ����ġ ����
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
