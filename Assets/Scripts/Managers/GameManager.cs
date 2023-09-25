using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// 게임매니저
/// 배경택_230906
/// </summary>
public class GameManager
{
    /// <summary>
    /// 현재 스테이지 종류 enum
    /// 배경택_230906
    /// </summary>
    public enum CurrentStageMode
    {
        WAIT_STAGE, BATTLE_STAGE
    }

    /// <summary>
    /// 대기 스테이지 enum
    /// 배경택_230906
    /// </summary>
    public enum WaitStageMode
    {
        BASIC_MODE,
        MONEY_1000_MODE,
        MONEY_3000_MODE
    }

    /// <summary>
    /// 전투 스테이지 enum
    /// 배경택_230906
    /// </summary>
    public enum BattleStageMode
    {
        READY_MODE,
        FIGHT_MODE,
        MANETIC_FIGHT_MODE
    }

    // 라운드에 맞춰서 대기실 모드 결정(인스펙터창에서 변경 가능) _230906 배경택
    private WaitStageMode[] waitStageModes = {
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_1000_MODE,
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_1000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_3000_MODE
    };

    private CurrentStageMode currentStage = CurrentStageMode.WAIT_STAGE;        // 스테이지 종류 (대기 / 전투)
    private WaitStageMode waitStageMode = WaitStageMode.BASIC_MODE;             // 대기 스테이지 타입 (기본 / 1000원 지급 / 3000원 지급)
    private BattleStageMode battleStageMode = BattleStageMode.READY_MODE;       // 전투 스테지이 타입 (대기 / 전투 / 자기장 전투)

    private GameObject director;        // 카메라 관리자

    private int roundNumber;            // 현재 라운드 번호
    private float baseTimer;            // 게임 타이머

    /// <summary>
    /// 현재 스테이지 종류 프로퍼티
    /// 김민섭_230925
    /// </summary>
    public CurrentStageMode CurrentStage
    {
        get => currentStage;
        set
        {
            currentStage = value;

            switch(currentStage)
            {
                case CurrentStageMode.WAIT_STAGE: director.transform.position = new Vector3(0f, 0f, WAITZONE_POSITION_Z); break;
                case CurrentStageMode.BATTLE_STAGE: director.transform.position = new Vector3(0f, 0f, BATTLEZONE_POSITION_Z); break;
            }
        }
    }

    #region 상수

    private const float WAIT_AREA_TIME = 5f;            // 40f
    private const float BATTLE_READY_TIME = 1f;         // 5f
    private const float START_MAGNETIC_TIME = 1f;       // 30f
    private const float MAGNETIC_CYCLE = 5f;            // 5f
    private const float WAITZONE_POSITION_Z = -93f;     // 대기존 카메라 z좌표       
    private const float BATTLEZONE_POSITION_Z = 0f;     // 배틀존 카메라 z좌표

    #endregion

    ///////////////////////////////////////

    // 카메라 변수_230906 배경택
    public Camera cameraWaitArea;
    public Camera cameraBattleArea1;
    public Camera cameraBattleArea2;

    // 자기장 _230906 배경택
    public GameObject magneticField1;
    public GameObject magneticField2;

    // 팀 체력 변수 _230906 배경택
    private const int TEAM_HP_20 = 20;
    private int team1HP;
    private int team2HP;
    private int team3HP;
    private int team4HP;
    public bool isTeam1Win;
    public bool isTeam2Win;
    public bool isTeam3Win;
    public bool isTeam4Win;

    // 4가지 팀 정의 _230917 배경택
    public enum Team
    {
        Team1,
        Team2,
        Team3,
        Team4
    }

    // 라운드 별 패배시 데미지 _230907 배경택
    private int Team4amage;
    private int[] teamRoundDamages =
    {
        0,2,2,2,4,4,4,6,6,6,8,8,8,10,10,10
    };

    // 라운드 종료 _230906 배경택
    private bool isRoundOver;

    // 게임종료, 한 팀만 살아남았을 경우 _230906 배경택
    private bool isGameOver;

