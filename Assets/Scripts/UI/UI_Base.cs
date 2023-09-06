using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> objectDict = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Bind<T>(Type _type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(_type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        objectDict.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null) Debug.LogError($"Failed to bind : {names[i]}");
        }
    }

    protected T Get<T>(int _index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (!objectDict.TryGetValue(typeof(T), out objects)) return null;
        return objects[_index] as T;
    }

    protected GameObject GetObject(int _index) => Get<GameObject>(_index);
    protected Text GetText(int _index) => Get<Text>(_index);
    protected Button GetButton(int _index) => Get<Button>(_index);
    protected Image GetImage(int _index) => Get<Image>(_index);

    public static void BindEvent(GameObject _target, Action<PointerEventData> _action, Define.UIEvent _type = Define.UIEvent.Click)
    {
        UI_EventHandler handler = Util.GetOrAddComponent<UI_EventHandler>(_target);

        switch(_type)
        {
            case Define.UIEvent.Click:
                {
                    handler.OnClickHandler -= _action;
                    handler.OnClickHandler += _action;
                }
                break;
            case Define.UIEvent.Drag:
                {
                    handler.OnDragHandler -= _action;
                    handler.OnDragHandler += _action;
                }
                break;
        }
    }
}
