using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        // 인풋 매니저 초기화
        Managers.Input.Init();

        UI_UnitBottomLayer bottomLayer = Managers.UI.ShowSceneUI<UI_UnitBottomLayer>();
        bottomLayer.SetTarget(Define.UnitName.Yasuo);
        bottomLayer.Init();
            
        SceneType = Define.Scene.Test;
    }

    public override void Clear()
    {
    }
}
