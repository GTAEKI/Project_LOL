using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private GameObject create_dummy_illusion;               // ?ㅼ튂 ?꾩튂 ?앹꽦 ?붾?
    private Dummy_Illusion dummy_illusion;          

    public bool IsCreate { private set; get; }      // ?앹꽦 ?쒖꽦??泥댄겕

    private const float RAY_DISTANCE = 100f;     // ?덉씠 ?ъ젙嫄곕━

    /// <summary>
    /// ?대떦 ?앹뾽???앹꽦???? 珥덇린???섎뒗 ?⑥닔
    /// 源誘쇱꽠_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

        create_dummy_illusion = Managers.Resource.Instantiate("Unit/Dummy_Illusion");
        dummy_illusion = create_dummy_illusion.GetComponent<Dummy_Illusion>();
        IsCreate = false;

        create_dummy_illusion.SetActive(false);

        // UI ?명똿
        Bind<Button>(typeof(Buttons));                    // Button ??낆쓽 UI?ㅼ쓣 諛붿씤??

        // 踰꾪듉 ?대깽???명똿
        BindEvent(GetButton((int)Buttons.Btn_CreateDummy).gameObject, OnCreateDummy);       // ?붾? ?앹꽦 踰꾪듉 ?대깽??遺??
        BindEvent(GetButton((int)Buttons.Btn_RemoveDummy).gameObject, OnRemoveDummy);       // ?붾? ?쒓굅 踰꾪듉 ?대깽??遺??
        BindEvent(GetButton((int)Buttons.Btn_Exit).gameObject, OnExit);                     // ?앹뾽 ?섍?湲?踰꾪듉 ?대깽??遺??
    }

    #region 踰꾪듉 ?대깽??

    /// <summary>
    /// ?붾? ?앹꽦 ?쒖꽦??泥섎━ 踰꾪듉 ?대깽???⑥닔
    /// 源誘쇱꽠_230907
    /// </summary>
    /// <param name="data">?대┃ ?대깽???곗씠??/param>
    private void OnCreateDummy(PointerEventData data)
    {
        Debug.Log("?붾? ?앹꽦 ?쒖꽦??");
        IsCreate = true;
        create_dummy_illusion?.SetActive(true);
    }

    /// <summary>
    /// ?붾? ?쒓굅 ?쒖꽦??泥섎━ 踰꾪듉 ?대깽???⑥닔
    /// 源誘쇱꽠_230907
    /// </summary>
    /// <param name="data">?대┃ ?대깽???곗씠??/param>
    private void OnRemoveDummy(PointerEventData data)
    {
        Debug.Log("?붾? ?앹꽦 鍮꾪솢?깊솕!");
        IsCreate = false;
        create_dummy_illusion?.SetActive(false);
    }

    /// <summary>
    /// ?앹뾽 ?섍?湲?踰꾪듉 ?대깽???⑥닔
    /// 源誘쇱꽠_230907
    /// </summary>
    /// <param name="data"></param>
    private void OnExit(PointerEventData data) => ClosePopupUI();

    #endregion

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;      // UI ?곗튂 諛⑹?

        if(IsCreate && create_dummy_illusion != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor", "Bush")))
            {
                Vector3 _location = hit.point;
                create_dummy_illusion.transform.position = _location;
                create_dummy_illusion.transform.position = new Vector3(create_dummy_illusion.transform.position.x, create_dummy_illusion.transform.localScale.y, create_dummy_illusion.transform.position.z);
            }
        }

        if (dummy_illusion != null && dummy_illusion.IsPossible && Managers.Input.CheckKeyEvent(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(IsCreate)
            {   // ?붾? ?앹꽦
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor", "Bush")))
                {
                    GameObject dummy = Managers.Resource.Instantiate("Unit/Dummy", hit.point, Quaternion.identity);
                    dummy.transform.position = new Vector3(dummy.transform.position.x, dummy.transform.localScale.y, dummy.transform.position.z);
                }
            }
            else
            {   // ?붾? ?쒓굅
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
