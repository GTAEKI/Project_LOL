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
        }
    }
}
