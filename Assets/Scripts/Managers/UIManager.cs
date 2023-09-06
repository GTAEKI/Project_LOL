using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    private UI_Scene sceneUI = null;

    private int order = 10;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject _target, bool _sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(_target);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(_sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T> (Transform _parent = null, string _name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/SubItem/{_name}");

        if(_parent != null)
        {
            prefab.transform.SetParent(_parent);
        }

        return prefab.GetOrAddComponent<T>();
    }

    public T ShowSceneUI<T>(string _name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/Scene/{_name}");
        T scene = Util.GetOrAddComponent<T>(prefab);
        sceneUI = scene;
        prefab.transform.SetParent(Root.transform);

        return scene;
    }

    public T ShowPopupUI<T>(string _name = null) where T: UI_Popup
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/Popup/{_name}");
        T popup = Util.GetOrAddComponent<T>(prefab);
        popupStack.Push(popup);
        prefab.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup _popup)
    {
        if (popupStack.Count == 0) return;

        if(popupStack.Peek() != _popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return;

        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0) ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        sceneUI = null;
    }
}
