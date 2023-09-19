using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    public Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    // 유닛 기본 스탯 딕셔너리
    public Dictionary<Define.UnitName, MS.Data.UnitBaseStat> UnitBaseStatDict { get; private set; } = new Dictionary<Define.UnitName, MS.Data.UnitBaseStat>();

    // 유닛 성장 스탯 딕셔너리
    public Dictionary<Define.UnitName, MS.Data.UnitGrowStat> UnitGrowStatDict { private set; get; } = new Dictionary<Define.UnitName, MS.Data.UnitGrowStat>();

    // 유닛 스킬 데이터 딕셔너리
    public Dictionary<Define.UnitName, MS.Data.UnitSkill> UnitSkillDict { private set; get; } = new Dictionary<Define.UnitName, MS.Data.UnitSkill>();

    public void Init()
    {
        UnitBaseStatDict = LoadJson<MS.Data.UnitStatData, Define.UnitName, MS.Data.UnitBaseStat>("UnitBaseStatData").MakeDict();
        UnitGrowStatDict = LoadJson<MS.Data.UnitGrowStatData, Define.UnitName, MS.Data.UnitGrowStat>("UnitGrowStatData").MakeDict();
        UnitSkillDict = LoadJson<MS.Data.UniSkillData, Define.UnitName, MS.Data.UnitSkill>("UnitSkillData").MakeDict();
    }

    private Loader LoadJson<Loader, Key, Value>(string _path) where Loader : ILoader<Key,Value>
    {
        TextAsset statData = Managers.Resource.Load<TextAsset>($"Data/{_path}");
        return JsonUtility.FromJson<Loader>(statData.text);
    }
}
