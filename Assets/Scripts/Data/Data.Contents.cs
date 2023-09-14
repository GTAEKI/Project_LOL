using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MS.Data
{
    #region StatData
    
    /// <summary>
    /// Unit Base Status
    /// KimMinSeob_230914
    /// </summary>
    [Serializable]
    public class UnitBaseStat
    {
        public int index;
        public string name_ko, name_en;
        public float maxHp, maxMp;
        public float strength;
        public float physicalDefense, magicDefense;
        public float attackSpeed;
        public float movementSpeed;
        public float hpRecovery, mpRecovery;
        public float attackRange;
    }

    [Serializable]
    public class UnitStatData : ILoader<Define.UnitName, UnitBaseStat>
    {
        public List<UnitBaseStat> unitStats = new List<UnitBaseStat>();     // json array name

        public Dictionary<Define.UnitName, UnitBaseStat> MakeDict()
        {
            Dictionary<Define.UnitName, UnitBaseStat> dict = new Dictionary<Define.UnitName, UnitBaseStat>();
            foreach (UnitBaseStat stat in unitStats)
            {
                dict.Add((Define.UnitName)stat.index, stat);
            }
            return dict;
        }
    }

    #endregion
}