using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DummyController : UI_Popup
{
    private enum Buttons
    {
        Btn_CreateDummy,
        Btn_RemoveDummy
    }

    /// <summary>
    /// �ش� �˾��� ������ ��, �ʱ�ȭ �ϴ� �Լ�
    /// ��μ�_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

        // UI ����
        Bind<Button>(typeof(Buttons));      // Button Ÿ���� UI���� ���ε�

        // ��ư �̺�Ʈ ����
        BindEvent(GetButton((int)Buttons.Btn_CreateDummy).gameObject, OnCreateDummy);       // ���� ���� ��ư�� �̺�Ʈ �ο�
        BindEvent(GetButton((int)Buttons.Btn_RemoveDummy).gameObject, OnRemoveDummy);       // ���� ���� ��ư�� �̺�Ʈ �ο�
    }

    /// <summary>
    /// ���� ���� Ȱ��ȭ ó�� ��ư �̺�Ʈ �Լ�
    /// ��μ�_230907
    /// </summary>
    /// <param name="data">Ŭ�� �̺�Ʈ ������</param>
    private void OnCreateDummy(PointerEventData data)
    {
        Debug.Log("���� ���� Ȱ��ȭ!");
    }

    /// <summary>
    /// ���� ���� Ȱ��ȭ ó�� ��ư �̺�Ʈ �Լ�
    /// ��μ�_230907
    /// </summary>
    /// <param name="data">Ŭ�� �̺�Ʈ ������</param>
    private void OnRemoveDummy(PointerEventData data)
    {
        Debug.Log("���� ���� ��Ȱ��ȭ!");
    }
}
