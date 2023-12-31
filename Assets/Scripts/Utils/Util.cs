using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject _target) where T : UnityEngine.Component
    {
        T component = _target.GetComponent<T>();
        if(component == null)
        {
            component = _target.AddComponent<T>();
        }
        return component;
    }

    public static GameObject FindChild(GameObject _root, string _targetName = null, bool _recursive = false)
    {
        Transform transform = FindChild<Transform>(_root, _targetName, _recursive);
        if (transform == null) return null;
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject _root, string _targetName = null, bool _recursive = false) where T : UnityEngine.Object
    {
        if (_root == null) return null;

        if(!_recursive)
        {
            for(int i = 0; i < _root.transform.childCount; i++)
            {
                Transform transform = _root.transform.GetChild(i);
                if(string.IsNullOrEmpty(_targetName) || transform.name == _targetName)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        else
        {
            foreach(T component in _root.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(_targetName) || component.name == _targetName) return component;
            }
        }
        return null;
    }

    /// <summary>
    /// 터치 지점으로 레이를 쏘는 함수
    /// 김민섭_230906
    /// </summary>
    /// <param name="startPoint">시작 지점</param>
    /// <param name="endPoint">끝 지점</param>
    /// <param name="rayColor">레이 색상</param>
    /// <param name="duration">지속 시간</param>
    public static void DrawTouchRay(Vector3 startPoint, Vector3 endPoint, Color rayColor, float duration = 1f)
    {
        Debug.DrawRay(startPoint, endPoint - startPoint, rayColor, duration);
    }
}
