using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SB_InvertoryItemSelect : MonoBehaviour, IPointerClickHandler
{
    SB_CallShop callShop;
    SB_ButtonSystem buttonSystem;
    SB_InvenrotySlot invenrorySlot;
    SB_Gold gold;

    void Start()
    {
        callShop = GameObject.Find("Shop").GetComponent<SB_CallShop>();
        buttonSystem = GameObject.Find("Buttons").GetComponent<SB_ButtonSystem>();
        invenrorySlot = GameObject.Find("Slot Group").GetComponent <SB_InvenrotySlot>();
        gold = GameObject.Find("Inven Gold").GetComponent<SB_Gold>();
    }

    /// <summary>
    /// 인벤토리 아이템 사용 & 판매
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 아이템 사용
        }
        else if (eventData.button == PointerEventData.InputButton.Right && callShop.m_isShop)
        {
            buttonSystem.ClickRightButton_Sell(gameObject); 
            invenrorySlot.ReturnItem();
            gold.EarnMoney();
        }
    }
}