    // 자기장 움직임 변수 _230907 배경택
    private int magneticCount = 0;
    private Vector3 MoveTomagneticPos1;
    private Vector3 MoveTomagneticPos2;
    private bool isArriveMagnetic;

    // 캐릭터 리스폰 포인트를 위한 변수 _230907 배경택
    public GameObject[] waitAreaResPoint;
    public GameObject[] battle1ResPoint;
    public GameObject[] battle2ResPoint;

    // 게임오브젝트 생성 및 삭제를 위한 변수 _230907 배경택
    public GameObject healFlower; // 체력꽃 프리펩
    public GameObject jumpFlower; // 점프꽃 프리펩
    public GameObject[] healFlowerPoints; // 체력꽃 생성 위치
    public GameObject[] jumpFlowerPoints; // 점프꽃 생성 위치
    private List<GameObject> healFlowers; // 체력꽃 저장
    private List<GameObject> jumpFlowers; // 점프꽃 저장

    // 게임 플레이어
    public GameObject playerPrefab;
    private GameObject myPlayer;

    public void Init()
    {
        director = GameObject.Find("Director");

        roundNumber = 0;

        CurrentStage = CurrentStageMode.WAIT_STAGE;
        waitStageMode = WaitStageMode.BASIC_MODE;
        battleStageMode = BattleStageMode.READY_MODE;
    }

    public void OnUpdate()
    {
        //GameFlow(); //게임 흐름 함수(시간에 따라 제어) _230906 배경택

        //if (!isArriveMagnetic) // 자기장이 전부 줄어들었는가 _230907 배경택
        //{
        //    MoveUpMagneticField(); // 자기장 움직임 _230907 배경택
        //}

        baseTimer += Time.deltaTime;

        switch(currentStage)
        {
            case CurrentStageMode.WAIT_STAGE: UpdateWaitStage(); break;
            case CurrentStageMode.BATTLE_STAGE: UpdateBattleStage(); break;
        }
    }

    #region 업데이트 함수

    /// <summary>
    /// 대기실 업데이트 함수
    /// 김민섭_230925
    /// </summary>
    private void UpdateWaitStage()
    {
        Debug.Log("대기실입니다.");

        if(baseTimer >= WAIT_AREA_TIME)
        {   // 대기실 시간이 다 지나면 배틀스테이지로 이동한다.
            baseTimer = 0f;

            CurrentStage = CurrentStageMode.BATTLE_STAGE;
            battleStageMode = BattleStageMode.READY_MODE;

            Debug.Log("경기장으로 이동합니다.");
            return;
        }
    }

    private void UpdateBattle_ReadyMode()
    {
        // TODO: 전투에서 사용될 오브젝트 관리자 활성화

        if(baseTimer >= BATTLE_READY_TIME)
        {   // 전투 대기 시간이 종료되면 실행
            baseTimer = 0f;
            battleStageMode = BattleStageMode.FIGHT_MODE;

            Debug.Log("전투가 시작됩니다.");

            return;
        }
    }

    private void UpdateBattle_FightMode()
    {
        if(baseTimer >= START_MAGNETIC_TIME)
        {
            baseTimer = 0f;
            battleStageMode = BattleStageMode.MANETIC_FIGHT_MODE;

            Debug.Log("자기장이 생성됩니다.");

            return;
        }
    }

    private void UpdateBattle_Manetic()
    {
        if(baseTimer >= MAGNETIC_CYCLE)
        {
            baseTimer = 0f;

            if (magneticCount < 3) // Test : 3번 자기장 위로 움직이게 실행
            {
                MagneticCycle(); // Cycle마다 자기장의 다음 움직일 위치 계산
            }
            else if (magneticCount == 3) // Test : 자기장 일정높이 도달시 라운드종료 할 수 있도록
            {
                EndMagneticField(); // 자기장 모드 종료시 실행
            }
        }
    }

    private void UpdateBattleStage()
    {
        Debug.Log("전투 중입니다.");

        // TODO: 라운드 종료 처리

        switch (battleStageMode)
        {
            case BattleStageMode.READY_MODE: UpdateBattle_ReadyMode(); break;
            case BattleStageMode.FIGHT_MODE: UpdateBattle_FightMode(); break;
            case BattleStageMode.MANETIC_FIGHT_MODE: UpdateBattle_Manetic(); break;
        }
    }

