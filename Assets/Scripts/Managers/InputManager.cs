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
        //if (EventSystem.current.IsPointerOverGameObject()) return;      // UI ��ġ ����

        playerUnit?.OnUpdate();
    }

    /// <summary>
    /// Managers Ŭ������ Clear �Լ����� �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void Clear()
    {

    }
}
