using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 속성을 담기 위한 클래스
/// </summary>

public class SB_ItemProperty : MonoBehaviour
{
    public int typeNumber; // 0: 전설, 1: 신화
    public string name; // 아이템 이름
    public string englishName; // 이미지 매칭 - 영문명
    public int attackDamage; // 공격력 5
    public int attackSpeed; // 공격 속도 6
    public int armor; // 방어력 7
    public int magicResistance; // 마법저항력 8
    public int health; // 체력 9 
    public int abilityHaste; // 스킬 가속 10
    public int lifeSteal; // 생명력 흡수 11
    public int criticalStrikeChance; // 치명타 확률 12
    public int movementSpeed; // 이동 속도 13
    public int lethality; // 물리 관통력 14
    public int armorPenetration; // 방어구 관통력 15
    public int magicPenetration; // 마법관통력 16
    public int abilityPower; // 주문력 17
    public int mana; // 마나 18
    public int basicHealthRegenaration; // 기본 체력 재생 20
    public int basicManaRegenaration; // 기본 마나 재생 21
    public int healthAndShieldPower; // 체력 회복 및 보호막 22

    public int tenacity; // 장화: 강인함

    // 방어구 관통력, 마법 관통력, 주문력, 마나, 쿨타임, 기본체력재생, 기본 마나 재생, 체력 회복 및 보호막
    // armorPenetration, 

    public int gold = 3000; // 아이템 가격 (통일)
    
        
    /// <summary>
    /// 아이템 이미지 교체를 위한 메서드입니다.
    /// </summary>
    /// <param name="_sprite">ItemDataReader.cs에서 Sprite Name 받기</param>
    public void ChangeItemImg(string _sprite)
    {
        string sprite = _sprite;

        Image image = transform.GetComponent<Image>();
        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{sprite}");
        Debug.Assert(itemImg != null);
        image.sprite = itemImg;
    }

    /// <summary>
    /// 장화 이미지 교체를 위한 메서드입니다.
    /// </summary>
    public void ChangeBootsImg(string _sprite)
    {
        string sprite = _sprite;

        Image image = transform.GetComponent<Image>();
        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Boots/{sprite}");
        Debug.Assert(itemImg != null);
        image.sprite = itemImg;
    }
}