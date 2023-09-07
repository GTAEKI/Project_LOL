using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DummyController : UI_Popup
{
    private enum Buttons
    {
        Btn_CreateDummy,
        Btn_RemoveDummy,
        Btn_Exit
    }

    private GameObject illusionDummy;               // 설치 위치 생성 더미

    public bool IsCreate { private set; get; }      // 생성 활성화 체크

    private const float RAY_DISTANCE = 100f;     // 레이 사정거리

    /// <summary>
    /// 해당 팝업이 생성될 때, 초기화 하는 함수
    /// 김민섭_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

        illusionDummy = Managers.Resource.Instantiate("Unit/Dummy_Illusion");
        illusionDummy.SetActive(false);
        IsCreate = false;

        // UI 세팅
        Bind<Button>(typeof(Buttons));      // Button 타입의 UI들을 바인딩

        // 버튼 이벤트 세팅
        BindEvent(GetButton((int)Buttons.Btn_CreateDummy).gameObject, OnCreateDummy);       // 더미 생성 버튼 이벤트 부여
        BindEvent(GetButton((int)Buttons.Btn_RemoveDummy).gameObject, OnRemoveDummy);       // 더미 제거 버튼 이벤트 부여
        BindEvent(GetButton((int)Buttons.Btn_Exit).gameObject, OnExit);                     // 팝업 나가기 버튼 이벤트 부여
    }

    #region 버튼 이벤트

    /// <summary>
    /// 더미 생성 활성화 처리 버튼 이벤트 함수
    /// 김민섭_230907
    /// </summary>
    /// <param name="data">클릭 이벤트 데이터</param>
    private void OnCreateDummy(PointerEventData data)
    {
        Debug.Log("더미 생성 활성화!");
        IsCreate = true;
        illusionDummy?.SetActive(true);
    }

    /// <summary>
    /// 더미 제거 활성화 처리 버튼 이벤트 함수
    /// 김민섭_230907
    /// </summary>
    /// <param name="data">클릭 이벤트 데이터</param>
    private void OnRemoveDummy(PointerEventData data)
    {
        Debug.Log("더미 생성 비활성화!");
        IsCreate = false;
        illusionDummy?.SetActive(false);
    }

    /// <summary>
    /// 팝업 나가기 버튼 이벤트 함수
    /// 김민섭_230907
    /// </summary>
    /// <param name="data"></param>
    private void OnExit(PointerEventData data) => ClosePopupUI();

    #endregion

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;      // UI 터치 방지

        if(IsCreate && illusionDummy != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
            {
                Vector3 _location = hit.point;
                illusionDummy.transform.position = _location;
                illusionDummy.transform.position = new Vector3(illusionDummy.transform.position.x, illusionDummy.transform.localScale.y, illusionDummy.transform.position.z);
            }
        }

        if (Managers.Input.CheckKeyEvent(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(IsCreate)
            {   // 더미 생성
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
                {
                    GameObject dummy = Managers.Resource.Instantiate("Unit/Dummy", hit.point, Quaternion.identity);
                    dummy.transform.position = new Vector3(dummy.transform.position.x, dummy.transform.localScale.y, dummy.transform.position.z);
                }
            }
            else
            {   // 더미 제거
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit")))
                {
                    Dummy dummy = hit.transform.GetComponent<Dummy>();
                    if(dummy != null)
                    {
                        Managers.Resource.Destroy(dummy.gameObject); 
                    }
                }
            }
        }
    }
}
