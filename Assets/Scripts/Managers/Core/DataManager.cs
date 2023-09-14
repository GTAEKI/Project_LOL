using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    public Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    // Unit's Base Status Data
    public Dictionary<Define.UnitName, MS.Data.UnitBaseStat> UnitBaseStatDict { get; private set; } = new Dictionary<Define.UnitName, MS.Data.UnitBaseStat>();

    public void Init()
    {
        UnitBaseStatDict = LoadJson<MS.Data.UnitStatData, Define.UnitName, MS.Data.UnitBaseStat>("UnitBaseStatData").MakeDict();
    }

    private Loader LoadJson<Loader, Key, Value>(string _path) where Loader : ILoader<Key,Value>
    {
        TextAsset statData = Managers.Resource.Load<TextAsset>($"Data/{_path}");
        return JsonUtility.FromJson<Loader>(statData.text);
    }
}
