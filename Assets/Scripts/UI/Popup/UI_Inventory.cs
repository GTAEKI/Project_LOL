using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    private enum GameObjects
    {
        GridPanel
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        // 인벤토리 정보 로드
        for(int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inventory_Item>(gridPanel.transform).gameObject;
            UI_Inventory_Item invenItem = item.GetOrAddComponent<UI_Inventory_Item>();
            invenItem.SetInfo($"집행검 +{i}");
        }
    }
}
