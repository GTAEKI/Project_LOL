using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private int gold;       // 플레이어 보유 골드

    public Unit PlayerUnit { private set; get; }            // 플레이어 유닛 (캐릭터)
    public int Gold
    {
        get => gold;
        set
        {
            gold = value;

            // UI
            UI_UnitBottomLayer bottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
            bottomLayer?.SetGold(gold);
        }
    }
    public int Hp { private set; get; }                     // 플레이어 체력 (캐릭터 체력과 다름)
    public bool IsDie { private set; get; } = false;        // 플레이어 탈락 유무 체크

    /// <summary>
    /// 골드 증감 함수
    /// 김민섭_230926
    /// </summary>
    /// <param name="value"></param>
    public void OnChangeGold(int value)
    {
        Gold += value;

        if (Gold < 0) Gold = 0;
    }

    /// <summary>
    /// 플레이어 유닛 세팅 함수
    /// 김민섭_230926
    /// </summary>
    /// <param name="unit">선택한 유닛</param>
    public void SettingUnit(Unit unit) => PlayerUnit = unit;

    /// <summary>
    /// 게임 체력 감소 함수
    /// 김민섭_230926
    /// </summary>
    /// <param name="dmg">받은 피해량</param>
    public void OnDamaged(int dmg)
    {
        Hp -= dmg;

        if (Hp < 0)
        {
            Hp = 0;
            IsDie = true;
        }
    }
}
