using System.Collections.Generic;
using UnityEngine;

public class SB_GatherStatus : MonoBehaviour
{
    public int attackDamege = 0;
    public int attackSpeed = 0;
    public int armor = 0;
    public int magicResistance = 0;
    public int health = 0;
    public int abilityHaste = 0;
    public int lifeSteal = 0; // 생명력 흡수
    public int criticalStrikeChance = 0; // 치명타 확률
    public int movementSpeed = 0;
    public int lethality = 0;
    public int armorPenetration; // 방어구 관통력 
    public int magicPenetration; // 마법관통력 
    public int abilityPower; // 주문력 
    public int mana; // 마나 
    public int basicHealthRegenaration; // 기본 체력 재생 
    public int basicManaRegenaration; // 기본 마나 재생 
    public int healthAndShieldPower; // 체력 회복 및 보호막 

    /// <summary>
    /// 인벤토리의 아이템 속성을 모두 합칩니다. (공격력 등)
    /// </summary>
    /// <param name="_itemList">인벤토리 아이템 리스트</param>
    public void AllItemStatus(List<GameObject> _itemList)
    {
        List<GameObject> itemList = _itemList;

        for (int i = 0; i < itemList.Count; i++)
        {
            attackDamege += itemList[i].GetComponent<SB_ItemProperty>().attackDamage;
            attackSpeed += itemList[i].GetComponent<SB_ItemProperty>().attackSpeed;
            armor += itemList[i].GetComponent<SB_ItemProperty>().armor;
            magicResistance += itemList[i].GetComponent<SB_ItemProperty>().magicResistance;
            health += itemList[i].GetComponent<SB_ItemProperty>().health;
            abilityHaste += itemList[i].GetComponent<SB_ItemProperty>().abilityHaste;
            lifeSteal += itemList[i].GetComponent<SB_ItemProperty>().lifeSteal;
            criticalStrikeChance += itemList[i].GetComponent<SB_ItemProperty>().criticalStrikeChance;
            movementSpeed += itemList[i].GetComponent<SB_ItemProperty>().movementSpeed;
            lethality += itemList[i].GetComponent<SB_ItemProperty>().lethality;
            armorPenetration += itemList[i].GetComponent<SB_ItemProperty>().armorPenetration;
            magicPenetration += itemList[i].GetComponent<SB_ItemProperty>().magicPenetration;
            abilityPower += itemList[i].GetComponent<SB_ItemProperty>().abilityPower;
            mana += itemList[i].GetComponent<SB_ItemProperty>().mana;
            basicHealthRegenaration += itemList[i].GetComponent<SB_ItemProperty>().basicHealthRegenaration;
            basicManaRegenaration += itemList[i].GetComponent<SB_ItemProperty>().basicManaRegenaration;
            healthAndShieldPower += itemList[i].GetComponent<SB_ItemProperty>().healthAndShieldPower;
        }
    }
}
