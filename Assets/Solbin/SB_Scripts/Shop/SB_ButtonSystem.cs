using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SB_ButtonSystem : MonoBehaviour
{
    Button m_buyButton;
    GameObject m_item;
    SB_InvenrotySlot invenrotySlot;

    public void Start()
    {
        Transform buy = transform.GetChild(3);
        m_buyButton = buy.GetComponent<Button>();
        m_buyButton.interactable = false;

        invenrotySlot = GameObject.Find("Slot Group").transform.GetComponent<SB_InvenrotySlot>();
    }

    /// <summary>
    /// 아이템 선택 시 구매버튼 활성화. 선택한 아이템을 멤버변수에 전달함.
    /// </summary>
    /// <param name="_itemName">구매한 아이템 오브젝트</param>
    public void ActiveBuyButton(GameObject _item)
    {
        m_item = _item;
        m_buyButton.interactable = true;
    }

    /// <summary>
    /// OnClick으로 아이템 구매를 알았다면 인벤토리로 아이템을 보냄
    /// </summary>
    public void PressBuy()
    {
        GameObject item = m_item;

        invenrotySlot.ReceiveItem(item);
        m_buyButton.interactable = false;
    }
}
