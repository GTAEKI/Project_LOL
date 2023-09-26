using System.Collections;
using UnityEngine;



public class Fiora : Unit
{
    private const float ROTATE_SPEED = 20f;     // ?醫롫짗??용쐻??덉굲 ???쐻??덉굲?醫롫셽?紐꾩굲
    private const float RAY_DISTANCE = 100f;

    Animator FioraAnimator = default;
    ///거리 관련
    private float attackRange = 2.0f;//공격사정거리
    private float detectionEnemyRange = 100f;//적자동공격하러가는 탐지거리
    private float detectionrangePassive = 20f;//패시브탐지거리
    private float QSkillDistance = 5.0f; //Q스킬 거리
    //private float RSkillDistance = 4.0f; //R스킬 시전가능거리
    /// <summary>
    /// 패시브관련
    /// </summary>
    private float PassiveTime = 10f; //패시브가 공격당하지 않은지 10초지나면 다른방향으로 다시 재생성
    public GameObject passive;
    public GameObject UltPassive;
    private bool isDetecting = false;

    /// <summary>
    /// Q 스킬관련
    /// </summary>

    private float QSkillmoveSpeed = 12.0f; //Q스킬 이동속도
    private float QSkillDuration = 0.5f; //Q스킬시전시간

    private Vector3 targetPosition;   // 이동할 목표 위치
    private Vector3 qSkillDirection;  // Q 스킬 이펙트 방향
    private bool isMoving = false;    // 이동 중인지 여부

    public GameObject qSkillEffectPrefab; // Q 스킬 이펙트 프리팹을 할당할 변수
    private GameObject qSkillEffectInstance; // 이펙트 인스턴스를 저장할 변수

    public GameObject wSkillEffectPrefab; // Q 스킬 이펙트 프리팹을 할당할 변수
    private GameObject wSkillEffectInstance; // 이펙트 인스턴스를 저장할 변수
    private float WSkillDuration = 0.5f;

    public override void Init()
    {
        Debug.Log("피오라가 생성되었습니다.");

        unitStat = new UnitStat(Managers.Data.UnitBaseStatDict[Define.UnitName.Dummy_Puppet]);
        unitSkill = new UnitSkill(Define.UnitName.Dummy_Puppet);


        FioraAnimator = GetComponent<Animator>();
        StartCoroutine(DetectionCoroutine());
        base.Init();
    }
    private IEnumerator DetectionCoroutine()
    {
        while (true) // 계속 실행하도록 루프 설정
        {
            if (!isDetecting)
            {
                // 탐지 중이 아닐 때만 탐지 실행
                DetectionEnemy();
            }
            yield return new WaitForSeconds(1.0f); // 1초마다 한 번씩 실행 (원하는 간격으로 조절 가능)
        }
    }


