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
        Btn_RemoveDummy,
        Btn_Exit
    }

    public bool IsCreate { private set; get; }      // ���� Ȱ��ȭ üũ

    private const float RAY_DISTANCE = 100f;     // ���� �����Ÿ�

    /// <summary>
    /// �ش� �˾��� ������ ��, �ʱ�ȭ �ϴ� �Լ�
    /// ��μ�_230907
    /// </summary>
    public override void Init()
    {
        base.Init();

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

        if (Managers.Input.CheckKeyEvent(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(IsCreate)
            {
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
                {
                    GameObject dummy = Managers.Resource.Instantiate("Unit/Dummy", hit.point, Quaternion.identity);
                    dummy.transform.position = new Vector3(dummy.transform.position.x, dummy.transform.localScale.y, dummy.transform.position.z);
                }
            }
            else
            {
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
