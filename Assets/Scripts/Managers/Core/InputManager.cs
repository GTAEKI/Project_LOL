using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    private GameObject playerSubject;      // ���� ��ü
    private Unit playerUnit;               // �÷��̾� (����)

    /// <summary>
    /// Managers Ŭ������ Start �Լ����� �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void Init()
    {
        playerSubject = GameObject.FindGameObjectWithTag("Player");
        playerUnit = playerSubject.GetOrAddComponent<Unit>();
    }

    /// <summary>
    /// Managers Ŭ������ Update �Լ����� �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;      // UI ��ġ ����

        playerUnit?.OnUpdate();

        if(Input.GetKeyDown(KeyCode.A))
        {
            Managers.UI.ShowPopupUI<UI_DummyController>();
        }
    }

    /// <summary>
    /// ���콺 Ŭ�� �̺�Ʈ �Է� ���� üũ �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <param name="evt">�̺�Ʈ ��ȣ</param>
    public bool CheckKeyEvent(int evt)
    {
        if (Input.GetMouseButtonDown(evt))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Managers Ŭ������ Clear �Լ����� �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void Clear()
    {

    }
}
