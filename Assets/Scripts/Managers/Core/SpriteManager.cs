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

        Sprite[] iconSprites = Resources.LoadAll<Sprite>("Sprites/UI/UnitSkill/Yasuo");
        Dictionary<string, Sprite> iconDict = new Dictionary<string, Sprite>();

        for(int i = 0; i < iconSprites.Length; i++)
        {
            iconDict.Add(iconSprites[i].name, iconSprites[i]);
        }

        unitSkill_IconDict.Add(Define.UnitName.Yasuo, iconDict);

        Sprite[] frameSprites = Resources.LoadAll<Sprite>("Sprites/UI/UnitFrame/Yasuo");
        Dictionary<string, Sprite> frameDict = new Dictionary<string, Sprite>();

        for(int i = 0; i < frameSprites.Length; i++)
        {
            frameDict.Add(frameSprites[i].name, frameSprites[i]);
        }

        unitFrame_IconDict.Add(Define.UnitName.Yasuo, frameDict);
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
