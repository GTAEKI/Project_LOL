using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GatherStatus : MonoBehaviour
{
    public void AllItemStatus(List<GameObject> _itemList)
    {
        List<GameObject> itemList = _itemList;

        int attackDamege = 0;
        int attackSpeed = 0;
        int armor = 0;
        int magicResistance = 0;
        int health = 0;
        int abilityHaste = 0;
        int lifeSteal = 0; // 생명력 흡수
        int criticalStrikeChance = 0; // 치명타 확률
        int movementSpeed = 0;
        int lethality = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            attackDamege += itemList[i].GetComponent<SB_ItemProperty>().attackDamage;
            attackSpeed += itemList[i].GetComponent<SB_ItemProperty>().attackSpeed;
            armor += itemList[i].GetComponent<SB_ItemProperty>().armor;
            magicResistance  += itemList[i].GetComponent<SB_ItemProperty>().magicResistance;
            health += itemList[i].GetComponent<SB_ItemProperty>().health;
            abilityHaste += itemList[i].GetComponent<SB_ItemProperty>().abilityHaste;
            lifeSteal += itemList[i].GetComponent<SB_ItemProperty>().lifeSteal;
            criticalStrikeChance += itemList[i].GetComponent<SB_ItemProperty>().criticalStrikeChance;
            movementSpeed += itemList[i].GetComponent<SB_ItemProperty>().movementSpeed;
            lethality += itemList[i].GetComponent<SB_ItemProperty>().lethality;
        }
    }
}
