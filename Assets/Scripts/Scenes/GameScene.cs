using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // 기본 Scene UI
        Managers.UI.ShowSceneUI<UI_UnitBottomLayer>().SetTarget(Define.UnitName.Yasuo); // 플레이어 하단 UI 생성
    }

    public override void Clear()
    {

    }
}
