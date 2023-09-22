using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> objDict = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// UI ������Ʈ�� ã�Ƽ� �迭�� �����ϴ� �Լ�
    /// </summary>
    /// <typeparam name="T">UI ������Ʈ Ÿ��</typeparam>
    /// <param name="_type">enum Ÿ��</param>
    protected void Bind<T>(Type _type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(_type); //Enum에 있는 녀석들 다 가져와서 배열로 만들수 있음 _ 기본적으로 제공하는 기능 
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length]; //유니티 Engine에 있는 Object와 System에 있는 Object구별을 위해
        objDict.Add(typeof(T), objs);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject)) //게임오브젝트는 컴포넌트 타입이 아니므로 다른방식으로 가져와야함
            {   // 
                objs[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {   // �� ���� Ÿ���� ���
                objs[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objs[i] == null)
            {
                Debug.LogWarning($"Failed to Bind. {names[i]}");
            }
        }
    }

    /// <summary>
    /// UI ������Ʈ�� ������ ���� �Լ�
    /// </summary>
    /// <typeparam name="T">UI ������Ʈ Ÿ��</typeparam>
    /// <param name="_index">UI ������Ʈ�� �ε���</param>
    /// <returns></returns>
    protected T Get<T>(int _index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (!objDict.TryGetValue(typeof(T), out objects)) return null;
        return objects[_index] as T; //objDict에 있는 값들이 objects로 배열로 들어감
    }

    /// <summary>
    /// �̺�Ʈ Ÿ�Կ� ���� UI ������Ʈ�� �̺�Ʈ�� �ο�
    /// </summary>
    /// <param name="_target">��ǥ ������Ʈ</param>
    /// <param name="_action">�̺�Ʈ</param>
    /// <param name="_type">�̺�Ʈ Ÿ��</param>
    public static void BindEvent(GameObject _target, Action<PointerEventData> _action, Define.UIEvent _type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(_target);

        switch (_type)
        {
            case Define.UIEvent.Click:
                {
                    evt.OnClickHandler -= _action;
                    evt.OnClickHandler += _action;
                }
                break;
        }
    }

    protected GameObject GetObject(int _index) => Get<GameObject>(_index);
    protected Text GetText(int _index) => Get<Text>(_index);
    protected TextMeshProUGUI GetTMP(int index) => Get<TextMeshProUGUI>(index);
    protected Image GetImage(int _index) => Get<Image>(_index);
    protected Button GetButton(int _index) => Get<Button>(_index);
}
