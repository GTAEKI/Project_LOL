using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    private class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        private Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject _original, int _count = 5)
        {
            Original = _original;
            Root = new GameObject().transform;
            Root.name = $"{Original.name}_Root";

            for(int i = 0; i < _count; i++)
            {
                Push(Create());
            }
        }

        private Poolable Create()
        {
            GameObject clone = Object.Instantiate(Original);
            clone.name = Original.name;
            return clone.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable _poolable)
        {
            if (_poolable == null) return;

            _poolable.transform.parent = Root;
            _poolable.gameObject.SetActive(false);
            _poolable.isUsing = false;

            poolStack.Push(_poolable);
        }

        public Poolable Pop(Transform _parent)
        {
            Poolable poolable = null;

            if (poolStack.Count > 0) poolable = poolStack.Pop();
            else poolable = Create();

            poolable.gameObject.SetActive(true);

            if(_parent == null)
            {   // DontDestroyOnLoad 해제 용도
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            }

            poolable.transform.parent = _parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

    private Dictionary<string, Pool> poolDict= new Dictionary<string, Pool>();
    private Transform root;

    public void Init()
    {
        if(root == null)
        {
            root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }
    }

    public void CreatePool(GameObject _original, int _count = 5)
    {
        Pool pool = new Pool();
        pool.Init(_original, _count);
        pool.Root.parent = root.transform;

        poolDict.Add(_original.name, pool);
    }

    public void Push(Poolable _poolable)
    {
        string name = _poolable.gameObject.name;
        if(!poolDict.ContainsKey(name))
        {
            Object.Destroy(_poolable.gameObject);
            return;
        }

        poolDict[name].Push(_poolable);
    }

    public Poolable Pop(GameObject _original, Transform _parent = null)
    {
        if(!poolDict.ContainsKey(_original.name))
        {
            CreatePool(_original);
        }

        return poolDict[_original.name].Pop(_parent);
    }

    public GameObject GetOriginal(string _name)
    {
        if (!poolDict.ContainsKey(_name)) return null;
        return poolDict[_name].Original;
    }

    public void Clear()
    {
        foreach(Transform child in root)
        {
            Object.Destroy(child.gameObject);
        }
        poolDict.Clear();
    }
}
