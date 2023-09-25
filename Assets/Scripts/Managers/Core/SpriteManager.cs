using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager
{
    // 유닛 스킬 아이콘 딕셔너리
    public Dictionary<Define.UnitName, Dictionary<string, Sprite>> unitSkill_IconDict { private set; get; } = new Dictionary<Define.UnitName, Dictionary<string, Sprite>>();

    // 유닛 초상화 아이콘 딕셔너리
    public Dictionary<Define.UnitName, Dictionary<string, Sprite>> unitFrame_IconDict { private set; get; } = new Dictionary<Define.UnitName, Dictionary<string, Sprite>>();

    public void Init()
    {
        if(unitSkill_IconDict.Count > 0) unitSkill_IconDict.Clear();

        // 유닛 스킬 아이콘
        for(int i = (int)Define.UnitName.Rumble; i <= (int)Define.UnitName.Yasuo; i++)
        {
            string pathUnitName = ((Define.UnitName)i).ToString();
            Sprite[] iconSprites = Managers.Resource.LoadAll<Sprite>($"Sprites/UI/UnitSkill/{pathUnitName}");
            Dictionary<string, Sprite> iconDict = new Dictionary<string, Sprite>();

            for(int j = 0; j < iconSprites.Length; j++)
            {
                iconDict.Add(iconSprites[j].name, iconSprites[j]);
            }

            unitSkill_IconDict.Add((Define.UnitName)i, iconDict);
        }

        // 유닛 초상화 아이콘
        for(int i = (int)Define.UnitName.Rumble; i <= (int)Define.UnitName.Yasuo; i++)
        {
            string pathUnitName = ((Define.UnitName)i).ToString();
            Sprite[] frameSprites = Resources.LoadAll<Sprite>($"Sprites/UI/UnitFrame/{pathUnitName}");
            Dictionary<string, Sprite> frameDict = new Dictionary<string, Sprite>();

            for(int j = 0; j < frameSprites.Length; j++)
            {
                frameDict.Add(frameSprites[j].name, frameSprites[j]);
            }

            unitFrame_IconDict.Add((Define.UnitName)i, frameDict);
        }
    }

    public Sprite GetSkillIcon(Define.UnitName unitName, string iconName)
    {
        return unitSkill_IconDict[unitName][iconName];
    }

    public Sprite GetFrame(Define.UnitName unitName, string iconName)
    {
        return unitFrame_IconDict[unitName][iconName];
    }
}