        #endregion

        /// <summary>
        /// 타이머 및 게임 스테이지 흐름 제어
        /// 배경택 230906
        /// </summary>
        private void GameFlow()
    {
        //baseTimer += Time.deltaTime;

        //switch (currentStage)
        //{
        //    case Stage.WAIT_STAGE:
        //        Debug.Log("대기실");

        //        if(baseTime >= WAIT_AREA_TIME) // 대기실 시간이 다 지나면 배틀스테이지로 이동
        //        {
        //            baseTime = 0f;
        //            battleStageMode = BattleStageMode.READY_MODE;
        //            stage = Stage.BATTLE_STAGE;
        //            isRoundOver = false;
        //            Debug.Log("경기장 이동");

        //            // 랜덤 한 위치로 카메라 이동
        //            int randomBattleArea = Random.Range(0, 2);
        //            if (randomBattleArea == 1)
        //            {
        //                //MoveToBattleArea1(); TODO
        //            }
        //            else
        //            {
        //                //MoveToBattlArea2(); TODO
        //            }
        //            break;
        //        }

        //        switch (waitStageMode) // 라운드 수에 따라 모드에 맞게 실행
        //        {
        //            case WaitStageMode.BASIC_MODE:
        //                Debug.Log("대기실_기본");

        //                break;

        //            case WaitStageMode.MONEY_1000_MODE:
        //                Debug.Log("대기실_1000_돈");

        //                break;

        //            case WaitStageMode.MONEY_3000_MODE:
        //                Debug.Log("대기실_3000_돈");

        //                break;

        //            case WaitStageMode.CARD_MODE:
        //                Debug.Log("대기실_카드");

        //                break;
        //        }
        //        break;

        //    case Stage.BATTLE_STAGE:

        //        if (isRoundOver) // 라운드가 종료되었다면 라운드 수를 높이고 대기실로 이동
        //        {
        //            baseTime = 0f;
        //            RoundOver(); //라운드 종료시 실행
        //            break;
        //        }

        //        switch (battleStageMode) // 배틀스테이지 모드 시간에 따라 수행
        //        {
        //            case BattleStageMode.READY_MODE:

        //                CreateGameObject();

        //                if(baseTime >= BATTLE_READY_TIME)
        //                {
        //                    baseTime = 0f;
        //                    battleStageMode = BattleStageMode.FIGHT_MODE;
        //                }
        //                Debug.Log("준비모드");
        //                break;

        //            case BattleStageMode.FIGHT_MODE:
        //                if (baseTime >= START_MAGNETIC_TIME)
        //                {
        //                    baseTime = 0f;
        //                    battleStageMode = BattleStageMode.MANETIC_FIGHT_MODE;
        //                    StartMagneticField();
        //                }
        //                Debug.Log("전투모드");
        //                break;

        //            case BattleStageMode.MANETIC_FIGHT_MODE: //자기장 시작됨
        //                if (baseTime >= MAGNETIC_CYCLE) // 자기장 주기에 맞춰서 실행
        //                {
        //                    baseTime = 0f;

        //                    if(magneticCount < 3) // Test : 3번 자기장 위로 움직이게 실행
        //                    {
        //                        MagneticCycle(); // Cycle마다 자기장의 다음 움직일 위치 계산
        //                    }
        //                    else if(magneticCount == 3) // Test : 자기장 일정높이 도달시 라운드종료 할 수 있도록
        //                    {
        //                        EndMagneticField(); // 자기장 모드 종료시 실행
        //                    }
        //                }
        //                Debug.Log("자기장 모드");
        //                break;
        //        }
        //        break;
        //}
    }




    /// <summary>
    /// 자기장 움직임
    /// 배경택 230907
    /// </summary>
    #region 자기장 움직임

