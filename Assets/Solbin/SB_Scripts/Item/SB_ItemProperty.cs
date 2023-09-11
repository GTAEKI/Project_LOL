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
    public int attackDamage; // 공격력
    public int attackSpeed; // 공격 속도
    public int armor; // 방어력
    public int magicResistance; // 마법방어력
    public int health; // 체력
    public int abilityHaste; // 스킬 가속
    public int lifeSteal; // 생명력 흡수
    public int criticalStrikeChance; // 치명타 확률
    public int movementSpeed;
    public int lethality;
    public int gold = 3000; // 아이템 가격 (통일)
        
    /// <summary>
    /// 이미지 교체를 위한 메서드입니다.
    /// </summary>
    /// <param name="_sprite">ItemDataReader.cs에서 Sprite Name 받기</param>
    public void ChangeImg(string _sprite)
    {
        string sprite = _sprite;

        Image image = transform.GetComponent<Image>();
        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{sprite}");
        Debug.Assert(itemImg != null);
        image.sprite = itemImg;
    }
}