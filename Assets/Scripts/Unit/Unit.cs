using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // 데이터
    protected UnitStat unitStat;                    // 유닛 기본 베이스 스탯
    protected CurrentUnitStat currentUnitStat;      // 유닛 현재 스탯
    protected UnitSkill unitSkill;                  // 유닛 스킬 정보
    protected Vector3 targetPos;                    // 목표 위치

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

    #region 

    /// <summary>
    /// 
    /// 
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

    #region 

    protected const float ROTATE_SPEED = 20f;    
    protected const float RAY_DISTANCE = 100f;

    #endregion

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public virtual void Init()
    {
        // Data
        currentUnitStat = new CurrentUnitStat(unitStat);

        // UI
        unitHUD = Managers.UI.MakeWordSpaceUI<UI_UnitHUD>(transform);
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public virtual void OnUpdate()
    {
        Move();
        Select();

        CastActiveSkill();

        if (Input.GetKeyDown(KeyCode.K))
        {
            currentUnitStat.OnDamaged(10);
        }

        
    }

    public virtual void Update()
    {
        switch (CurrentState)
        {
            case Define.UnitState.IDLE: UpdateIdle(); break;
            case Define.UnitState.MOVE: UpdateMove(); break;
        }
    }

    #region 

    /// <summary>
    /// 
    /// 
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: 
    }

    /// <summary>
    /// 
    /// 
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
    /// 
    /// 
    /// </summary>
    public void Select()
    {
        if(Managers.Input.CheckKeyEvent(0))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy == null || (ui_dummy != null && !ui_dummy.IsCreate))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object")))
                {
                    Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    protected virtual void Move()
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
     
    #region 스킬 관련 함수

    /// <summary>
    /// 
    /// 
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
    /// 
    /// 
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// 
    /// 
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

    #region 액티브 스킬 함수

    /// <summary>
    /// 액티브 스킬 Q 실행 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q 스킬 사용");

        StartCoroutine(CoolActive(0));
    }

    private IEnumerator CoolActive(int index)
    {
        UI_UnitBottomLayer unitBottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
        if (unitBottomLayer?.GetCooltime((UI_UnitBottomLayer.CooltimeType)index) > 0f) yield break;

        float currentTime = unitSkill.Actives[index].Cooltime;
        unitBottomLayer?.SetCooltime((UI_UnitBottomLayer.CooltimeType)index, currentTime, unitSkill.Actives[index].Cooltime);        

        while (unitBottomLayer?.GetCooltime((UI_UnitBottomLayer.CooltimeType)index) > 0f)
        {
            currentTime -= Time.deltaTime;
            unitBottomLayer?.SetCooltime((UI_UnitBottomLayer.CooltimeType)index, currentTime, unitSkill.Actives[index].Cooltime);

            yield return null;
        }
        yield break;
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W 스킬 사용");

        StartCoroutine(CoolActive(1));
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E 스킬 사용");

        StartCoroutine(CoolActive(2));
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R 스킬 사용");

        StartCoroutine(CoolActive(3));
    }

    #endregion

    #region 

    protected virtual void CastPassive()
    {
        Debug.Log("패시브 스킬 사용");
    }

    #endregion

    #region D,F스킬

    protected virtual void CastSpellD()
    {
        Debug.Log("D 스킬 사용");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F 스킬 사용");
    }

    #endregion

    #endregion
}
