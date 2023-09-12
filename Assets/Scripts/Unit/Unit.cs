using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // ?좎룞?쇿뜝?숈삕?좎룞??
    protected UnitStat unitStat;                    // ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕?좎룞??(?좎룞?쇿뜝?숈삕?좎룞??
    protected CurrentUnitStat currentUnitStat;      // ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕?좎룞??(?좎룞?쇿뜝?밴컪)
    protected Vector3 targetPos;                    // ?좎떛?몄삕?좎룞???좎룞?숈튂

    // UI
    protected UI_UnitHUD unitHUD;       // ?좎룞?쇿뜝?숈삕 泥닷뜝?밸콈??

    [Header("?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕")]
    [SerializeField] protected Define.UnitState currentState = Define.UnitState.IDLE;

    #region ?좎룞?쇿뜝?숈삕?좎룞?숉떚

    /// <summary>
    /// ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕?좎룞?숉떚
    /// ?좎룞?숅뿙??230906
    /// </summary>
    public virtual Define.UnitState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            
            // TODO: ?좎룞?쇿뜝?뱀슱???좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?쒕벝???좎뙇?덈챿?쇿뜝?깆눦???좎룞?쇿뜝?숈삕
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

    #region ?좎룞?쇿뜝?

    private const float ROTATE_SPEED = 20f;     // ?좎룞?쇿뜝?숈삕 ?뚦뜝?숈삕?좎뙂?몄삕
    private const float RAY_DISTANCE = 100f;     // ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕?좎떊紐뚯삕

    #endregion

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ?좎룞?쇿뜝?숈삕 ?좎떗源띿삕???좎뙃?쎌삕
    /// ?좎룞?숅뿙??230911
    /// </summary>
    public virtual void Init()
    {
        currentUnitStat = new CurrentUnitStat(unitStat);
        currentUnitStat.OnHeal(unitStat.Hp);

        unitHUD = Managers.UI.MakeWordSpaceUI<UI_UnitHUD>(transform);       // ?좎룞?쇿뜝?숈삕 HUD ?좎룞?쇿뜝?숈삕
    }

    /// <summary>
    /// InputManager ?닷뜝?숈삕?좎룞?쇿뜝?숈삕 OnUpdate ?좎뙃?쎌삕?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?ㅻ뙋???좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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

    #region ?좎룞?쇿뜝?밸툦???좎룞?쇿뜝?숈삕?좎룞?숉듃 ?좎뙃?쎌삕

    /// <summary>
    /// ?좎룞?쇿뜝?밴낀??IDLE?좎룞???좎룞???좎룞?쇿뜝?숈삕?붷뜝??좎룞?쇿뜝?숈삕?좎룞?숉듃 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    protected virtual void UpdateIdle()
    {
        // TODO: ?좎룞?쇿뜝?숈삕
    }

    /// <summary>
    /// ?좎룞?쇿뜝?밴낀??MOVE?좎룞???좎룞???좎룞?쇿뜝?숈삕?붷뜝??좎룞?쇿뜝?숈삕?좎룞?숉듃 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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
    /// ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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
    /// ?좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕?좎룞??泥댄겕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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

    #region ?좎룞?숉궗 ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕

    /// <summary>
    /// ???좎뙃?μ슱???좎룞?쇿뜝?숈삕 ?좎룞?숉떚?좎룞???좎룞?숉궗 ?좎룞?쇿뜝?숈삕?좎떦?먯삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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
    /// ?좎떩?쒕툦???좎룞?숉궗 ?좎룞?쇿뜝?숈삕?좎떦?먯삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// ???좎뙃?μ슱???좎룞?쇿뜝?숈삕 ?좎룞?쇿뜝?숈삕 ?좎룞?숉궗 ?좎룞?쇿뜝?숈삕?좎떦?먯삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
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

    #region ?좎룞?숉떚?좎룞???좎룞?숉궗

    /// <summary>
    /// ?좎룞?숉떚?좎룞???좎룞?숉궗 Q ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q ?좎룞?숉궗 ?좎뙥?몄삕");
    }

    /// <summary>
    /// ?좎룞?숉떚?좎룞???좎룞?숉궗 W ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W ?좎룞?숉궗 ?좎뙥?몄삕");
    }

    /// <summary>
    /// ?좎룞?숉떚?좎룞???좎룞?숉궗 E ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E ?좎룞?숉궗 ?좎뙥?몄삕");
    }

    /// <summary>
    /// ?좎룞?숉떚?좎룞???좎룞?숉궗 R ?좎룞?쇿뜝?숈삕 ?좎뙃?쎌삕
    /// ?좎룞?숅뿙??230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R ?좎룞?숉궗 ?좎뙥?몄삕");
    }

    #endregion

    #region ?좎떩?쒕툦???좎룞?숉궗

    protected virtual void CastPassive()
    {
        Debug.Log("?좎떩?쒕툦???좎룞?숉궗");
    }

    #endregion

    #region ?좎룞?쇿뜝?숈삕 ?좎룞?숉궗

    protected virtual void CastSpellD()
    {
        Debug.Log("D ?좎룞?쇿뜝?숈삕 ?좎뙥?몄삕");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F ?좎룞?쇿뜝?숈삕 ?좎뙥?몄삕");
    }

    #endregion

    #endregion
}
