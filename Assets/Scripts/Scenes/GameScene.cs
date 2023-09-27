using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        // 게임 매니저 초기화
        Managers.Game.Init();

        // 인풋 매니저 초기화
        Managers.Input.Init();

        SceneType = Define.Scene.Game;
    }

    public override void Clear()
    {

    }
}
