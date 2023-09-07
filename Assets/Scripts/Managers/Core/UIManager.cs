 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    private Camera uiCamera;        // UI Ä«¸Þ¶ó
    
    private Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    private Dictionary<string, UI_Scene> sceneDict = new Dictionary<string, UI_Scene>();
    
    private int order = 10;      // ÇöÀç Äµ¹ö½ºÀÇ ¿À´õ

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

    public void Init()
    {
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }

    public T GetScene<T>() where T : UI_Scene
    {
        if (sceneDict.Count <= 0) return null;
        if (sceneDict.ContainsKey(typeof(T).Name)) return sceneDict[typeof(T).Name].GetComponent<T>();
        return null;
    }

    public void SetCanvas(GameObject _target, bool _sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(_target);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = uiCamera;
        canvas.overrideSorting = true;

        if (_sort)
        {   // TODO: ÆË¾÷ UI´Â order Áõ°¡
            canvas.sortingOrder = order;
            order++;
        }
        else
        {   // TODO: ÀÏ¹Ý UI´Â order 0
            canvas.sortingOrder = 0;
        }
    }

    #region Scene °ü¸®
    /// <summary>
    /// ¾À UI¸¦ È£ÃâÇÏ´Â ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    /// <typeparam name="T">½ºÅ©¸³Æ® ÀÌ¸§</typeparam>
    /// <param name="_name">ÇÁ¸®ÆÕ ÀÌ¸§</param>
    public T ShowSceneUI<T>(string _name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/Scene/{_name}");
        T sceneUI = Util.GetOrAddComponent<T>(prefab);
        sceneDict.Add(_name, sceneUI);

        prefab.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public void CloseAllSceneUI()
    {
        if (sceneDict.Count <= 0) return;
        sceneDict.Clear();
    }
    #endregion

    #region SubItem °ü¸®

    /// <summary>
    /// ¼­ºê¾ÆÀÌÅÛ »ý¼º ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    /// <typeparam name="T">½ºÅ©¸³Æ® ÀÌ¸§</typeparam>
    /// <param name="_parent">»ý¼ºµÉ À§Ä¡ (ºÎ¸ð)</param>
    /// <param name="_name">ÇÁ¸®ÆÕ ÀÌ¸§</param>
    public T MakeSubItem<T>(Transform _parent = null, string _name = null) where T: UI_Base
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/SubItem/{_name}");
        if(_parent != null)
        {
            prefab.transform.SetParent(_parent);
        }
        return prefab.GetOrAddComponent<T>();
    }
    #endregion

    #region WordSpace °ü¸®
    public T MakeWordSpaceUI<T>(Transform _parent = null, string _name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/WorldSpace/{_name}");
        if (_parent != null)
        {
            prefab.transform.SetParent(_parent);
        }

        Canvas canvas = prefab.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return prefab.GetOrAddComponent<T>();
    }
    #endregion

    #region Popup °ü¸®

    public T GetPopupUI<T>(string name = null) where T: UI_Popup
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        List<UI_Popup> popList = popupStack.ToList();
        if(popList.Count > 0)
        {
            return popList.Find(x => x.name == name) as T;
        }
        return null;
    }

    /// <summary>
    /// ÆË¾÷ UI¸¦ È£ÃâÇÏ´Â ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    /// <typeparam name="T">½ºÅ©¸³Æ® ÀÌ¸§</typeparam>
    /// <param name="_name">ÇÁ¸®ÆÕ ÀÌ¸§</param>
    public T ShowPopupUI<T>(string _name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(_name)) _name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Instantiate($"UI/Popup/{_name}");
        T popup = Util.GetOrAddComponent<T>(prefab);
        popupStack.Push(popup);

        prefab.transform.SetParent(Root.transform);

        return popup;
    }

    /// <summary>
    /// ´Ý´Â ÆË¾÷ UI°¡ ¸Â´ÂÁö ºñ±³ ÈÄ ´Ý´Â ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    /// <param name="_popup">´ÝÈ÷´Â ÆË¾÷</param>
    public void ClosePopupUI(UI_Popup _popup)
    {
        if (popupStack.Count == 0) return;

        if(popupStack.Peek() != _popup)
        {
            Debug.LogWarning("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }

    /// <summary>
    /// ÆË¾÷ UI¸¦ ´Ý´Â ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return;

        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);

        order--;
    }

    /// <summary>
    /// ¸ðµç ÆË¾÷ UI¸¦ ´Ý´Â ÇÔ¼ö
    /// ±è¹Î¼·_230906
    /// </summary>
    public void CloseAllPopupUI()
    {
        while(popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
    #endregion

    public void Clear()
    {
        CloseAllSceneUI();
        CloseAllPopupUI();
    }
}