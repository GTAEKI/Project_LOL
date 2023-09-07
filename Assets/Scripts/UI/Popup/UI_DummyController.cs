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

    private GameObject illusionDummy;               // ��ġ ��ġ ���� ����

    public bool IsCreate { private set; get; }      // ���� Ȱ��ȭ üũ

    private const float RAY_DISTANCE = 100f;     // ���� �����Ÿ�

    /// <summary>
    /// �ش� �˾��� ������ ��, �ʱ�ȭ �ϴ� �Լ�
    /// ��μ�_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

        illusionDummy = Managers.Resource.Instantiate("Unit/Dummy_Illusion");
        illusionDummy.SetActive(false);
        IsCreate = false;

        // UI ����
        Bind<Button>(typeof(Buttons));      // Button Ÿ���� UI���� ���ε�

        // ��ư �̺�Ʈ ����
        BindEvent(GetButton((int)Buttons.Btn_CreateDummy).gameObject, OnCreateDummy);       // ���� ���� ��ư �̺�Ʈ �ο�
        BindEvent(GetButton((int)Buttons.Btn_RemoveDummy).gameObject, OnRemoveDummy);       // ���� ���� ��ư �̺�Ʈ �ο�
        BindEvent(GetButton((int)Buttons.Btn_Exit).gameObject, OnExit);                     // �˾� ������ ��ư �̺�Ʈ �ο�
    }

    #region ��ư �̺�Ʈ

    /// <summary>
    /// ���� ���� Ȱ��ȭ ó�� ��ư �̺�Ʈ �Լ�
    /// ��μ�_230907
    /// </summary>
    /// <param name="data">Ŭ�� �̺�Ʈ ������</param>
    private void OnCreateDummy(PointerEventData data)
    {
        Debug.Log("���� ���� Ȱ��ȭ!");
        IsCreate = true;
        illusionDummy?.SetActive(true);
    }

    /// <summary>
    /// ���� ���� Ȱ��ȭ ó�� ��ư �̺�Ʈ �Լ�
    /// ��μ�_230907
    /// </summary>
    /// <param name="data">Ŭ�� �̺�Ʈ ������</param>
    private void OnRemoveDummy(PointerEventData data)
    {
        Debug.Log("���� ���� ��Ȱ��ȭ!");
        IsCreate = false;
        illusionDummy?.SetActive(false);
    }

    /// <summary>
    /// �˾� ������ ��ư �̺�Ʈ �Լ�
    /// ��μ�_230907
    /// </summary>
    /// <param name="data"></param>
    private void OnExit(PointerEventData data) => ClosePopupUI();

    #endregion

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;      // UI ��ġ ����

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
            {   // ���� ����
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
                {
                    GameObject dummy = Managers.Resource.Instantiate("Unit/Dummy", hit.point, Quaternion.identity);
                    dummy.transform.position = new Vector3(dummy.transform.position.x, dummy.transform.localScale.y, dummy.transform.position.z);
                }
            }
            else
            {   // ���� ����
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
