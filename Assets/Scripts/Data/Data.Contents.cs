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

    /// <summary>
    /// 유닛 성장 스탯 클래스
    /// 김민섭_230917
    /// </summary>
    [Serializable]
    public class UnitGrowStat
    {
        public int index;
        public string name_ko, name_en;
        public float growHp, growMp;
        public float growStrength;
        public float growDefense, growMagicDefense;
        public float growAttackSpeed;
        public float growMovementSpeed;
        public float growHpRecovery, growMpRecovery;
        public float growAttackRange;
    }

    [Serializable]
    public class UnitGrowStatData : ILoader<Define.UnitName, UnitGrowStat>
    {
        public List<UnitGrowStat> unitGrowStats = new List<UnitGrowStat>();     // json array name

        public Dictionary<Define.UnitName, UnitGrowStat> MakeDict()
        {
            Dictionary<Define.UnitName, UnitGrowStat> dict = new Dictionary<Define.UnitName, UnitGrowStat>();
            foreach (UnitGrowStat stat in unitGrowStats)
            {
                dict.Add((Define.UnitName)stat.index, stat);
            }
            return dict;
        }
    }

    /// <summary>
    /// 유닛 스킬 관련 데이트 클래스
    /// 김민섭_230917
    /// </summary>
    [Serializable]
    public class UnitSkill
    {
        public int index;
        public string name_ko, name_en;
        public float passive_cooltime;
        public float activeQ_cooltime;
        public float activeW_cooltime;
        public float activeE_cooltime;
        public float activeR_cooltime;
    }

    [Serializable]
    public class UniSkillData : ILoader<Define.UnitName, UnitSkill>
    {
        public List<UnitSkill> unitSkill = new List<UnitSkill>();     // json array name

        public Dictionary<Define.UnitName, UnitSkill> MakeDict()
        {
            Dictionary<Define.UnitName, UnitSkill> dict = new Dictionary<Define.UnitName, UnitSkill>();
            foreach (UnitSkill stat in unitSkill)
            {
                dict.Add((Define.UnitName)stat.index, stat);
            }
            return dict;
        }
    }

    #endregion
}