using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Stat
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> statList = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in statList)
        {
            dict.Add(stat.level, stat);
        }
        return dict;
    }
}
#endregion
