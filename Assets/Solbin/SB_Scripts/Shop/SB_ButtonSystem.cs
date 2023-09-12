using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SB_ButtonSystem : MonoBehaviour
{
    Button m_buyButton; // 구매 버튼
    Button m_returnButton;
    GameObject m_item;
    SB_InvenrotySlot invenrotySlot;
    SB_TabInventorySlot tabInventorySlot;
    SB_Gold gold;

    public UnityEvent buyItem;
    public UnityEvent sellItem;

    List<GameObject> buyList = new List<GameObject>(); // 구매한 아이템 리스트

    public void Start()
    {
        Transform buy = transform.GetChild(3);
        m_buyButton = buy.GetComponent<Button>();
        m_buyButton.interactable = false;
        m_returnButton = transform.GetChild(2).GetComponent<Button>();
        m_returnButton.interactable = false;

        invenrotySlot = GameObject.Find("Slot Group").transform.GetComponent<SB_InvenrotySlot>();
        tabInventorySlot = GameObject.Find("TapUI").GetComponent<SB_TabInventorySlot>();
        gold = GameObject.Find("Inven Gold").transform.GetComponent<SB_Gold>();
    }

    /// <summary>
    /// 아이템 선택 시 구매버튼 활성화. 선택한 아이템을 멤버변수에 전달함.
    /// </summary>
    /// <param name="_itemName">구매한 아이템 오브젝트</param>
    public void ActiveBuyButton(GameObject _item)
    {
        if (gold.m_gold > 3000)
        {
            m_item = _item;
            m_item.GetComponent<Button>().interactable = true; // 선택한 아이템 버튼 활성화
            m_buyButton.interactable = true;
        }
    }

    /// <summary>
    /// OnClick으로 아이템 구매를 알았다면 인벤토리로 아이템을 보냄
    /// </summary>
    public void PressBuy()
    {
        GameObject item = m_item;

        buyList.Add(item);

        invenrotySlot.ReceiveItem(item);
        tabInventorySlot.ReceiveItem(item);

        m_buyButton.interactable = false;
        m_item.GetComponent<Button>().interactable = false; // 선택한 아이템 버튼 비활성화

        buyItem.Invoke();

        ActiveReturnButton();
    }

    /// <summary>
    /// 구매와 동시에 되돌리기 버튼을 활성화
    /// </summary>
    public void ActiveReturnButton()
    {
        m_returnButton.interactable = true;
    }

    public void PressReturn()
    {
        invenrotySlot.ReturnItem();
        sellItem.Invoke(); // 골드, Tab 인벤토리
        ActiveBuyButton(buyList[buyList.Count - 1]);
        buyList.RemoveAt(buyList.Count - 1); // 마지막 구매 아이템 리스트에서 제거
    }
}
