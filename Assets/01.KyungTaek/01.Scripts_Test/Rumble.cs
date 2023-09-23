using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 럼블 캐릭터를 위한 스크립트
/// 배경택 _ 230921
/// </summary>
public class Rumble : Unit
{
    public Animator animator;
    public GameObject RangeImg_R;
    public GameObject RangeImg_Attack;
    public GameObject ShotImg;
    public GameObject Effect_Q;
    public GameObject MuzzleE;
    public GameObject Effect_E;
    public GameObject Effect_R;

    private Vector3 startPosR;
    private Vector3 rotationR;

    // 공격 체크를 위한 bool
    private bool isAttack;

    // 다른 플레이어를 담아줄 게임오브젝트
    private GameObject otherPlayer;

    // 럼블 객체 Init
    public override void Init()
    {
        Debug.Log("럼블 생성");

        unitStat = new UnitStat(
            Managers.Data.UnitBaseStatDict[Define.UnitName.Rumble]);
        unitSkill = new UnitSkill(Define.UnitName.Rumble);
        animator = GetComponent<Animator>();
        base.Init();
    }

    // 움직임 구현에 추가해야할 내용이 있어서 Override함, Unit은 현재 공용으로 사용중이므로 수정하지 않았음
    protected override void Move()
    {
        if (Managers.Input.CheckKeyEvent(1))
        {
            Debug.Log("시작");
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy == null || (ui_dummy != null && !ui_dummy.IsCreate))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                Vector3 direct = targetPos - transform.position;
                direct.y = 0f;

                // 맞은 상대가 Object일 경우 기본공격을 위해 isAttack을 true로 변경함
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object")))
                {
                    isAttack = true;
                    otherPlayer = hit.transform.gameObject;
                    Debug.Log("상대 선택");
                }
                else
                {
                    otherPlayer = default;
                    isAttack = false;
                }

                // 히트 포인트 위치 저장
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
                {
                    Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                    targetPos = hit.point;

                    CurrentState = Define.UnitState.MOVE;
                }                
            }
        }
    }

    // 스킬 캐스팅
    public override void CastActiveSkill()
    {
        if (Input.GetKey(KeyCode.A))
        {
            BasicAttackMove();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RangeImg_Attack.SetActive(false);
        }

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
            CurrentState = Define.UnitState.CastE;
            CastActiveE();
        }

        if (Input.GetKey(KeyCode.R))
        {
            CastActiveR();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RangeImg_R.SetActive(false);
            ShotImg.SetActive(false);
        }
    }

    // 일반 공격을 위해 이동, 실제 공격처리는 UpdateMove()에서 처리함
    private void BasicAttackMove()
    {
        RangeImg_Attack.SetActive(true); // 평타 범위 보이도록 변경
        if (Managers.Input.CheckKeyEvent(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object")))
            {
                isAttack = true;

                otherPlayer = hit.transform.gameObject;

                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);

                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;

                targetPos = hit.point;

                CurrentState = Define.UnitState.MOVE;
            }
        }
    }

    // 스킬 Q
    protected override void CastActiveQ()
    {
        StartCoroutine(SkillQ()); //스킬 Q 보였다가 꺼지도록
        base.CastActiveQ();
    }

    IEnumerator  SkillQ()
    {
        Effect_Q.SetActive(true);
        Effect_Q.GetComponent<CalculateDamage>().damage = unitStat.Atk;
        yield return new WaitForSeconds(3f);

        Effect_Q.SetActive(false);
    }

    // 스킬 W
    protected override void CastActiveW()
    {
        animator.SetTrigger("W");

        base.CastActiveW();
    }

    // 스킬 E
    protected override void CastActiveE()
    {
        
        Invoke("InstantiateSkillE",0.2f);
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

            targetPos = hit.point;

            Vector3 direct = targetPos - transform.position;
            direct.y = 0f;

            transform.rotation = Quaternion.LookRotation(direct);
        }

        base.CastActiveE();
    }

    // 애니메이션 모션에 맞춰 스킬 E를 나가게 하기 위한 함수_Invoke에 사용
    private void InstantiateSkillE()
    {
        GameObject skiil_E = Instantiate(Effect_E, MuzzleE.transform.position, MuzzleE.transform.rotation);
        skiil_E.GetComponent<CalculateDamage>().damage = unitStat.Atk;
    }

    // 스킬 R
    protected override void CastActiveR()
    {
        RangeImg_R.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
            {
                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                startPosR = hit.point;

                ShotImg.SetActive(true);
                ShotImg.transform.position = hit.point;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_DISTANCE))
            {
                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                rotationR = hit.point;

                ShotImg.transform.LookAt(hit.point);

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CurrentState = Define.UnitState.CastR;

            Vector3 direction = (rotationR - startPosR).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            // Effect_R을 계산된 위치와 회전값으로 생성
            GameObject skillR = Instantiate(Effect_R, startPosR, rotation);

            Destroy(skillR, 3f);

            RangeImg_R.SetActive(false);
            ShotImg.SetActive(false);
            
            base.CastActiveR();
        }
    }

    protected override void CastPassive()
    {
        //base.CastPassive();
    }


    // 실제 이동처리와 공격처리를 하는 함수
    protected override void UpdateMove()
    {
        if (targetPos == default) return;

        Vector3 direct = targetPos - transform.position;
        direct.y = 0f;

        float minDistance = direct.magnitude;

        
        if(isAttack == true && unitStat.AttackRange >= direct.magnitude)
        {
            targetPos = default;
            transform.rotation = Quaternion.LookRotation(direct);
            CurrentState = Define.UnitState.Attack;



            return;
        }
        else if (minDistance <= 1f && CurrentState != Define.UnitState.Attack)
        {
            targetPos = default;
            CurrentState = Define.UnitState.IDLE;
            return;
        }
        else if(currentState == Define.UnitState.MOVE)
        {
            float moveDistance = Mathf.Clamp(unitStat.MoveMentSpeed * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), ROTATE_SPEED * Time.deltaTime);
        }
    }

    //죽었는지 체크
    bool isDead = false;

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (currentUnitStat.Hp <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("나 죽었다");
            CurrentState = Define.UnitState.Die;
        }
    }


    // 상태에 맞춰서 애니메이션 처리함
    public override Define.UnitState CurrentState
    {
        get => base.CurrentState;
        set
        {
            currentState = value;

            switch (currentState)
            {
                case Define.UnitState.IDLE:
                    animator.SetBool("Move", false);
                    animator.SetBool("Attack",false);
                    break;

                case Define.UnitState.MOVE:
                    animator.SetBool("Move", true);
                    animator.SetBool("Attack",false);

                    break;

                // 이동을 하면서 사용해야하는 스킬은 주석처리함
                // MOVE상태일때만 이동하게끔 함수가 되어있음
                //case Define.UnitState.CastQ:
                //    animator.SetTrigger("Q");
                //    break;

                //case Define.UnitState.CastW:
                //    animator.SetTrigger("W");
                //    break;

                case Define.UnitState.CastE:
                    animator.SetTrigger("E");
                    break;

                case Define.UnitState.CastR:
                    animator.SetTrigger("R");
                    break;

                case Define.UnitState.Attack:                 
                    animator.SetTrigger("AttackTR"); // AnyState에서 Transition을 원활하게 하기 위한 Trigger
                    animator.SetBool("Attack",true);
                    animator.SetBool("Move", false);
                    break;

                case Define.UnitState.Die:
                    animator.SetTrigger("Death");
                    break;
            }
        }
    }
}
