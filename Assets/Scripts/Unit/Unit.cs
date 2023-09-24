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

    // 스펠 쿨타임 체크 변수
    [SerializeField] protected bool isCool_SpellQ = false;
    [SerializeField] protected bool isCool_SpellW = false;
    [SerializeField] protected bool isCool_SpellE = false;
    [SerializeField] protected bool isCool_SpellR = false;

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

    protected Animator anim;            // 유닛 애니메이터

    #region 프로퍼티

    /// <summary>
    /// 현재 상태에 따라 처리하는 프로퍼티
    /// 김민섭_230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set => currentState = value;
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

    #region 상수

    protected const float ROTATE_SPEED = 20f;     // 유닛 회전 속도
    protected const float RAY_DISTANCE = 100f;     // 레이 사거리

    #endregion

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 유닛 초기화 함수
    /// 김민섭_230911
    /// </summary>
    public virtual void Init()
    {
        // Data
        currentUnitStat = new CurrentUnitStat(unitStat);

        // UI
        unitHUD = Managers.UI.MakeWordSpaceUI<UI_UnitHUD>(transform); 

        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// InputManager 에서 사용하는 OnUpdate 함수
    /// 김민섭_230906
    /// </summary>
    public void OnUpdate()
    {
        Move();
        Select();

        CastActiveSkill();
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
            // 이동속도 비율 조정 0.03f
            float moveDistance = Mathf.Clamp((currentUnitStat.MoveMentSpeed * 0.03f) * Time.deltaTime, 0f, direct.magnitude);
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
    public virtual void Move()
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
    /// ???醫롫셾?關????醫롫짗??용쐻??덉굲 ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 ?醫롫짗??용쐻??덉굲?醫롫뼣?癒?굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    public virtual void CastActiveSkill()
    {
        // 기본 상태 및 움직임 
        if (!(CurrentState == Define.UnitState.MOVE || CurrentState == Define.UnitState.IDLE)) return;

        if (!isCool_SpellQ && Input.GetKeyDown(KeyCode.Q))
        {
            CastActiveQ();
        }

        if (!isCool_SpellW && Input.GetKeyDown(KeyCode.W))
        {
            CastActiveW();
        }

        if (!isCool_SpellE && Input.GetKeyDown(KeyCode.E))
        {
            CastActiveE();
        }

        if (!isCool_SpellR && Input.GetKeyDown(KeyCode.R))
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

    #region 액티브 스킬 함수

    /// <summary>
    /// 쿨타임 체크 함수
    /// 김민섭_230917
    /// </summary>
    /// <param name="index">스킬 인덱스</param>
    private IEnumerator CoolActive(int index)
    {
        UI_UnitBottomLayer unitBottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();
        if (unitBottomLayer?.GetCooltime((UI_UnitBottomLayer.CooltimeType)index) > 0f) yield break;

        switch (index)
        {
            case 0: isCool_SpellQ = true; break;
            case 1: isCool_SpellW = true; break;
            case 2: isCool_SpellE = true; break;
            case 3: isCool_SpellR = true; break;
        }

        float currentTime = unitSkill.Actives[index].Cooltime;
        unitBottomLayer?.SetCooltime((UI_UnitBottomLayer.CooltimeType)index, currentTime, unitSkill.Actives[index].Cooltime);

        while (unitBottomLayer?.GetCooltime((UI_UnitBottomLayer.CooltimeType)index) > 0f)
        {
            currentTime -= Time.deltaTime;
            unitBottomLayer?.SetCooltime((UI_UnitBottomLayer.CooltimeType)index, currentTime, unitSkill.Actives[index].Cooltime);

            yield return null;
        }

        switch (index)
        {
            case 0: isCool_SpellQ = false; break;
            case 1: isCool_SpellW = false; break;
            case 2: isCool_SpellE = false; break;
            case 3: isCool_SpellR = false; break;
        }

        yield break;
    }

    /// <summary>
    /// 액티브 스킬 Q 실행 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q 스킬 사용");

        StartCoroutine(CoolActive(0));
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 W ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W 스킬 사용");

        StartCoroutine(CoolActive(1));
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 E ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E 스킬 사용");

        StartCoroutine(CoolActive(2));
    }

    /// <summary>
    /// ?醫롫짗??됰뼒?醫롫짗???醫롫짗??됯텢 R ?醫롫짗??용쐻??덉굲 ?醫롫셾??뚯굲
    /// ?醫롫짗??낅퓳??230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R 스킬 사용");

        StartCoroutine(CoolActive(3));
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
