using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Yasuo : Unit
{
    private ParticleSystem spellEffect;

    // Q 스킬
    private IEnumerator co_SpellQ_OutTime;      // Q 스택 초기화 시간 코루틴
    private int spellQ_Count = 0;               // Q 스택 카운트
    private float spellQ_currentTimer = 0f;     // Q 스택 현재 유지 시간

    #region 프로퍼티

    public override Define.UnitState CurrentState 
    { 
        get => base.CurrentState; 
        set
        {
            base.CurrentState = value;

            switch(base.CurrentState)
            {
                case Define.UnitState.IDLE: anim?.SetFloat("MovementSpeed", 0f); break;
                case Define.UnitState.MOVE: anim?.SetFloat("MovementSpeed", currentUnitStat.MoveMentSpeed); break;
            }
        }
    }

    public int SpellQ_Count
    {
        get => spellQ_Count;
        set
        {
            spellQ_Count = value;

            if (spellQ_Count <= 0) return;

            anim.SetBool("SpellQ", true);
            anim.SetInteger("SpellQ_Count", spellQ_Count);

            CountingSpellQ_Stack();

            switch (spellQ_Count)
            {
                case 2: spellEffect.Play(); break;          // 2스택 이벤트 처리
                case 3: spellEffect.Stop(); break;          // 3스택 이벤트 처리
            }
        }
    }

    #endregion

    #region 상수

    private const float SPELL_Q_STACK_TIME = 6f;            // Q 스택 유지 시간
    private const float SPELL_W_WORK_TIME = 4f;             // W 유지 시간

    #endregion

    public override void Init()
    {
        Debug.Log("야스오가 생성되었습니다.");

        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Yasuo]);
        unitSkill = new UnitSkill(Define.UnitName.Yasuo);
        spellEffect = GetComponentInChildren<ParticleSystem>(); 

        base.Init();
    }

    public override void Move()
    {
        if (CurrentState == Define.UnitState.SPELLQ || CurrentState == Define.UnitState.SPELLW ||
            CurrentState == Define.UnitState.SPELLE || CurrentState == Define.UnitState.SPELLR) return;

        base.Move();
    }

    #region 액티브 스킬

    #region 액티브 Q

    protected override void CastActiveQ()
    {
        if (anim.GetBool("SpellQ")) return;

        targetPos = default;
        CurrentState = Define.UnitState.SPELLQ;

        StartCoroutine(Thrust());
    }

    private IEnumerator Thrust()
    {
        SpellQ_Count++;

        // 해당 스킬 애니메이션인지 딜레이
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("SPELL_Q_" + SpellQ_Count))
        {
            //anim.SetBool("SpellQ", true);
            yield return null;
        }

        float animDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        float elapsedTime = 0f;

        if (SpellQ_Count >= 3)
        {
            Vector3 initialPosition = transform.position;                           // 시작 위치
            Vector3 targetPosition = initialPosition + transform.forward;
            Vector3 spawnPosition = new Vector3(targetPosition.x, transform.localScale.y / 1f, targetPosition.z);
            Vector3 moveDirection = (targetPosition - initialPosition).normalized;

            GameObject tornado = Managers.Resource.Instantiate("Unit/Yasuo/Tornado", spawnPosition, transform.rotation);
            tornado.GetComponent<TornadoController>().moveDirect = moveDirection;
            Managers.Resource.Destroy(tornado, SPELL_W_WORK_TIME);
        }

        while (elapsedTime < animDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("SpellQ", false);
        CurrentState = Define.UnitState.IDLE;

        if (SpellQ_Count >= 3) SpellQ_Count = 0;

        base.CastActiveQ();
    }

    /// <summary>
    /// 야스오 Q 스킬 스택 체크 함수
    /// 김민섭_230921
    /// </summary>
    private void CountingSpellQ_Stack()
    {
        if (co_SpellQ_OutTime != null)
        {
            StopCoroutine(co_SpellQ_OutTime);
        }

        if (SpellQ_Count >= 3) return;          // 3스택에서는 바로 초기화

        co_SpellQ_OutTime = Co_CheckSpellQ_OutTime();
        StartCoroutine(co_SpellQ_OutTime);
    }

    /// <summary>
    /// 야스오 Q 스킬 스택 초기화 체크 코루틴 함수
    /// 김민섭_230921
    /// </summary>
    private IEnumerator Co_CheckSpellQ_OutTime()
    {
        spellQ_currentTimer = SPELL_Q_STACK_TIME;

        while (true)
        {
            spellQ_currentTimer -= Time.deltaTime;

            if (spellQ_currentTimer <= 0f)
            {   // 6초가 지났으면 스택 초기화
                spellQ_currentTimer = 0f;
                SpellQ_Count = 0;
                spellEffect.Stop();
                yield break;
            }

            yield return null;
        }
    }

    #endregion

    #region 액티브 W

    protected override void CastActiveW()
    {
        if (anim.GetBool("SpellW")) return;

        targetPos = default;
        CurrentState = Define.UnitState.SPELLW;

        StartCoroutine(WindWall());
    }

    private IEnumerator WindWall()
    {
        // 해당 스킬 애니메이션인지 딜레이
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("SPELL_W"))
        {
            anim.SetBool("SpellW", true);
            yield return null;
        }

        float animDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;                           // 시작 위치
        Vector3 targetPosition = initialPosition + transform.forward * 10f;

        while (elapsedTime < animDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 spawnPosition = new Vector3(targetPosition.x, transform.localScale.y / 2f, targetPosition.z);

        GameObject windwall = Managers.Resource.Instantiate("Unit/Yasuo/WindWall", spawnPosition, transform.rotation);
        Managers.Resource.Destroy(windwall, SPELL_W_WORK_TIME);

        anim.SetBool("SpellW", false);
        CurrentState = Define.UnitState.IDLE;

        base.CastActiveW();
    }

    #endregion

    #region 액티브 E

    protected override void CastActiveE()
    {
        if (anim.GetBool("SpellE")) return;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object", "Unit_Blue", "Unit_Red")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);

            targetPos = default;
            CurrentState = Define.UnitState.SPELLE;

            Vector3 goalPos = hit.transform.position;
            goalPos.y = 0f;

            // 타겟을 바라보게 회전
            transform.LookAt(goalPos);

            // 타겟을 향해 돌진 시작
            StartCoroutine(Dash(goalPos));

            // 타겟에게 데미지 부여
            StartCoroutine(SpellDamage(hit.transform));
        }
    }

    private IEnumerator Dash(Vector3 targetPosition)
    {
        // 해당 스킬 애니메이션인지 딜레이
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("SPELL_E"))
        {
            anim.SetBool("SpellE", true);
            yield return null;
        }

        Vector3 initialPosition = transform.position;
        float dashDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            Vector3 moveDirection = (targetPosition - initialPosition).normalized;

            transform.position = Vector3.Lerp(initialPosition, targetPosition + moveDirection * 10f, t);
            elapsedTime += Time.deltaTime * 2.2f;

            yield return null;
        }

        anim.SetBool("SpellE", false);
        CurrentState = Define.UnitState.IDLE;

        base.CastActiveE();
    }

    private IEnumerator SpellDamage(Transform target)
    {
        CurrentUnitStat targetStat = target.GetComponent<Unit>().CurrentUnitStat;       // 타겟의 현재 스탯

        // 60 + (0.2 물리계수) + (0.6 주문력계수)

        float atk = currentUnitStat.Atk * 0.2f;
        float apk = currentUnitStat.Apk * 0.6f;
        float totalDamage = 60 + atk + apk;

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("SPELL_E"))
        {
            yield return null;
        }

        float dashDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration / 3.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetStat.OnDamaged(totalDamage);
    }

    #endregion

    #region 액티브 R

    protected override void CastActiveR()
    {
        if (anim.GetBool("SpellR")) return;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object", "Unit_Blue", "Unit_Red")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);

            targetPos = default;
            CurrentState = Define.UnitState.SPELLR;

            Vector3 goalPos = hit.transform.position;
            goalPos.y = 0f;

            // 타겟을 바라보게 회전
            transform.LookAt(goalPos);

            // 타겟을 향해 돌진 시작
            StartCoroutine(Ultimate(goalPos));
        }
    }

    private IEnumerator Ultimate(Vector3 targetPosition)
    {
        // 해당 스킬 애니메이션인지 딜레이
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("SPELL_R"))
        {
            anim.SetBool("SpellR", true);
            yield return null;
        }

        Vector3 initialPosition = transform.position;
        float dashDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            Vector3 moveDirection = (targetPosition - initialPosition).normalized;

            //transform.position = Vector3.Lerp(initialPosition, targetPosition + moveDirection * 10f, t);
            transform.position = targetPosition + moveDirection * 8f;
            elapsedTime += Time.deltaTime * 2f;

            transform.LookAt(targetPosition);

            yield return null;
        }

        anim.SetBool("SpellR", false);
        CurrentState = Define.UnitState.IDLE;

        base.CastActiveR();
    }

    #endregion

    #endregion
}
