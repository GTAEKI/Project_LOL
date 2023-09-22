using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : Unit
{
    public Animator animator;

    public GameObject RangeImg_Attack;
    public GameObject ShotImg_R;

    public GameObject Effect_Attack;
    public GameObject Effect_E;
    public GameObject Effect_R;

    public GameObject Muzzle_Attack;
    public GameObject Muzzle_W;
    public GameObject Muzzle_E;
    public GameObject Muzzle_R;

    private Vector3 startPosR;
    private Vector3 rotationR;

    // 공격 체크를 위한 bool
    private bool isAttack;

    // 다른 플레이어를 담아줄 게임오브젝트
    private GameObject otherPlayer;

    // 애쉬 객체 Init
    public override void Init()
    {
        Debug.Log("애쉬 생성");

        unitStat = new UnitStat(
            Managers.Data.UnitBaseStatDict[Define.UnitName.Ashe]);
        unitSkill = new UnitSkill(Define.UnitName.Ashe);
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
            CurrentState = Define.UnitState.CastW;
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
            ShotImg_R.SetActive(false);
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
        animator.SetBool("Q", true) ;
        base.CastActiveQ();
    }

    // 스킬 W
    protected override void CastActiveW()
    {
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
        //Instantiate(Effect_Attack, Muzzle_W.transform.position, Muzzle_W.transform.rotation);

        int numProjectiles = 10; // 이펙트 개수
        float startAngle = -45f; // 시작 각도
        float endAngle = 45f; // 끝 각도

        float angleStep = (endAngle - startAngle) / (numProjectiles - 1);

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 spawnDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            GameObject skill_W = Instantiate(Effect_Attack, Muzzle_W.transform.position, Quaternion.LookRotation(spawnDirection));
            Destroy(skill_W, 0.7f);
        }

        animator.SetTrigger("W");
        base.CastActiveW();

        currentState = Define.UnitState.IDLE;
    }

    // 스킬 E
    protected override void CastActiveE()
    {
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
        Instantiate(Effect_E, Muzzle_E.transform.position, Muzzle_E.transform.rotation);
        base.CastActiveE();
    }

    // 스킬 R
    protected override void CastActiveR()
    {
        ShotImg_R.SetActive(true);

        startPosR = transform.position;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);
            ShotImg_R.transform.position = hit.point;
            Vector3 direction = (hit.point-startPosR).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            ShotImg_R.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, RAY_DISTANCE))
            {
                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                transform.LookAt(hit.point);

                GameObject skillR = Instantiate(Effect_R, Muzzle_R.transform.position, Muzzle_R.transform.rotation);
                Destroy(skillR, 10f);

                ShotImg_R.SetActive(false);

                CurrentState = Define.UnitState.CastR;

                base.CastActiveR();
            }
        }
    }

    protected override void CastPassive()
    {
        //base.CastPassive();
    }



    IEnumerator Attack()
    {
        while (true)
        {
            animator.SetTrigger("AttackTR");

            yield return new WaitForSeconds(0.3f);

            Instantiate(Effect_Attack, Muzzle_Attack.transform.position, Muzzle_Attack.transform.rotation);
        }
    }

    // 실제 이동처리와 공격처리를 하는 함수
    protected override void UpdateMove()
    {
        if (targetPos == default) return;

        Vector3 direct = targetPos - transform.position;
        direct.y = 0f;

        float minDistance = direct.magnitude;


        if (isAttack == true && unitStat.AttackRange >= direct.magnitude)
        {
            targetPos = default;
            transform.rotation = Quaternion.LookRotation(direct);
            CurrentState = Define.UnitState.Attack;
            StartCoroutine(Attack());
            return;
        }
        else if (minDistance <= 1f && CurrentState != Define.UnitState.Attack)
        {
            targetPos = default;
            CurrentState = Define.UnitState.IDLE;
            return;
        }
        else if (currentState == Define.UnitState.MOVE)
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
                    animator.SetBool("Attack", false);
                    animator.SetTrigger("Idle");
                    animator.SetBool("Move", false);
                    break;

                case Define.UnitState.MOVE:
                    animator.SetBool("Move", true);
                    animator.SetBool("Attack", false);

                    break;

                // 이동을 하면서 사용해야하는 스킬은 주석처리함
                // MOVE상태일때만 이동하게끔 함수가 되어있음
                //case Define.UnitState.CastQ:
                //    animator.SetTrigger("Q");
                //    break;

                case Define.UnitState.CastW:
                    targetPos = default;
                    animator.SetTrigger("W");
                    break;

                case Define.UnitState.CastE:
                    animator.SetTrigger("E");
                    break;

                case Define.UnitState.CastR:
                    animator.SetTrigger("R");
                    break;

                case Define.UnitState.Attack:
                    //animator.SetTrigger("AttackTR"); // AnyState에서 Transition을 원활하게 하기 위한 Trigger
                    animator.SetBool("Attack", true);
                    animator.SetBool("Move", false);
                    break;

                case Define.UnitState.Die:
                    animator.SetTrigger("Death");
                    break;
            }
        }
    }
}
