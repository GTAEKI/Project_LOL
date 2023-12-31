using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject _target) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_target);
    }

    public static void AddUIEvent(this GameObject _target, Action<PointerEventData> _action, Define.UIEvent _type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(_target, _action, _type);
    }
}
