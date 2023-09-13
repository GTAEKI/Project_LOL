using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // ?μ’λ£??Ώλ??μ?μ’λ£??
    protected UnitStat unitStat;                    // ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ?μ’λ£??(?μ’λ£??Ώλ??μ?μ’λ£??
    protected CurrentUnitStat currentUnitStat;      // ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ?μ’λ£??(?μ’λ£??Ώλ?λ°΄μ»ͺ)
    protected Vector3 targetPos;                    // ?μ’λ?λͺμ?μ’λ£???μ’λ£??ν
    
    [Header("Game Team Type")]
    [SerializeField] protected Define.GameTeam unitTeam;             // Unit's Team Type

    public Define.GameTeam UnitTeam
    {
        get => unitTeam;
        set
        {
            unitTeam = value;

            switch(unitTeam)
            {
                case Define.GameTeam.BLUE: break;
                case Define.GameTeam.RED: break;
            }
        }
    }

    // UI
    protected UI_UnitHUD unitHUD;       // Unit's HUD

    [Header("Player Current State")]
    [SerializeField] protected Define.UnitState currentState = Define.UnitState.IDLE;

    #region ?μ’λ£??Ώλ??μ?μ’λ£??λ

    /// <summary>
    /// ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ?μ’λ£??λ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            
            // TODO: ?μ’λ£??Ώλ?λ±????μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??λ²???μ’λ??μ±Ώ??Ώλ?κΉλ¦???μ’λ£??Ώλ??μ
        }
    }

    public CurrentUnitStat CurrentUnitStat
    {
        get => currentUnitStat;
        set
        {
            currentUnitStat = value;
        }
    }

    #endregion

    #region ?μ’λ£??Ώλ?

    private const float ROTATE_SPEED = 20f;     // ?μ’λ£??Ώλ??μ ?????μ?μ’λ?λͺμ
    private const float RAY_DISTANCE = 100f;     // ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ?μ’λο§λ―??

    #endregion

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ?μ’λ£??Ώλ??μ ?μ’λζΊλΏ????μ’λ??μ
    /// ?μ’λ£??λΏ??230911
    /// </summary>
    public virtual void Init()
    {
        // Data
        currentUnitStat = new CurrentUnitStat(unitStat);
        currentUnitStat.OnHeal(unitStat.Hp);

        // UI
        unitHUD = Managers.UI.MakeWordSpaceUI<UI_UnitHUD>(transform);       // ?μ’λ£??Ώλ??μ HUD ?μ’λ£??Ώλ??μ
    }

    /// <summary>
    /// InputManager ??·λ??μ?μ’λ£??Ώλ??μ OnUpdate ?μ’λ??μ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??»λ???μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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

    #region ?μ’λ£??Ώλ?λ°Έν¦???μ’λ£??Ώλ??μ?μ’λ£??λ ?μ’λ??μ

    /// <summary>
    /// ?μ’λ£??Ώλ?λ°΄λ???IDLE?μ’λ£???μ’λ£???μ’λ£??Ώλ??μ?λΆ·λ??μ’λ£??Ώλ??μ?μ’λ£??λ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: ?μ’λ£??Ώλ??μ
    }

    /// <summary>
    /// ?μ’λ£??Ώλ?λ°΄λ???MOVE?μ’λ£???μ’λ£???μ’λ£??Ώλ??μ?λΆ·λ??μ’λ£??Ώλ??μ?μ’λ£??λ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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
    /// ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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
    /// ?μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ?μ’λ£??ο§£λκ²??μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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

    #region ?μ’λ£??κΆ ?μ’λ£??Ώλ??μ ?μ’λ??μ

    /// <summary>
    /// ???μ’λ?ΞΌ????μ’λ£??Ώλ??μ ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ ?μ’λ£??Ώλ??μ?μ’λ¦?λ¨? ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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
    /// ?μ’λ©??ν¦???μ’λ£??κΆ ?μ’λ£??Ώλ??μ?μ’λ¦?λ¨? ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// ???μ’λ?ΞΌ????μ’λ£??Ώλ??μ ?μ’λ£??Ώλ??μ ?μ’λ£??κΆ ?μ’λ£??Ώλ??μ?μ’λ¦?λ¨? ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
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

    #region ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ

    /// <summary>
    /// ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ Q ?μ’λ£??Ώλ??μ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q ?μ’λ£??κΆ ?μ’λ₯?λͺμ");
    }

    /// <summary>
    /// ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ W ?μ’λ£??Ώλ??μ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W ?μ’λ£??κΆ ?μ’λ₯?λͺμ");
    }

    /// <summary>
    /// ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ E ?μ’λ£??Ώλ??μ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E ?μ’λ£??κΆ ?μ’λ₯?λͺμ");
    }

    /// <summary>
    /// ?μ’λ£??λ?μ’λ£???μ’λ£??κΆ R ?μ’λ£??Ώλ??μ ?μ’λ??μ
    /// ?μ’λ£??λΏ??230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R ?μ’λ£??κΆ ?μ’λ₯?λͺμ");
    }

    #endregion

    #region ?μ’λ©??ν¦???μ’λ£??κΆ

    protected virtual void CastPassive()
    {
        Debug.Log("?μ’λ©??ν¦???μ’λ£??κΆ");
    }

    #endregion

    #region ?μ’λ£??Ώλ??μ ?μ’λ£??κΆ

    protected virtual void CastSpellD()
    {
        Debug.Log("D ?μ’λ£??Ώλ??μ ?μ’λ₯?λͺμ");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F ?μ’λ£??Ώλ??μ ?μ’λ₯?λͺμ");
    }

    #endregion

    #endregion
}
