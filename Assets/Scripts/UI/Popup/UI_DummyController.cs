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
    /// 해당 팝업이 생성될 때, 초기화 하는 함수
    /// 김민섭_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

        // UI 세팅
        Bind<Button>(typeof(Buttons));      // Button 타입의 UI들을 바인딩

        // 버튼 이벤트 세팅
        BindEvent(GetButton((int)Buttons.Btn_CreateDummy).gameObject, OnCreateDummy);       // 더미 생성 버튼에 이벤트 부여
        BindEvent(GetButton((int)Buttons.Btn_RemoveDummy).gameObject, OnRemoveDummy);       // 더미 제거 버튼에 이벤트 부여
    }

    /// <summary>
    /// 더미 생성 활성화 처리 버튼 이벤트 함수
    /// 김민섭_230907
    /// </summary>
    /// <param name="data">클릭 이벤트 데이터</param>
    private void OnCreateDummy(PointerEventData data)
    {
        Debug.Log("더미 생성 활성화!");
    }

    /// <summary>
    /// 더미 제거 활성화 처리 버튼 이벤트 함수
    /// 김민섭_230907
    /// </summary>
    /// <param name="data">클릭 이벤트 데이터</param>
    private void OnRemoveDummy(PointerEventData data)
    {
        Debug.Log("더미 생성 비활성화!");
    }
}
