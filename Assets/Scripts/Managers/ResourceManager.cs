using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string _filePath) where T: Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = _filePath;
            int index = name.LastIndexOf('/');
            if (index >= 0) name = name.Substring(index + 1);

            GameObject clone = Managers.Pool.GetOriginal(name);
            if (clone != null) return clone as T;
        }
        return Resources.Load<T>(_filePath);
    }

    public GameObject Instantiate(string _filePath, Transform _parent = null)
    {
        GameObject original = Load<GameObject>($"Prefab/{_filePath}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {_filePath}");
            return null;
        }

        if(original.GetComponent<Poolable>() != null) return Managers.Pool.Pop(original, _parent).gameObject;

        GameObject clone = Object.Instantiate(original, _parent);
        clone.name = original.name;
        return clone;
    }

    public void Destroy(GameObject _target)
    {
        if (_target == null) return;

        Poolable poolable = _target.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        Object.Destroy(_target);
    }
}