    public override void OnUpdate()
    {
        Define.UnitState unitState = CurrentState;

        DetectionEnemy();

        if (Managers.Input.CheckKeyEvent(1))
        {
            UI_DummyController ui_dummy = Managers.UI.GetPopupUI<UI_DummyController>();
            if (ui_dummy == null || (ui_dummy != null && !ui_dummy.IsCreate))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, RAY_DISTANCE, LayerMask.GetMask("Enemy")))
                {

                }
            }
        }

        base.OnUpdate();

    }



    public Unit[] allUnits;

    public void DetectionEnemy()
    {
        // 현재 유닛의 팀을 가져옵니다.
        Define.GameTeam myTeam = UnitTeam;

        // 모든 유닛을 배열로 가져옵니다.
        //Unit[] 
        allUnits = FindObjectsOfType<Unit>();

        foreach (Unit unit in allUnits)
        {
            // 현재 유닛과 같은 팀인 경우 무시합니다.
            if (unit.UnitTeam == myTeam)
            {
                continue;
            }
            // 적인 경우
            else if (unit.UnitTeam != myTeam)
            {


                float distanceToEnemy = Vector3.Distance(transform.position, unit.transform.position);


                if (distanceToEnemy <= detectionEnemyRange)
                {
                    isDetecting = true;

                    DetectionMove(unit);

                }
            }
                    isDetecting = false;
        }
    }

    public void DetectionMove(Unit targetEnemy)
    {
        
            if (targetEnemy == null)
            {
                Debug.LogWarning("대상 적이 유효하지 않습니다.");
                return;
            }

            // 현재 유닛의 위치와 타겟 적의 위치 사이의 거리 계산
            float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.transform.position);

            // 이미 공격 범위 내에 있다면 추가 이동하지 않고 공격
            if (distanceToEnemy <= attackRange)
            {
                Attack(targetEnemy);
                return;
            }

            // 타겟 적의 위치로 이동
            Vector3 targetPosition = targetEnemy.transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;

            float speed = unitStat.MoveMentSpeed;
            // 이동 속도를 곱하여 이동
            transform.position += direction * Time.deltaTime * speed;

            // 이동 완료 후에 다시 한 번 체크하여 공격
            float updatedDistance = Vector3.Distance(transform.position, targetPosition);
            if (updatedDistance <= attackRange)
            {
                Attack(targetEnemy);
            }
            else
            {
                // 공격 범위에 도달하지 못한 경우 추가 이동 로직을 구현할 수 있습니다.
                // 예를 들어, 타겟 적을 추적하거나 다른 동작을 수행할 수 있습니다.
            }
            FioraAnimator.SetBool("Infight", false);
        
    }


    public void Attack(Unit enemyUnit)
    {
        FioraAnimator.SetBool("Attack", true);
        FioraAnimator.SetBool("Critical", true);
    }






    protected override void CastPassive()
    {

        base.CastPassive();
    }


    protected override void CastActiveQ()
    {
        QskillRotationandMoveTowards();
        FioraAnimator.SetTrigger("q");
        FioraAnimator.SetBool("Infight", true);

        base.CastActiveQ();
    }

    #region  Q스킬관련
    private void QskillRotationandMoveTowards()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Floor")))
        {
            Vector3 newTargetPosition = hit.point;

            // 이동 중이 아니거나 새로운 목표 위치가 현재 위치와 다르면 이동 시작
            if (!isMoving || newTargetPosition != targetPosition)
            {
                // 새로운 목표 위치와 현재 위치 사이의 거리 계산
                float distanceToNewPosition = Vector3.Distance(transform.position, newTargetPosition);
                // 마우스 포지션과 현재 위치 사이의 거리가 QSkillDistance 보다 작으면 즉시 이동
                if (distanceToNewPosition <= QSkillDistance)
                {
                    targetPosition = newTargetPosition;
                    isMoving = true;
                }
                else
                {
                    // 마우스 포지션 방향으로 QSkillDistance 만큼만 이동
                    targetPosition = transform.position + (newTargetPosition - transform.position).normalized * QSkillDistance;
                    isMoving = true;

                    // 코루틴을 시작하여 스무스한 이동을 실행
                    StartCoroutine(SmoothMoveToTarget());
                }
                // 코루틴을 시작하여 스무스한 이동을 실행
                StartCoroutine(SmoothMoveToTarget());
            }

            if (isMoving)
            {
                qSkillDirection = targetPosition - transform.position;
                qSkillDirection.y = 0f;

                Vector3 targetDirection = targetPosition - transform.position;
                targetDirection.y = 0f;

                if (targetDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 20000f * Time.deltaTime);
                }
            }
            // 패시브 스킬을 감지하는 코드 추가
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionrangePassive, LayerMask.GetMask("Enemy"));

            if (hitColliders.Length > 0)
            {
                // 적이 주변에 있을 때 Q 스킬 이펙트 생성
                if (qSkillEffectPrefab != null)
                {
                    // 이펙트를 적 위치로 보내려면 hitColliders[0] 또는 다른 방법을 사용하여 적의 위치를 가져와서 해당 위치로 이펙트를 생성하십시오.
                    Vector3 enemyPosition = hitColliders[0].transform.position; // 첫 번째 적의 위치를 가져옴

                    // 이펙트를 바라보도록 회전 설정
                    Quaternion rotation = Quaternion.LookRotation(enemyPosition - transform.position);

                    qSkillEffectInstance = Instantiate(qSkillEffectPrefab, enemyPosition, rotation);

                    // 이펙트가 필요한 지속 시간 후에 제거
                    StartCoroutine(DestroyQSkillEffectAfterDelay());
                }
            }

        }
    }

    private IEnumerator SmoothMoveToTarget()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < QSkillDuration)
        {
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float journeyTime = journeyLength / QSkillmoveSpeed;

            float distanceCovered = (Time.time - startTime) * QSkillmoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // 목표 위치에 도달하면 이동 중지
        transform.position = targetPosition;
        isMoving = false;

        // Q 스킬 이펙트 생성
        if (qSkillEffectPrefab != null)
        {
            // 이펙트를 마우스 클릭 지점에 생성
            Vector3 effectPosition = targetPosition;
            // 이펙트를 바라보도록 회전 설정
            Quaternion rotation = Quaternion.LookRotation(qSkillDirection);

            qSkillEffectInstance = Instantiate(qSkillEffectPrefab, effectPosition, rotation);

            // 이펙트가 필요한 지속 시간 후에 제거
            StartCoroutine(DestroyQSkillEffectAfterDelay());
        }
    }

    private IEnumerator DestroyQSkillEffectAfterDelay()
    {
        yield return new WaitForSeconds(QSkillDuration); // 이펙트 지속 시간을 대기

        if (qSkillEffectInstance != null)
        {
            Destroy(qSkillEffectInstance); // 이펙트 제거
        }
    }
    #endregion  

    protected override void CastActiveW()
    {
        WskillRotateMouse();
        FioraAnimator.SetTrigger("w");
        FioraAnimator.SetBool("Infight", true);

        base.CastActiveW();
    }

    #region W스킬관련
    private void WskillRotateMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Floor")))
        {
            Vector3 targetDirection = hit.point - transform.position;
            
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero)
            {
                // 스킬을 시작 위치에서 시작합니다.
                Vector3 skillStartPosition = transform.position;

                // 이펙트를 마우스 방향으로 회전합니다.
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 20000f * Time.deltaTime);

                // 스킬 이펙트의 종료 위치를 시작 위치에서 시작 위치로부터 이동 방향으로 0.5f만큼 앞쪽으로 설정합니다.
                Vector3 skillEndPosition = skillStartPosition + targetDirection.normalized * 0.5f;

            

            // "wSkillEffectPrefab" 이펙트를 생성합니다.
            GameObject wSkillEffectInstance = Instantiate(wSkillEffectPrefab, skillStartPosition, targetRotation);

            // 스킬 이동 코루틴을 시작합니다.
            StartCoroutine(MoveSkill(wSkillEffectInstance, skillStartPosition, skillEndPosition));
                
            }
        }

    }
    private IEnumerator MoveSkill(GameObject skillEffect, Vector3 startPosition, Vector3 endPosition)
    {
        float startTime = Time.time;
        float duration = 0.5f;
        while (Time.time - startTime < duration)
        {
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float distanceCovered = (Time.time - startTime);
            float fractionOfJourney = distanceCovered / journeyLength;

            skillEffect.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            yield return null;
        }

        // 스킬 이동이 끝나면 이펙트를 제거합니다.
        Destroy(skillEffect);
    }

    #endregion


    protected override void CastActiveE()
    {
        FioraAnimator.SetBool("e", true);
        FioraAnimator.SetBool("Infight", true);

        base.CastActiveE();
    }
    protected override void CastActiveR()
    {
        FioraAnimator.SetBool("Infight", true);

        base.CastActiveR();
    }



    protected override void UpdateIdle()
    {
        FioraAnimator.SetBool("Moving", false);
        FioraAnimator.SetInteger("Speed", 0);
        base.UpdateIdle();

    }

    protected override void UpdateMove()
    {
        FioraAnimator.SetBool("Moving", true);
        FioraAnimator.SetInteger("Speed", 2);
        base.UpdateMove();
    }

















}