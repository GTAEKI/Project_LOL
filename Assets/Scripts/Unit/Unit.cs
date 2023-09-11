using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected UnitStat unitStat;     // ���� ���� ������ (������)
    protected Vector3 targetPos;          // �̵��� ��ġ

    [Header("���� ���� ����")]
    [SerializeField] protected Define.UnitState currentState = Define.UnitState.IDLE;

    #region ������Ƽ

    /// <summary>
    /// ���� ���� ���� ������Ƽ
    /// ��μ�_230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            
            // TODO: ���¿� ���� ���õ� �ִϸ��̼� ����
        }
    }

    #endregion

    #region ���

    private const float ROTATE_SPEED = 20f;     // ���� ȸ���ӵ�
    private const float RAY_DISTANCE = 100f;     // ���� �����Ÿ�

    #endregion

    private void Start()
    {
        Init();
    }

    public abstract void Init();

    /// <summary>
    /// InputManager Ŭ������ OnUpdate �Լ����� ���Ǵ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public void OnUpdate()
    {
        Move();
        Select();
    }

    public void Update()
    {
        switch (CurrentState)
        {
            case Define.UnitState.IDLE: UpdateIdle(); break;
            case Define.UnitState.MOVE: UpdateMove(); break;
        }
    }

    #region ���º� ������Ʈ �Լ�

    /// <summary>
    /// ���°� IDLE�� �� ����Ǵ� ������Ʈ �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: ����
    }

    /// <summary>
    /// ���°� MOVE�� �� ����Ǵ� ������Ʈ �Լ�
    /// ��μ�_230906
    /// </summary>
    protected virtual void UpdateMove()
    {
        if (targetPos == default) return;

        Vector3 direct = targetPos - transform.position;
        direct.y = 0f;

        float minDistance = direct.magnitude;
        if (minDistance <= 1f)
        {
            targetPos = default;
            CurrentState = Define.UnitState.IDLE;
            return;
        }
        else
        {
            float moveDistance = Mathf.Clamp(unitStat.MoveMentSpeed * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), ROTATE_SPEED * Time.deltaTime);
        }
    }

    #endregion

    /// <summary>
    /// ���� ���� �Լ�
    /// ��μ�_230906
    /// </summary>
    private void Select()
    {
        if(Managers.Input.CheckKeyEvent(0))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy == null || (ui_dummy != null && !ui_dummy.IsCreate))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit")))
                {
                    Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);
                }
            }
        }
    }

    /// <summary>
    /// ���� ������ üũ �Լ�
    /// ��μ�_230906
    /// </summary>
    public void Move()
    {
        if (Managers.Input.CheckKeyEvent(1))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy == null || (ui_dummy != null && !ui_dummy.IsCreate))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
                {
                    Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                    targetPos = hit.point;
                    CurrentState = Define.UnitState.MOVE;
                }
            }
        }
    }

    #region ��ų ���� �Լ�

    /// <summary>
    /// Ű �Է¿� ���� ��Ƽ�� ��ų �����ϴ� �Լ�
    /// ��μ�_230906
    /// </summary>
    public virtual void CastActiveSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CastActiveQ();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CastActiveW();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastActiveE();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CastActiveR();
        }
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
