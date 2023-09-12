using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SB_TabInventorySlot : MonoBehaviour
{
    Transform firstTeam;
    int itemCounter = 0;

    private void Start()
    {
        firstTeam = transform.GetChild(0).transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab)) // Tab키를 누르면 동작 
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

    /// <summary>
    /// Tab 상태정보창에 아이템을 출력
    /// </summary>
    /// <param name="_item">구매 아이템</param>
    public void ReceiveItem(GameObject _item)
    {
        GameObject item = _item;
        Transform firstPlayerItem = firstTeam.transform.GetChild(1).transform.GetChild(4);

        string itemName = _item.GetComponent<SB_ItemProperty>().englishName;
        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{itemName}");

        firstPlayerItem.GetChild(itemCounter).GetComponent<Image>().sprite = itemImg;

        itemCounter++; // 슬롯 아이템 개수
    }

    /// <summary>
    /// ButtonSystem 이벤트 받아서 최근 등록된 아이템 반환
    /// </summary>
    public void ReturnItem()
    {
        itemCounter--;

        Debug.Log(itemCounter);
        Transform firstPlayerItem = firstTeam.transform.GetChild(1).transform.GetChild(4);

        firstPlayerItem.GetChild(itemCounter).GetComponent<Image>().sprite = null;

    }
}
