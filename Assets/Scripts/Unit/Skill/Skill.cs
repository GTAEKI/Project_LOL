using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSkill
{
    private PassiveSkill passive;
    private ActiveSkill[] actives;

    public PassiveSkill Passive { private set => passive = value; get => passive; }
    public ActiveSkill[] Actives { private set => actives = value; get => actives; }

    public UnitSkill(Define.UnitName unitName)
    {
        passive = new PassiveSkill(Managers.Data.UnitSkillDict[unitName].passive_cooltime);
        actives = new ActiveSkill[4]
        {
            new ActiveSkill(Managers.Data.UnitSkillDict[unitName].activeQ_cooltime),
            new ActiveSkill(Managers.Data.UnitSkillDict[unitName].activeW_cooltime),
            new ActiveSkill(Managers.Data.UnitSkillDict[unitName].activeE_cooltime),
            new ActiveSkill(Managers.Data.UnitSkillDict[unitName].activeR_cooltime),
        };
    }
}

public class Skill
{
    private float cooltime;

    public float Cooltime => cooltime;

    public Skill(float cooltime)
    {
        this.cooltime = cooltime;
    }
}

public class PassiveSkill : Skill
{
    public PassiveSkill(float cooltime) : base(cooltime) { }
}

public class ActiveSkill : Skill
{
    public ActiveSkill(float cooltime) : base(cooltime) { }
}
