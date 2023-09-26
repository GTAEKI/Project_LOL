using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SB_ItemSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject m_largeItemInfo;
    GameObject m_smallItemInfo;
    SB_ItemProperty m_itemProperty;
    Button m_buyButton;
    SB_ButtonSystem m_buttonSystem;

    Camera UICamera;

    public static bool hoverMouse = false;

    private (string, int)[] itemProperties;

    // Start is called before the first frame update
    void Start()
    {
        m_largeItemInfo = GameObject.Find("Explain Window"); // 큰 화면 설명창
        m_smallItemInfo = GameObject.Find("Item Info"); // 작은 화면 설명창
        m_itemProperty = transform.GetComponent<SB_ItemProperty>();
        m_buttonSystem = GameObject.Find("Buttons").transform.GetComponent<SB_ButtonSystem>();

        UICamera = GameObject.Find("UI Camera").transform.GetComponent<Camera>();

        itemProperties = new (string, int)[] // 아이템 속성과 설명 튜플 배열
        {
            ("공격력", m_itemProperty.attackDamage),
            ("공격 속도", m_itemProperty.attackSpeed),
            ("방어력", m_itemProperty.armor),
            ("마법 방어력", m_itemProperty.magicResistance),
            ("체력", m_itemProperty.health),
            ("스킬 가속", m_itemProperty.abilityHaste),
            ("생명력 흡수", m_itemProperty.lifeSteal),
            ("치명타 확률", m_itemProperty.criticalStrikeChance),
            ("이동 속도", m_itemProperty.movementSpeed),
            ("물리 관통력", m_itemProperty.lethality)
        };
    }

    void Update()
    {
        if (!hoverMouse)
        {
            RectTransform infoRect = m_smallItemInfo.transform as RectTransform;
            infoRect.anchoredPosition = new Vector2(-1765, -550);
        }
    }

    /// <summary>
    /// 아이콘 위 마우스 진입
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ///<point> 제대로 작동하지 않음.

        PrintInfoText();

        hoverMouse = true;

        RectTransform infoRect = m_smallItemInfo.transform.GetComponent<RectTransform>();

        Vector3 mousePosition = Input.mousePosition;
        Vector2 canvasLocalPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (m_smallItemInfo.transform.parent as RectTransform, mousePosition, UICamera, out canvasLocalPosition);
        canvasLocalPosition.y -= 50;

        // UI 요소의 위치를 설정합니다.
        infoRect.localPosition = canvasLocalPosition;
    }

    /// <summary>
    /// 작은 설명창 출력
    /// </summary>
    public void PrintInfoText()
    {
        Transform infoWindow = m_smallItemInfo.transform.GetChild(0);
        Image image = infoWindow.GetChild(0).GetComponent<Image>();
        TMP_Text itemName = infoWindow.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text itemProperty = infoWindow.GetChild(2).GetComponent<TMP_Text>();

        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{m_itemProperty.englishName}");

        if (itemImg == null)
        {
            itemImg = Resources.Load<Sprite>($"Item Img/Boots/{m_itemProperty.englishName}");

            if (itemImg == null)
            {
                itemImg = Resources.Load<Sprite>($"Item Img/Myth/{m_itemProperty.englishName}");
            }
        }

        image.sprite = itemImg;
        itemName.text = m_itemProperty.name;

        string allProperty = string.Empty;
        foreach ((string propertyName, int propertyValue) in itemProperties) // 튜플의 아이템 속성 출력
        {
            if (propertyValue > 0)
            {
                allProperty += $"{propertyName} : {propertyValue}\n"; // 아이템 속성 누적
            }
        }

        itemProperty.text = allProperty;

        // 텍스트의 선호값에 따라 설명창 크기 조정
        float preferedHeight = LayoutUtility.GetPreferredHeight(itemProperty.GetComponent<RectTransform>());

        Transform backgroundBox = m_smallItemInfo.transform.GetChild(0);
        backgroundBox.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 150 + preferedHeight);
    }

    /// <summary>
    /// 아이콘 밖으로 마우스 나가면
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverMouse = false;
    }

    /// <summary>
    /// 아이템 클릭 시 선택된 아이템의 이름 전달, 큰 설명창 출력
    /// </summary>
    /// <param name="eventData">입력받기</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Image image = m_largeItemInfo.transform.GetChild(0).GetComponent<Image>();
            TMP_Text itemName = m_largeItemInfo.transform.GetChild(1).GetComponent<TMP_Text>();
            Color color = image.color;
            color.a = 1;
            image.color = color;

            Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{m_itemProperty.englishName}");

            if (itemImg == null)
            {
                itemImg = Resources.Load<Sprite>($"Item Img/Boots/{m_itemProperty.englishName}");

                if (itemImg == null)
                {
                    itemImg = Resources.Load<Sprite>($"Item Img/Myth/{m_itemProperty.englishName}");
                }
            }

            image.sprite = itemImg;
            itemName.text = m_itemProperty.name;

            m_buttonSystem.ActiveBuyButton(gameObject);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            m_buttonSystem.ClickRightButton(gameObject);
        }
    }
}
