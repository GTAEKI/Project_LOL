using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using System.Threading;
using static UnityEditor.Progress;

public class SB_ButtonSystem : MonoBehaviour
{
    Button m_buyButton; // 구매 버튼
    Button m_returnButton; // 되돌리기 버튼
    Button m_sellButton; // 판매 버튼 
    GameObject m_item;
    SB_InvenrotySlot invenrotySlot;
    SB_TabInventorySlot tabInventorySlot;
    SB_Gold gold;

    public static event EventHandler buyItem; // 구매 이벤트
    public static event EventHandler returnItem; // 되돌리기 이벤트
    public static event EventHandler sellItem; // 판매 이벤트

    List<GameObject> buyList = new List<GameObject>(); // 구매한 아이템 리스트

    GameObject content; // 상점 아이템 목록
    GameObject popupContent; // 팝업 상점 아이템 목록

    public void Start()
    {
        Transform buy = transform.GetChild(3);
        m_buyButton = buy.GetComponent<Button>();
        m_buyButton.interactable = false;
        m_returnButton = transform.GetChild(2).GetComponent<Button>();
        m_returnButton.interactable = false;
        m_sellButton = transform.GetChild(1).GetComponent<Button>();
        m_sellButton.interactable = false;

        invenrotySlot = GameObject.Find("Slot Group").transform.GetComponent<SB_InvenrotySlot>();
        tabInventorySlot = GameObject.Find("TapUI").GetComponent<SB_TabInventorySlot>();
        gold = GameObject.Find("Inven Gold").transform.GetComponent<SB_Gold>();

        content = GameObject.Find("Content");
        popupContent = GameObject.Find("Side Shop_Down");
    }

    /// <summary>
    /// 아이템 선택 시 구매버튼 활성화. 선택한 아이템을 멤버변수에 전달함.
    /// </summary>
    /// <param name="_itemName">구매한 아이템 오브젝트</param>
    public void ActiveBuyButton(GameObject _item)
    {
        if (gold.m_gold >= 3000 && buyList.Count + 1 < 7) // 아직 Count 전이라 +1
        {
            m_item = _item;
            m_buyButton.interactable = true;
        }
    }

    public void ClickRightButton(GameObject _item)
    {
        if (gold.m_gold >= 3000 && buyList.Count + 1 < 7) // 아직 Count 전이라 +1
        {
            m_item = _item;
            PressBuy();
        }
    }

    /// <summary>
    /// OnClick으로 아이템 구매를 알았다면 인벤토리로 아이템을 보냄
    /// </summary>
    public void PressBuy()
    {
        GameObject item = m_item;

        int whatType = m_item.GetComponent<SB_ItemProperty>().typeNumber; // 아이템 정보 타입(전설, 장화, 신화)

        buyList.Add(item);

        invenrotySlot.ReceiveItem(item);
        tabInventorySlot.ReceiveItem(item);

        m_buyButton.interactable = false;
        m_item.GetComponent<Button>().interactable = false; // 선택한 아이템 버튼 비활성화

        if (whatType == 1) // 신화 아이템 구매 시
            BanMyth();
        else if (whatType == 2) // 장화 아이템 구매 시
            BanBoots();

        buyItem?.Invoke(this, EventArgs.Empty);

        ActiveReturnButton();
        ActiveSellButton();
    }

    /// <summary>
    /// 신화 아이템 중복 구매 불가
    /// </summary>
    void BanMyth()
    {
        TMP_Text mythText = content.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>();
        Button[] mythChildren = content.transform.GetChild(1).transform.GetChild(1).GetComponentsInChildren<Button>();
        Debug.Assert(mythChildren != null);

        for (int i = 0; i < mythChildren.Length; i++)
        {
            mythChildren[i].interactable = false;
        }

        mythText.text = "신화급 (보유 중)";
    }

    /// <summary>
    /// 장화 아이템 중복 구매 불가
    /// </summary>
    void BanBoots()
    {
        Button[] mythChildren = popupContent.GetComponentsInChildren<Button>();
        Debug.Assert(mythChildren != null);

        for (int i = 0; i < mythChildren.Length; i++)
        {
            mythChildren[i].interactable = false;
        }
    }

    /// <summary>
    /// 인벤에 신화 아이템이 없다면 재활성화
    /// </summary>
    void ActiveMyth()
    {
        TMP_Text mythText = content.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>();
        Button[] mythChildren = content.transform.GetChild(1).transform.GetChild(1).GetComponentsInChildren<Button>();
        Debug.Assert(mythChildren != null);

        for (int i = 0; i < mythChildren.Length; i++)
        {
            mythChildren[i].interactable = true;
        }

        mythText.text = "신화급";
    }

    /// <summary>
    /// 인벤에 장화 아이템이 없다면 재활성화
    /// </summary>
    void ActiveBoot()
    {
        Button[] mythChildren = popupContent.GetComponentsInChildren<Button>();
        Debug.Assert(mythChildren != null);

        for (int i = 0; i < mythChildren.Length; i++)
        {
            mythChildren[i].interactable = true;
        }
    }

    /// <summary>
    /// 구매와 동시에 되돌리기 버튼을 활성화
    /// </summary>
    public void ActiveReturnButton()
    {
        m_returnButton.interactable = true;
    }

    public void ActiveSellButton()
    {
        m_sellButton.interactable = true;
    }

    public void PressReturn()
    {
        invenrotySlot.ReturnItem();
        returnItem?.Invoke(this, EventArgs.Empty); // 골드, Tab 인벤토리
        ActiveBuyButton(buyList[buyList.Count - 1]);
        buyList[buyList.Count - 1].GetComponent<Button>().interactable = true;
        buyList.RemoveAt(buyList.Count - 1); // 마지막 구매 아이템 리스트에서 제거

        if (buyList.Count <= 0)
        {
            m_returnButton.interactable = false;
            m_sellButton.interactable = false;
        }

        int mythCount = 0;
        int bootCount = 0;

        for (int i = 0; i < buyList.Count; i++)
        {
            if (buyList[i].GetComponent<SB_ItemProperty>().typeNumber == 1)
                mythCount += 1;

            if (buyList[i].GetComponent<SB_ItemProperty>().typeNumber == 2)
                bootCount += 1;
        }

        if (mythCount <= 0)
        {
            ActiveMyth();
        }
        
        if (bootCount <= 0)
        {
            ActiveBoot();
        }
    }

    public void PressSell()
    {
        invenrotySlot.ReturnItem();
        sellItem?.Invoke(this, EventArgs.Empty); // 골드, Tab 인벤토리
        ActiveBuyButton(buyList[buyList.Count - 1]);
        buyList[buyList.Count - 1].GetComponent<Button>().interactable = true;
        buyList.RemoveAt(buyList.Count - 1); // 마지막 구매 아이템 리스트에서 제거

        if (buyList.Count <= 0)
        {
            m_sellButton.interactable = false;
            m_returnButton.interactable = false;
        }

        int mythCount = 0;
        int bootCount = 0;

        for (int i = 0; i < buyList.Count; i++)
        {
            if (buyList[i].GetComponent<SB_ItemProperty>().typeNumber == 1)
                mythCount += 1;

            if (buyList[i].GetComponent<SB_ItemProperty>().typeNumber == 2)
                bootCount += 1;
        }

        if (mythCount <= 0)
        {
            ActiveMyth();
        }

        if (bootCount <= 0)
        {
            ActiveBoot();
        }
    }
}

// 환불가격 75%
