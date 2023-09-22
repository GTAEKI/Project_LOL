 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    private Camera uiCamera;        // UI ī�޶�
    
    private Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    private Dictionary<string, UI_Scene> sceneDict = new Dictionary<string, UI_Scene>();
    
    private int order = 10;      // ���� ĵ������ ����

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
        {   // TODO: �˾� UI�� order ����
            canvas.sortingOrder = order;
            order++;
        }
        else
        {   // TODO: �Ϲ� UI�� order 0
            canvas.sortingOrder = 0;
        }
    }

    #region Scene ����
    /// <summary>
    /// �� UI�� ȣ���ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <typeparam name="T">��ũ��Ʈ �̸�</typeparam>
    /// <param name="_name">������ �̸�</param>
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

    #region SubItem ����

    /// <summary>
    /// ��������� ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <typeparam name="T">��ũ��Ʈ �̸�</typeparam>
    /// <param name="_parent">������ ��ġ (�θ�)</param>
    /// <param name="_name">������ �̸�</param>
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

    #region WordSpace ����
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

    #region Popup ����

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
    /// �˾� UI�� ȣ���ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <typeparam name="T">��ũ��Ʈ �̸�</typeparam>
    /// <param name="_name">������ �̸�</param>
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
    /// �ݴ� �˾� UI�� �´��� �� �� �ݴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <param name="_popup">������ �˾�</param>
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
    /// �˾� UI�� �ݴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return;

        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);

        order--;
    }

    /// <summary>
    /// ��� �˾� UI�� �ݴ� �Լ�
    /// ��μ�_230906
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