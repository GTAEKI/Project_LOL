using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : Unit
{
    public Animator animator;

    public GameObject rangeImg_Attack;
    public GameObject shotImg_R;

    public GameObject effect_Attack;
    public GameObject effect_E;
    public GameObject effect_R;

    public GameObject muzzle_Attack;
    public GameObject muzzle_Q;
    public GameObject muzzle_W;
    public GameObject muzzle_E;
    public GameObject muzzle_R;

    public GameObject vision;

    private Vector3 startPosR;

    // 공격 체크를 위한 bool
    private bool isAttack;

    // 다른 플레이어를 담아줄 게임오브젝트
    //private GameObject otherPlayer;
    public int otherActorNumber;

    private PhotonView pv;

    // 애쉬 객체 Init
    public override void Init()
    {
        Debug.Log("애쉬 생성");

        unitStat = new UnitStat(
            Managers.Data.UnitBaseStatDict[Define.UnitName.Ashe]);
        unitSkill = new UnitSkill(Define.UnitName.Ashe);
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
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

                // 맞은 상대가 Object일 경우 기본공격을 위해 isAttack을 true로 변경함 +  나를 선택한것이 아니라면
                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object")) && hit.transform.gameObject != pv.IsMine)
                {
                    isAttack = true;
                    otherActorNumber = hit.transform.GetComponent<PhotonView>().Owner.ActorNumber;
                    //otherPlayer = hit.transform.gameObject;
                    Debug.Log("상대 선택");

                }
                else
                {
                    otherActorNumber = 0;
                    //otherPlayer = default;
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
            rangeImg_Attack.SetActive(false);
        }

        if (!isCool_SpellQ && Input.GetKeyDown(KeyCode.Q))
        {

            CastActiveQ();
        }

        if (!isCool_SpellW && Input.GetKeyDown(KeyCode.W))
        {
            CurrentState = Define.UnitState.CastW;
            CastActiveW();
        }

        if (!isCool_SpellE && Input.GetKeyDown(KeyCode.E))
        {
            CurrentState = Define.UnitState.CastE;
            CastActiveE();
        }

        if (!isCool_SpellR && Input.GetKey(KeyCode.R))
        {
            CastActiveR();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            shotImg_R.SetActive(false);
        }
    }

    // 일반 공격을 위해 이동, 실제 공격처리는 UpdateMove()에서 처리함
    private void BasicAttackMove()
    {
        rangeImg_Attack.SetActive(true); // 평타 범위 보이도록 변경
        if (Managers.Input.CheckKeyEvent(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            // 유닛 오브젝트를 선택했다면
            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Unit_Object")))
            {
                isAttack = true;

                otherActorNumber = hit.transform.GetComponent<PhotonView>().Owner.ActorNumber;

                //otherPlayer = hit.transform.gameObject;

                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.blue);

                targetPos = hit.point;

                CurrentState = Define.UnitState.MOVE;
            }
            else
            {
                otherActorNumber = 0;
            }
        }
    }

    // 스킬 Q
    protected override void CastActiveQ()
    {
        CurrentState = Define.UnitState.CastQ;
        base.CastActiveQ();
    }

    // 스킬 W
    protected override void CastActiveW()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

            targetPos = hit.point;

            Vector3 direct = targetPos - transform.position;
            direct.y = 0f;

            transform.rotation = Quaternion.LookRotation(direct);
        }

        photonView.RPC("CastActiveWRPC", RpcTarget.All);

        animator.SetTrigger("W");
        base.CastActiveW();
    }

    // 네트워크 상 상대편 컴퓨터에서도 데미지가 들어갈 수 있도록 RPC처리함
    [PunRPC]
    private void CastActiveWRPC()
    {
        int numProjectiles = 10; // 이펙트 개수
        float startAngle = -45f; // 시작 각도
        float endAngle = 45f; // 끝 각도

        float angleStep = (endAngle - startAngle) / (numProjectiles - 1);

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 spawnDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            GameObject skill_W = Instantiate(effect_Attack, muzzle_W.transform.position, Quaternion.LookRotation(spawnDirection));
            skill_W.GetComponent<CalculateDamage>().damage = unitStat.Atk; // 스킬 W 데미지 계산
            Destroy(skill_W, 0.7f);
        }
    }

    // 스킬 E
    protected override void CastActiveE()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

            targetPos = hit.point;

            Vector3 direct = targetPos - transform.position;
            direct.y = 0f;

            transform.rotation = Quaternion.LookRotation(direct);
        }
        GameObject skill_E = Instantiate(effect_E, muzzle_E.transform.position, muzzle_E.transform.rotation);
        StartCoroutine(DetectionSight(skill_E, targetPos));
        base.CastActiveE();
    }

    IEnumerator DetectionSight(GameObject skill_E, Vector3 targetPos)
    {
        while (true)
        {
            Debug.Log("스킬 들어왔다");
            if (skill_E.transform.position.z >= targetPos.z)
            {
                Destroy(skill_E);
                GameObject _vision = Instantiate(vision, skill_E.transform.position, Quaternion.identity);
                Destroy(_vision, 5f);
                Debug.Log("시야 탐지");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

    // 스킬 R
    protected override void CastActiveR()
    {
        shotImg_R.SetActive(true);

        startPosR = transform.position;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
        {
            Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);
            shotImg_R.transform.position = hit.point;
            Vector3 direction = (hit.point - startPosR).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            shotImg_R.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Floor")))
            {
                Util.DrawTouchRay(Camera.main.transform.position, hit.point, Color.red);

                transform.LookAt(hit.point);

                GameObject skillR = Instantiate(effect_R, muzzle_R.transform.position, muzzle_R.transform.rotation);
                skillR.GetComponent<CalculateDamage>().damage = unitStat.Atk * 3; // 데미지 계산
                Destroy(skillR, 10f);

                shotImg_R.SetActive(false);

                CurrentState = Define.UnitState.CastR;

                base.CastActiveR();
            }
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


        if (isAttack == true && unitStat.AttackRange >= direct.magnitude)
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
        else if (currentState == Define.UnitState.MOVE)
        {
            float moveDistance = Mathf.Clamp((currentUnitStat.MoveMentSpeed * 0.03f) * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), ROTATE_SPEED * Time.deltaTime);
        }
    }

    private void AutoAttack()
    {
        if (photonView.IsMine && otherActorNumber != 0)
        {
            photonView.RPC("AutoAttackRPC", RpcTarget.All, otherActorNumber);
        }
    }

    [PunRPC]
    private void AutoAttackRPC(int _otherActorNumber)
    {
        GameObject guidedArrow = Instantiate(effect_Attack, muzzle_Attack.transform.position, muzzle_Attack.transform.rotation);
        //guidedArrow.GetComponent<GuidedArrow>().enemy = otherPlayer;
        guidedArrow.GetComponent<GuidedArrow>().actorNumber = _otherActorNumber;
        guidedArrow.GetComponent<CalculateDamage>().damage = unitStat.Atk;
        
        Debug.Log(unitStat.Atk);

    }

    /// <summary>
    /// 애니메이션 이벤트에 맞춰 실행되는 함수_ 스킬 Q실행시
    /// 배경택 _ 230923
    /// </summary>
    private void AutoAttackQ()
    {
        if (photonView.IsMine && otherActorNumber != 0)
        {
            StartCoroutine(MakeAttackQ());
        }
    }

    // AutoAttackQ에 맞춰 실행되는 코루틴, 여러개의 화살을 발사하기 위한 함수
    IEnumerator MakeAttackQ()
    {
        for (int i = -2; i < 3; i++)
        {
            photonView.RPC("MakeAttackQRPC", RpcTarget.All, i, otherActorNumber);
            yield return new WaitForSeconds(0.05f); // 각 화살을 일정 시간 간격으로 발사 (조절 가능)
        }
    }

    [PunRPC]
    private void MakeAttackQRPC(int i, int _otherActorNumber)
    {
        Vector3 localPosition = new Vector3(i * 0.3f, 0f, 0f);
        Vector3 arrowPosition = muzzle_Q.transform.TransformPoint(localPosition);

        // 화살 생성
        GameObject guidedArrow = Instantiate(effect_Attack, arrowPosition, muzzle_Attack.transform.rotation);
        //guidedArrow.GetComponent<GuidedArrow>().enemy = otherPlayer; // Enemy값을 추가하여 화살이 대상을 따라가도록 함
        guidedArrow.GetComponent<GuidedArrow>().actorNumber = _otherActorNumber;// Enemy값을 추가하여 화살이 대상을 따라가도록 함
        guidedArrow.GetComponent<CalculateDamage>().damage = unitStat.Atk / 3; // 데미지 계산

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

                case Define.UnitState.CastQ:
                    animator.SetBool("Q", true);
                    animator.SetTrigger("QTR");
                    animator.SetBool("Attack", true);
                    break;

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
                    animator.SetTrigger("AttackTR"); // AnyState에서 Transition을 원활하게 하기 위한 Trigger
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
