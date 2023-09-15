using System;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // ?醫롫짗??용쐻??덉굲?醫롫짗??
    protected UnitStat unitStat;                    // ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲?醫롫짗??(?醫롫짗??용쐻??덉굲?醫롫짗??
    protected CurrentUnitStat currentUnitStat;      // ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲?醫롫짗??(?醫롫짗??용쐻?諛댁빽)
    protected Vector3 targetPos;                    // ?醫롫뼓?紐꾩굲?醫롫짗???醫롫짗??덊뒄

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

    #region ?醫롫짗??용쐻??덉굲?醫롫짗??됰뼒

    /// <summary>
    /// ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲?醫롫짗??됰뼒
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;

            //testText.text = currentState.ToString();
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

    #region ?醫롫짗??용쐻?

    private const float ROTATE_SPEED = 20f;     // ?醫롫짗??용쐻??덉굲 ???쐻??덉굲?醫롫셽?紐꾩굲
    private const float RAY_DISTANCE = 100f;     // ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲?醫롫뻿筌뤿슣??

    #endregion

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ?醫롫짗??용쐻??덉굲 ?醫롫뼏繹먮씮????醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230911
    /// </summary>
    public virtual void Init()
    {
        // Data
        currentUnitStat = new CurrentUnitStat(unitStat);
        currentUnitStat.OnHeal(unitStat.Hp);

        // UI
        unitHUD = Managers.UI.MakeWordSpaceUI<UI_UnitHUD>(transform);       // ?醫롫짗??용쐻??덉굲 HUD ?醫롫짗??용쐻??덉굲
    }

    /// <summary>
    /// InputManager ??룸쐻??덉굲?醫롫짗??용쐻??덉굲 OnUpdate ?醫롫셾??뚯굲?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??삳솇???醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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

    #region ?醫롫짗??용쐻?諛명닰???醫롫짗??용쐻??덉굲?醫롫짗??됰뱜 ?醫롫셾??뚯굲

    /// <summary>
    /// ?醫롫짗??용쐻?諛대???IDLE?醫롫짗???醫롫짗???醫롫짗??용쐻??덉굲?遺룸쐻??醫롫짗??용쐻??덉굲?醫롫짗??됰뱜 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: ?醫롫짗??용쐻??덉굲
    }

    /// <summary>
    /// ?醫롫짗??용쐻?諛대???MOVE?醫롫짗???醫롫짗???醫롫짗??용쐻??덉굲?遺룸쐻??醫롫짗??용쐻??덉굲?醫롫짗??됰뱜 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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
    /// ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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
    /// ?醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲?醫롫짗??筌ｋ똾寃??醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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

    #region ?醫롫짗??됯텢 ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲

    /// <summary>
    /// ???醫롫셾?關????醫롫짗??용쐻??덉굲 ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 ?醫롫짗??용쐻??덉굲?醫롫뼣?癒?굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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
    /// ?醫롫뼦??뺥닰???醫롫짗??됯텢 ?醫롫짗??용쐻??덉굲?醫롫뼣?癒?굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// ???醫롫셾?關????醫롫짗??용쐻??덉굲 ?醫롫짗??용쐻??덉굲 ?醫롫짗??됯텢 ?醫롫짗??용쐻??덉굲?醫롫뼣?癒?굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
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

    #region ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 Q ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q ?醫롫짗??됯텢 ?醫롫솯?紐꾩굲");
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 W ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W ?醫롫짗??됯텢 ?醫롫솯?紐꾩굲");
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 E ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E ?醫롫짗??됯텢 ?醫롫솯?紐꾩굲");
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 R ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R ?醫롫짗??됯텢 ?醫롫솯?紐꾩굲");
    }

    #endregion

    #region ?醫롫뼦??뺥닰???醫롫짗??됯텢

    protected virtual void CastPassive()
    {
        Debug.Log("?醫롫뼦??뺥닰???醫롫짗??됯텢");
    }

    #endregion

    #region ?醫롫짗??용쐻??덉굲 ?醫롫짗??됯텢

    protected virtual void CastSpellD()
    {
        Debug.Log("D ?醫롫짗??용쐻??덉굲 ?醫롫솯?紐꾩굲");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F ?醫롫짗??용쐻??덉굲 ?醫롫솯?紐꾩굲");
    }

    #endregion

    #endregion
}
