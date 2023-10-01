using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TopLayer : UI_Scene
{
    private enum GameObjects
    {
        BaseInterface
    }

    public List<TeamBoxHp> TeamBoxes { private set; get; } = new List<TeamBoxHp>();

    public override void Init()
    {
        if (isInit) return;
        isInit = true;

        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        for(int i = 0; i < Managers.Game.players.Count; i++)
        {
            TeamBoxHp box = Managers.UI.MakeSubItem<TeamBoxHp>(GetObject((int)GameObjects.BaseInterface).transform);
            TeamBoxes.Add(box);
        }
    }
}