    //배경택 230907
    private void StartMagneticField() // 자기장 생성
    {
        magneticField1.transform.localPosition = Vector3.zero; // 자기장 위치 초기화
        magneticField2.transform.localPosition = Vector3.zero; // 자기장 위치 초기화
        MoveTomagneticPos1 = magneticField1.transform.localPosition; // 자기장이 움직일 위치 초기화
        MoveTomagneticPos2 = magneticField2.transform.localPosition; // 자기장이 움직일 위치 초기화
        magneticField1.SetActive(true);
        magneticField2.SetActive(true);
    }

    //배경택 230907
    private void EndMagneticField() // 자기장모드 종료
    {
        magneticCount = 0;
        magneticField1.SetActive(false);
        magneticField2.SetActive(false);

        isRoundOver = true; // Test : 추후 게임결과 나올때 라운드 오버로 변경
    }

    //배경택 230907
    private void MoveUpMagneticField() // 자기장 위쪽방향으로 움직이며 자기장 크기 조절
    {
        magneticField1.transform.localPosition = Vector3.Lerp(magneticField1.transform.localPosition, MoveTomagneticPos1, Time.deltaTime * 2f);
        magneticField2.transform.localPosition = Vector3.Lerp(magneticField2.transform.localPosition, MoveTomagneticPos2, Time.deltaTime * 2f);
        if(magneticField1.transform.position == MoveTomagneticPos1 && magneticField2.transform.position == MoveTomagneticPos2)
        {
            isArriveMagnetic = true;
        }
    }

    //배경택 230907
    private void MagneticCycle() // 자기장 위쪽방향의 위치값 저장
    {
        Debug.Log("자기장 +1");
        magneticCount += 1;
        isArriveMagnetic = false;

        MoveTomagneticPos1 = magneticField1.transform.localPosition + (Vector3.up * 1.5f);
        MoveTomagneticPos2 = magneticField2.transform.localPosition + (Vector3.up * 1.5f);
    }
    #endregion 자기장 움직임


    /// <summary>
    /// 게임 오브젝트 생성
    /// 배경택 _ 230907
    /// </summary>
    #region 게임오브젝트 생성
    private void CreateGameObject() // 게임오브젝트 생성 _ 230907 배경택
    {
        CreateHealFlower();
        CreateJumpFlower();
    }
    private void CreateHealFlower() // 체력을 채워주는 꽃 생성 _ 230907 배경택
    {
        //foreach(GameObject healPoint in healFlowerPoints)
        //{
        //    healFlowers.Add(Instantiate(healFlower, healPoint.transform.position, Quaternion.identity));
        //}
    }
    private void CreateJumpFlower() // 터지면 점프하게 하는 꽃 생성 _ 230907 배경택
    {
        //foreach (GameObject jumpPoint in jumpFlowerPoints)
        //{
        //    jumpFlowers.Add(Instantiate(jumpFlower, jumpPoint.transform.position, Quaternion.identity));
        //}
    }

    private void DestroyGameObject() // 게임오브젝트 삭제 _ 230907 배경택
    {
        DestroyHealFlower();
        DestroyJumpFlower();
    }
    private void DestroyHealFlower() // 체력을 채워주는 꽃 파괴 _ 230907 배경택
    {
        //foreach(GameObject healFlower in healFlowers)
        //{
        //    Destroy(healFlower);
        //}
    }
    private void DestroyJumpFlower() // 점프를 하게만드는 꽃 파괴 _ 230907 배경택
    {
        //foreach(GameObject jumpFlower in jumpFlowers)
        //{
        //    Destroy(jumpFlower);
        //}
    }
    #endregion 게임 오브젝트 생성

    /// <summary>
    /// 라운드 오버시 실행
    /// 배경택 _ 230907
    /// </summary>
    private void RoundOver()
    {
        roundNumber += 1; // 라운드 변경
        waitStageMode = waitStageModes[roundNumber]; //라운드별 스테이지 모드 적용
        Team4amage = teamRoundDamages[roundNumber]; //라운드별 체력감소량 적용

        //stage = Stage.WAIT_STAGE;
        RoundResult(); // 캐릭터 및 팀 체력계산되면서 수정필요
        GameResult(); // 게임 결과 계산 내용 수정필요 
        DestroyGameObject();
        ReturnWaitArea();
        Debug.Log("대기실로 이동");
    }

