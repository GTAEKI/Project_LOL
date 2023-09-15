using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SB_InvenrotySlot : MonoBehaviour, IDropHandler
{
    GameObject[] m_slots;
    List<GameObject> m_items = new List<GameObject>();

    SB_GatherStatus gatherStatus;

    int m_childCount; // 자식 오브젝트 개수
    int m_leftSlot; // 남은 슬롯 개수

    // Start is called before the first frame update
    void Start()
    {
        m_childCount = transform.childCount;
        m_slots = new GameObject[m_childCount]; // 슬롯 최대 개수 = 아이템 최대 개수
        for (int i = 0; i < m_childCount; i++)
        {
            m_slots[i] = transform.GetChild(i).gameObject;
        }

        gatherStatus = transform.parent.GetComponent<SB_GatherStatus>();
    }

    /// <summary>
    /// 구매한 아이템을 상점에서 전달받아 인벤토리에 배치.
    /// </summary>
    /// <param name="_item">ButtonSystem에서 전달받은 구매 아이템</param>
    public void ReceiveItem(GameObject _item)
    {
        GameObject itemContainer = this.gameObject;
        GameObject item = _item;

        m_leftSlot = m_childCount - m_items.Count; // 남은 슬롯 개수 = 슬롯 개수 - 아이템 개수
        m_items.Add(Instantiate(item)); // 상점 아이템을 복사한 것을 아이템 리스트에 추가함.

        m_items[m_items.Count - 1].transform.SetParent(itemContainer.transform, false); // 위치 수정
        m_items[m_items.Count - 1].transform.SetSiblingIndex(m_items.Count - 1);

        //if ((m_items.Count - 1) > )
        Debug.Log(m_items.Count - 1);
        m_items[m_items.Count - 1].transform.GetComponent<SB_ItemSelect>().enabled = false; // 상점 아이템 선택 X
        m_items[m_items.Count - 1].transform.GetComponent<SB_ItemSelect_Inven>().enabled = true; // 인벤 아이템 선택 O

        m_slots[m_items.Count - 1].SetActive(false); // 원 슬롯 비활성화

        gatherStatus.AllItemStatus(m_items);
    }

    public void ReturnItem()
    {
        Destroy(m_items[m_items.Count - 1]); // error
        m_slots[m_items.Count - 1].SetActive(true); // 원 슬롯 활성화
        m_items.RemoveAt(m_items.Count - 1);
    }

    /// <summary>
    /// 하단 인벤토리 내 드래그 앤 드롭 
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnDrop(PointerEventData eventData)
    {
        // 아이템 추가 후 드래그 앤 드롭 구현
    }
}
