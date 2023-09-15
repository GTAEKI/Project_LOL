using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SB_ItemSelect_Inven : MonoBehaviour, IPointerClickHandler
{
    SB_ButtonSystem buttonSystem;

    // Start is called before the first frame update
    void Start()
    {
        buttonSystem = GameObject.Find("Buttons").GetComponent<SB_ButtonSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 아이템 사용
        }
        else if (eventData.button == PointerEventData.InputButton.Right && SB_CallShop.m_isShop)
        {
            buttonSystem.PressSell();
        }
    }
}
