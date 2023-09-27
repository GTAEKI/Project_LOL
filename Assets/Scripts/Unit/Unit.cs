using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Unit : MonoBehaviourPun
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

            switch (unitTeam)
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

    // 시야 오브젝트
    private FieldOfView eyeSight;

    #region 프로퍼티

    /// <summary>
    /// 현재 상태에 따라 처리하는 프로퍼티
    /// 김민섭_230906
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
        //eyeSight = transform.Find("ViewVisualisation");
        eyeSight = transform.GetComponentInChildren<FieldOfView>();

        if (photonView.IsMine)
        {
            eyeSight.gameObject.SetActive(true);
            Debug.Log("시야 켰다");
        }
        else
        {
            eyeSight.gameObject.SetActive(false);
            Debug.Log("시야 껏다");
        }
    }

    /// 유닛 초기화 함수
    /// 김민섭_230911
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
    public virtual void OnUpdate()
    {
        Move();
        Select();

        CastActiveSkill();

        // Test: 데미지 테스트 코드
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

    #region 업데이트 함수

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
            // 이동속도 비율 조정 0.03f
            float moveDistance = Mathf.Clamp((currentUnitStat.MoveMentSpeed * 0.03f) * Time.deltaTime, 0f, direct.magnitude);
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
        if (Managers.Input.CheckKeyEvent(0))
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

    private float currDamageTime = 0f;
    private float damageTimeMax = 2.5f;

    private void OnTriggerStay(Collider other)
    {
        // TODO: 자기장에 닿으면 일정 시간마다 지속 데미지
        if(other.tag == "MagneticField")
        {
            currDamageTime += Time.deltaTime;

            if(currDamageTime >= damageTimeMax )
            {
                currDamageTime = 0f;
                currentUnitStat.OnDamaged((int)(unitStat.Hp * 0.05f));  // 최대 체력의 5퍼센트씩 데미지 부여
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // TODO: 자기장에서 벗어나면 지속 데미지 초기화
        if (other.tag == "MagneticField")
        {
            currDamageTime = 0f;
        }
    }
}
