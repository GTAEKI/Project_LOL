using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    private GameObject playerSubject;      // 관찰 주체
    private Unit playerUnit;               // 플레이어 (본인)

    /// <summary>
    /// Managers 클래스의 Start 함수에서 동작하는 함수
    /// 김민섭_230906
    /// </summary>
    public void Init()
    {
        playerSubject = GameObject.FindGameObjectWithTag("Player");
        playerUnit = playerSubject.GetOrAddComponent<Unit>();
    }

    /// <summary>
    /// Managers 클래스의 Update 함수에서 동작하는 함수
    /// 김민섭_230906
    /// </summary>
    public void OnUpdate()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return;      // UI 터치 방지

        playerUnit?.OnUpdate();
    }

    /// <summary>
    /// Managers 클래스의 Clear 함수에서 동작하는 함수
    /// 김민섭_230906
    /// </summary>
    public void Clear()
    {

    }
}
