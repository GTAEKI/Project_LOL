using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    public Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    private Loader LoadJson<Loader, Key, Value>(string _path) where Loader : ILoader<Key,Value>
    {
        TextAsset statData = Managers.Resource.Load<TextAsset>($"Data/{_path}");
        return JsonUtility.FromJson<Loader>(statData.text);
    }
}
