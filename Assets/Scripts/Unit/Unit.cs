using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public delegate void CastActiveHandler();
    public CastActiveHandler castActiveHandler;

    private Vector3 targetPos;          // �̵��� ��ġ

    /// <summary>
    /// InputManager Ŭ������ OnUpdate �Լ����� ���Ǵ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void OnUpdate()
    {
        //CastPassiveSkill();

        //Move();
        UpdateMove(targetPos);

        //CastActiveSkill();
        //CastSpellSkill();
    }

    #region �̵� ���� �Լ�

    public void Move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Unit")))
            {
                Vector3 rayStartPosition = Camera.main.transform.position;      // ���̸� �׷��� ���� ��ġ (ī�޶� ��ġ)
                Vector3 rayEndPosition = hit.point;         // ���̸� �׷��� �� ��ġ (�浹 ����)    
                float duration = 2f;        // ���̸� �׷��ֱ� ���� �ð� (�����Ӵ� ���̰� ������ �ð�)
                Debug.DrawRay(rayStartPosition, rayEndPosition - rayStartPosition, Color.blue, duration);        // Debug.DrawRay�� ����Ͽ� ���̸� �׸�

                Debug.Log("������ �����߽��ϴ�.");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Floor")))
            {
                Vector3 rayStartPosition = Camera.main.transform.position;      // ���̸� �׷��� ���� ��ġ (ī�޶� ��ġ)
                Vector3 rayEndPosition = hit.point;         // ���̸� �׷��� �� ��ġ (�浹 ����)    
                float duration = 2f;        // ���̸� �׷��ֱ� ���� �ð� (�����Ӵ� ���̰� ������ �ð�)
                Debug.DrawRay(rayStartPosition, rayEndPosition - rayStartPosition, Color.red, duration);        // Debug.DrawRay�� ����Ͽ� ���̸� �׸�

                targetPos = rayEndPosition;
            }
        }
    }

    public void UpdateMove(Vector3 targetPos)
    {
        if (targetPos == default) return;

        Vector3 direct = targetPos - transform.position;
        direct.y = 0f;

        float minDistance = direct.magnitude;
        if(minDistance <= 1f)
        {
            targetPos = default;
            Debug.Log("��ǥ�� ������");
            return;
        }
        else
        {
            float moveDistance = Mathf.Clamp(5f * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), 20 * Time.deltaTime);
        }    
    }

    #endregion

    #region ��ų ���� �Լ�

    /// <summary>
    /// Ű �Է¿� ���� ��Ƽ�� ��ų �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public virtual void CastActiveSkill()
    {
        castActiveHandler();

        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    CastActiveQ();
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    CastActiveW();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    CastActiveE();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    CastActiveR();
        //}
    }

    /// <summary>
    /// �нú� ��ų �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// Ű �Է¿� ���� ���� ��ų �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    /// <param name="key"></param>
    public virtual void CastSpellSkill()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CastSpellD();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CastSpellF();
        }
    }

    #region ��Ƽ�� ��ų

    /// <summary>
    /// ��Ƽ�� ��ų Q ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q ��ų �ߵ�");
    }

    /// <summary>
    /// ��Ƽ�� ��ų W ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W ��ų �ߵ�");
    }

    /// <summary>
    /// ��Ƽ�� ��ų E ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E ��ų �ߵ�");
    }

    /// <summary>
    /// ��Ƽ�� ��ų R ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R ��ų �ߵ�");
    }

    #endregion

    #region �нú� ��ų

    protected virtual void CastPassive()
    {
        Debug.Log("�нú� ��ų");
    }

    #endregion

    #region ���� ��ų

    protected virtual void CastSpellD()
    {
        Debug.Log("D ���� �ߵ�");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F ���� �ߵ�");
    }

    #endregion

    #endregion
}
