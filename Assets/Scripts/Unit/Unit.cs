using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected CharacterStat unitStat;     // 유닛 스탯 데이터 (원본값)
    protected Vector3 targetPos;          // 이동할 위치

    [Header("현재 유닛 상태")]
    [SerializeField] protected Define.UnitState currentState = Define.UnitState.IDLE;

    #region 프로퍼티

    /// <summary>
    /// 현재 유닛 상태 프로퍼티
    /// 김민섭_230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            
            // TODO: 상태에 따라 관련된 애니메이션 실행
        }
    }

    #endregion

    #region 상수

    private const float ROTATE_SPEED = 20f;     // 유닛 회전속도
    private const float RAY_DISTANCE = 100f;     // 레이 사정거리

    #endregion

    private void Start()
    {
        Init();
    }

    public abstract void Init();

    /// <summary>
    /// InputManager 클래스의 OnUpdate 함수에서 사용되는 함수
    /// 김민섭_230906
    /// </summary>
    public void OnUpdate()
    {
        Move();
        Select();

        switch (CurrentState)
        {
            case Define.UnitState.IDLE: UpdateIdle(); break;
            case Define.UnitState.MOVE: UpdateMove(); break;
        }
    }

    #region 상태별 업데이트 함수

    /// <summary>
    /// 상태가 IDLE일 때 실행되는 업데이트 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: 보류
    }

    /// <summary>
    /// 상태가 MOVE일 때 실행되는 업데이트 함수
    /// 김민섭_230906
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
            float moveDistance = Mathf.Clamp(unitStat.moveMentSpeed * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), ROTATE_SPEED * Time.deltaTime);
        }
    }

    #endregion

    /// <summary>
    /// 유닛 선택 함수
    /// 김민섭_230906
    /// </summary>
    private void Select()
    {
        if(Managers.Input.CheckKeyEvent(0))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy != null) return;

            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit")))
            {
                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);
            }
        }
    }

    /// <summary>
    /// 유닛 움직임 체크 함수
    /// 김민섭_230906
    /// </summary>
    public void Move()
    {
        if (Managers.Input.CheckKeyEvent(1))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy != null) return;

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

    #region 스킬 관련 함수

    /// <summary>
    /// 키 입력에 따라 액티브 스킬 시전하는 함수
    /// 김민섭_230906
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
    /// 패시브 스킬 시전하는 함수
    /// 김민섭_230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// 키 입력에 따라 스펠 스킬 시전하는 함수
    /// 김민섭_230906
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

    #region 액티브 스킬

    /// <summary>
    /// 액티브 스킬 Q 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 W 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 E 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 R 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R 스킬 발동");
    }

    #endregion

    #region 패시브 스킬

    protected virtual void CastPassive()
    {
        Debug.Log("패시브 스킬");
    }

    #endregion

    #region 스펠 스킬

    protected virtual void CastSpellD()
    {
        Debug.Log("D 스펠 발동");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F 스펠 발동");
    }

    #endregion

    #endregion
}