    #region 맵 지역 이동
    /// <summary>
    /// 카메라 이동
    /// TODO 캐릭터 이동
    /// 배경택 230906
    /// </summary>
    public void MoveToBattleArea1(Team team1, Team team2)
    {
        //cameraWaitArea.gameObject.SetActive(false);
        //cameraBattleArea2.gameObject.SetActive(false);
        //cameraBattleArea1.gameObject.SetActive(true);
        ////TODO 캐릭터 이동
    }

    public void MoveToBattlArea2(Team team1, Team team2)
    {
        //cameraWaitArea.gameObject.SetActive(false);
        //cameraBattleArea1.gameObject.SetActive(false);
        //cameraBattleArea2.gameObject.SetActive(true);
        ////TODO 캐릭터 이동
    }

    public void ReturnWaitArea()
    {
        //cameraBattleArea1.gameObject.SetActive(false);
        //cameraBattleArea2.gameObject.SetActive(false);
        //cameraWaitArea.gameObject.SetActive(true);
        ////TODO 캐릭터 이동
    }
    #endregion

    /// <summary>
    /// 팀 별로 배틀을 어떻게 이루어지게 할 것인지 결정
    /// 배경택 _ 230917
    /// </summary>
    private void BattleSystem()
    {
        List<Team> ExistTeams = CheckExistTeam(); //현재 존재하는 팀을 확인

        if(ExistTeams.Count == 2)
        {
            
        }
        else if(ExistTeams.Count == 3)
        {

        }
        else if(ExistTeams.Count == 4)
        {

        }
        else
        {
            //TODO ExistTeams[0] = win 하도록
        }
    }

    // 현재 존재하는 팀을 체크함 _ 230917 배경택
    private List<Team> CheckExistTeam()
    {
        List<Team> teams = new List<Team>();

        if (team1HP > 0)
        {
            teams.Add(Team.Team1);
        }
        if (team2HP > 0)
        {
            teams.Add(Team.Team2);
        }
        if (team3HP > 0)
        {
            teams.Add(Team.Team3);
        }
        if (team4HP > 0)
        {
            teams.Add(Team.Team4);
        }

        return teams;
    }



    #region 라운드 및 최종 결과 계산 _230907 배경택
    // 라운드 결과 계산 _230907 배경택
    private void RoundResult()
    {
        if (!isTeam1Win)
        {
            team1HP -= Team4amage;
            isTeam1Win = true;
        }

        if (!isTeam2Win)
        {
            team2HP -= Team4amage;
            isTeam2Win = true;
        }

        if (!isTeam3Win)
        {
            team3HP -= Team4amage;
            isTeam3Win = true;
        }

        if (!isTeam4Win)
        {
            team4HP -= Team4amage;
            isTeam4Win = true;
        }
    }

    //최종 경기 결과 계산 _ 230906 배경택
    private void GameResult() 
    {
        if(team1HP <= 0)
        {
            //TODO team1을 갖고있는 플레이어들은 게임 결과창이 보이는 로비로 나가짐
        }
        if (team2HP <= 0)
        {
            //TODO team2을 갖고있는 플레이어들은 게임 결과창이 보이는 로비로 나가짐
        }
        if (team3HP <= 0)
        {
            //TODO team3을 갖고있는 플레이어들은 게임 결과창이 보이는 로비로 나가짐
        }
        if (team4HP <= 0)
        {
            //TODO team4을 갖고있는 플레이어들은 게임 결과창이 보이는 로비로 나가짐
        }

        if (team1HP != 0 && team2HP == 0 && team3HP == 0 && team4HP == 0)
        {
            //TODO team1 게임승리
        }
        else if(team2HP != 0 && team1HP == 0 && team3HP == 0 && team4HP == 0)
        {
            //TODO team2 게임승리
        }
        else if(team3HP != 0 && team1HP == 0 && team2HP == 0 && team4HP == 0)
        {
            //TODO team3 게임승리
        }
        else if(team4HP != 0 && team1HP == 0 && team2HP == 0 && team3HP == 0)
        {
            //TODO team4 게임승리
        }
    }
    #endregion

    //TODO 관전시 시야 이동
}
