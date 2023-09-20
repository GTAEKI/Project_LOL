using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임매니저
/// 배경택_230906
/// </summary>
public class GameManager
{
    // 게임 스테이지 및 모드 _230906 배경택
    private enum Stage
    {
        WAIT_STAGE,
        BATTLE_STAGE
    }

    private Dictionary<int, Transform[]> waitAreaSpawnPointDict = new Dictionary<int, Transform[]>();       // 대기존 스폰 포인트
    private Dictionary<int, Transform[]> battleAreaASpawnPointDict = new Dictionary<int, Transform[]>();    // 배틀존A 스폰 포인트
    private Dictionary<int, Transform[]> battleAreaBSpawnPointDict = new Dictionary<int, Transform[]>();    // 배틀존B 스폰 포인트
    private Dictionary<int, TeamController> totalTeamDict = new Dictionary<int, TeamController>();          // 아레나 참가팀

    private GameObject director;                                                                    // 카메라 관리자
    private Dictionary<int, Vector3> directorPosDict = new Dictionary<int, Vector3>();              // 카메라 관리자 이동 위치

    private FlowerObjectManager healFlowerManager;      // 회복초 관리자
    private FlowerObjectManager explosionFlowerManager; // 폭발초 관리자

    private Stage stage = Stage.WAIT_STAGE;             // 현재 게임 상태
    private Define.CameraMode userSettingCamera;        // 사용자 지정 카메라

    private int roundNumber;                            // 라운드

    // 타이머 변수 _230906 배경택
    private float baseTime;                             // 현재 게임 진행 시간



    // 카메라 변수_230906 배경택
    public Camera cameraWaitArea;
    public Camera cameraBattleArea1;
    public Camera cameraBattleArea2;
        
    // 자기장 _230906 배경택
    public GameObject magneticField1;
    public GameObject magneticField2;

    // 팀 체력 변수 _230906 배경택
    private int team1HP;
    private int team2HP;
    private int team3HP;
    private int team4HP;
    public bool isTeam1Win;
    public bool isTeam2Win;
    public bool isTeam3Win;
    public bool isTeam4Win;

    // 라운드 별 패배시 데미지 _230907 배경택
    private int teamDamage;
    private int[] teamRoundDamages =
    {
        0,2,2,2,4,4,4,6,6,6,8,8,8,10,10,10
    };


    // 캐릭터 체력 변수 _230906 배경택
    private float CharactorHP = 100f;

    // 라운드 종료 _230906 배경택
    private bool isRoundOver;
    //private 

    // 게임종료, 한 팀만 살아남았을 경우 _230906 배경택
    private bool isGameOver;

    // 라운드에 맞춰서 대기실 모드 결정(인스펙터창에서 변경 가능) _230906 배경택
    public WaitStageMode[] waitStageModes = {
        WaitStageMode.BASIC_MODE,
        WaitStageMode.MONEY_1000_MODE,
        WaitStageMode.CARD_MODE,
        WaitStageMode.MONEY_1000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.CARD_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.CARD_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.MONEY_3000_MODE,
        WaitStageMode.CARD_MODE,
        WaitStageMode.MONEY_3000_MODE
    };


    // 자기장 움직임 변수 _230907 배경택
    private int maneticCount = 0;
    private Vector3 MoveTomagneticPos1;
    private Vector3 MoveTomagneticPos2;
    private bool isArriveMagnetic;

    #region 게임 스테이지 및 모드 _230906 배경택

    public enum WaitStageMode
    {
        BASIC_MODE,
        MONEY_1000_MODE,
        MONEY_3000_MODE,
        CARD_MODE
    }
    private WaitStageMode waitStageMode;
    private enum BattleStageMode
    {
        READY_MODE,
        FIGHT_MODE,
        MANETIC_FIGHT_MODE

    }
    private BattleStageMode battleStageMode;
    #endregion

    // 캐릭터 리스폰 포인트를 위한 변수


    public GameObject[] waitAreaResPoint;
    public GameObject[] battle1ResPoint;
    public GameObject[] battle2ResPoint;

    // 게임오브젝트 생성 및 삭제를 위한 변수 _230907 배경택
    public GameObject healFlower; // 체력꽃 프리펩
    public GameObject jumpFlower; // 점프꽃 프리펩
    public GameObject[] healFlowerPoints; // 체력꽃 생성 위치
    public GameObject[] jumpFlowerPoints; // 점프꽃 생성 위치
    //private List<GameObject> healFlowers; // 체력꽃 저장
    //private List<GameObject> jumpFlowers; // 점프꽃 저장

    // 게임 플레이어
    public GameObject playerPrefab;
    private GameObject myPlayer;

    #region 상수

    private const int TOTAL_ARENA_TEAM = 4;                 // 아레나 참여팀 수
    private const float WAIT_AREA_TIME = 1f; //40f          // 대기 시간
    private const float BATTLE_READY_TIME = 1f; //5f        // 배틀 시작 직전 시간
    private const float START_MAGNETIC_TIME = 1f; //30f     // 자기장 생성 되기 전의 시간
    private const float MAGNETIC_CYCLE = 5f; //5f           // 자기장 줄어드는 사이클

    #endregion

    public void Init()
    {
        director = GameObject.Find("Director");
        directorPosDict.Add(-1, new Vector3(0, 0, -94));
        directorPosDict.Add(0, Vector3.zero);
        directorPosDict.Add(1, new Vector3(130, 0, 0));

        // 게임시작시 스테이지 대기모드로 초기화 _230906 배경택
        stage = Stage.WAIT_STAGE;
        roundNumber = 0;

        // 대기존 스폰 포인트 추가
        Transform waitAreaZone = GameObject.Find("WaitAreaZone").transform;
        for(int i = 1; i <= TOTAL_ARENA_TEAM; i++)
        {
            GameObject waitArea = waitAreaZone.transform.GetChild(i - 1).gameObject;
            Transform pointParent = waitArea.transform.Find("SpawnPoints");
            Transform[] spawnPoints = { pointParent.GetChild(0), pointParent.GetChild(1) };
            waitAreaSpawnPointDict.Add(i, spawnPoints);
        }

        // 배틀존 스폰 포인트 추가
        Transform battleAreaZone = GameObject.Find("BattleAreaZone").transform;
        for(int i = 1; i <= TOTAL_ARENA_TEAM; i+=2)
        {
            int index;
            if (i == 1) index = 0;
            else index = 1;

            GameObject battleArea = battleAreaZone.transform.GetChild(index).gameObject;
            Transform pointParent = battleArea.transform.Find("RespawnPoints");

            Transform[] spawnRedPoints = { pointParent.GetChild(0).GetChild(0), pointParent.GetChild(0).GetChild(1) };
            battleAreaASpawnPointDict.Add(i, spawnRedPoints);

            Transform[] spawnBluePoints = { pointParent.GetChild(1).GetChild(0), pointParent.GetChild(1).GetChild(1) };
            battleAreaASpawnPointDict.Add(i + 1, spawnBluePoints);
        }

        // 아레나 참여팀 초기화 진행
        for (int i = 1; i <= TOTAL_ARENA_TEAM; i++)
        {
            TeamController arenaTeam = new TeamController();
            arenaTeam.Init(i);                  // 팀 관련 초기화 진행

            // Test: 대기존 스폰 포인트로 세팅
            arenaTeam.SetSpawnPoint(waitAreaSpawnPointDict[i]);

            // Test: 임시로 피오라 캐릭터 생성 후 팀에 추가
            arenaTeam.SettingPlayer(0, Managers.Resource.Instantiate("Unit/Player_Piora", waitAreaSpawnPointDict[i][0]).GetComponent<Unit>());
            arenaTeam.SettingPlayer(1, Managers.Resource.Instantiate("Unit/Player_Piora", waitAreaSpawnPointDict[i][1]).GetComponent<Unit>());

            // Test: 스폰 포인트로 이동
            arenaTeam.SpawnPlayers();

            totalTeamDict.Add(i, arenaTeam);    // 참여팀에 합류
        }

        // 회복초 관리자 초기화
        //healFlowerManager = new FlowerObjectManager();
        //healFlowerManager.Init("HealFlowerSpawnPoint");

        //// 폭발초 관리자 초기화
        //explosionFlowerManager = new FlowerObjectManager();
        //explosionFlowerManager.Init("ExplosionFlowerSpawnPoint");

        //#region 변수 초기화
        ////게임시작시 스테이지 대기모드로 초기화 _230906 배경택
        ////stage = Stage.WAIT_STAGE;
        ////roundNumber = 0;
        //waitStageMode = waitStageModes[roundNumber];
        //battleStageMode = BattleStageMode.READY_MODE;

        ////게임 시작시 bool값 초기화 _230906 배경택
        //isRoundOver = false;
        //isGameOver = false;

        //게임 오브젝트를 담는 리스트 초기화 _230907 배경택
        //healFlowers = new List<GameObject>();
        //jumpFlowers = new List<GameObject>();

        //게임 시작시 팀 체력 초기화
        //team1HP = TEAM_HP_20;
        //team2HP = TEAM_HP_20;
        //team3HP = TEAM_HP_20;
        //team4HP = TEAM_HP_20;

        //myPlayer = Managers.Resource.Instantiate("Unit/Player_Piora", waitAreaResPoint[0].transform.position, Quaternion.identity);
        //#endregion
    }

    public void OnUpdate()
    {
        GameFlow(); //게임 흐름 함수(시간에 따라 제어) _230906 배경택

        if (!isArriveMagnetic) // 자기장이 전부 줄어들었는가 _230907 배경택
        {
            //MoveUpMagneticField(); // 자기장 움직임 _230907 배경택
        }
    }

    /// <summary>
    /// 타이머 및 게임 스테이지 흐름 제어
    /// 배경택 230906
    /// </summary>
    private void GameFlow()
    {
        baseTime += Time.deltaTime; 

        switch (stage)
        {
            case Stage.WAIT_STAGE: UpdateWait(); break;
            //case Stage.BATTLE_STAGE: UpdateBattle(); break;
        }
    }

    #region 업데이트 함수

    private void UpdateWait()
    {
        Debug.Log("Test: 대기실 입장");

        if(baseTime >= WAIT_AREA_TIME)
        {   // 대기실 시간이 다 지나면 배틀 스테이지로 이동
            baseTime = 0f;
            stage = Stage.BATTLE_STAGE;
            Debug.Log("Test: 경기장 이동");

            // 랜덤 한 위치로 카메라 이동
            MoveMap(Random.Range(0, 2));

            // 각 팀들이 지정된 위치로 위치 이동
            for (int i = 1; i <= totalTeamDict.Count; i++)
            {
                TeamController arenaTeam = totalTeamDict[i];

                // Test: 대기존 스폰 포인트로 세팅
                arenaTeam.SetSpawnPoint(battleAreaASpawnPointDict[i]);

                // Test: 팀원들 부모 오브젝트 재정의
                for(int j = 0; j < arenaTeam.Players.Count; j++)
                {
                    arenaTeam.Players[j].transform.parent = battleAreaASpawnPointDict[i][j];
                }

                // Test: 스폰 포인트로 이동
                arenaTeam.SpawnPlayers();
            }
            return;
        }

        //switch (waitStageMode) // 라운드 수에 따라 모드에 맞게 실행
        //{
        //    case WaitStageMode.BASIC_MODE:
        //        Debug.Log("대기실_기본");

        //        break;

        //    case WaitStageMode.MONEY_1000_MODE:
        //        Debug.Log("대기실_1000_돈");

        //        break;

        //    case WaitStageMode.MONEY_3000_MODE:
        //        Debug.Log("대기실_3000_돈");

        //        break;

        //    case WaitStageMode.CARD_MODE:
        //        Debug.Log("대기실_카드");

        //        break;
        //}
    }

    public void UpdateBattle()
    {
        if (isRoundOver) // 라운드가 종료되었다면 라운드 수를 높이고 대기실로 이동
        {
            baseTime = 0f;
            RoundOver(); //라운드 종료시 실행
            return;
        }

        switch (battleStageMode) // 배틀스테이지 모드 시간에 따라 수행
        {
            case BattleStageMode.READY_MODE: UpdateBattle_Ready(); break;
            case BattleStageMode.FIGHT_MODE: UpdateBattle_Fight(); break;
            case BattleStageMode.MANETIC_FIGHT_MODE: UpdateBattle_Magnetic(); break;
        }
    }

    /// <summary>
    /// 전투 대기 업데이트 함수
    /// 김민섭_230919
    /// </summary>
    private void UpdateBattle_Ready()
    {
        Debug.Log("전투 대기 중입니다.");

        if(baseTime >= BATTLE_READY_TIME)
        {
            baseTime = 0f;
            battleStageMode = BattleStageMode.FIGHT_MODE;

            Debug.Log("전투가 시작됩니다.");

            // TODO: 각 flowerManager에서 폭발초 및 회복초가 생성되는 로직이 동작하도록 명령

            return;
        }
    }

    /// <summary>
    /// 전투 업데이트 함수
    /// 김민섭_230919
    /// </summary>
    private void UpdateBattle_Fight()
    {
        Debug.Log("전투 중입니다.");

        if (baseTime >= START_MAGNETIC_TIME)
        {
            baseTime = 0f;
            battleStageMode = BattleStageMode.MANETIC_FIGHT_MODE;

            Debug.Log("자기장이 생성됩니다.");

            // TODO: 자기장 생성 로직 추가
            //StartMagneticField();

            return;
        }
    }

    /// <summary>
    /// 자기장 업데이트 함수
    /// 김민섭_230919
    /// </summary>
    private void UpdateBattle_Magnetic()
    {
        Debug.Log("자기장이 있습니다.");

        if (baseTime >= MAGNETIC_CYCLE) // 자기장 주기에 맞춰서 실행
        {
            baseTime = 0f;

            if (maneticCount < 3) // Test : 3번 자기장 위로 움직이게 실행
            {
                //MangeticCycle(); // Cycle마다 자기장의 다음 움직일 위치 계산
            }
            else if (maneticCount == 3) // Test : 자기장 일정높이 도달시 라운드종료 할 수 있도록
            {
                //EndMagneticField(); // 자기장 모드 종료시 실행
            }
        }
    }

    #endregion

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
        maneticCount = 0;
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
    private void MangeticCycle() // 자기장 위쪽방향의 위치값 저장
    {
        Debug.Log("자기장 +1");
        maneticCount += 1;
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
        {
            //healFlowers.Add(Instantiate(healFlower, healPoint.transform.position, Quaternion.identity));
        }
    }
    private void CreateJumpFlower() // 터지면 점프하게 하는 꽃 생성 _ 230907 배경택
    {
        //foreach (GameObject jumpPoint in jumpFlowerPoints)
        {
            //jumpFlowers.Add(Instantiate(jumpFlower, jumpPoint.transform.position, Quaternion.identity));
        }
    }

    private void DestroyGameObject() // 게임오브젝트 삭제 _ 230907 배경택
    {
        DestroyHealFlower();
        DestroyJumpFlower();
    }
    private void DestroyHealFlower() // 체력을 채워주는 꽃 파괴 _ 230907 배경택
    {
        //foreach(GameObject healFlower in healFlowers)
        {
            //Destroy(healFlower);
        }
    }
    private void DestroyJumpFlower() // 점프를 하게만드는 꽃 파괴 _ 230907 배경택
    {
        //foreach(GameObject jumpFlower in jumpFlowers)
        {
            //Destroy(jumpFlower);
        }
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
        teamDamage = teamRoundDamages[roundNumber]; //라운드별 체력감소량 적용

        stage = Stage.WAIT_STAGE;
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
    public void MoveToBattleArea1()
    {
        //cameraWaitArea.gameObject.SetActive(false);
        //cameraBattleArea2.gameObject.SetActive(false);
        //cameraBattleArea1.gameObject.SetActive(true);
        ////TODO 캐릭터 이동
    }

    /// <summary>
    /// 카메라 관리자 이동 함수
    /// 김민섭_230919
    /// </summary>
    private void MoveMap(int index) => director.transform.position = directorPosDict[index];

    public void MoveToBattlArea2()
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

    #region 라운드 및 최종 결과 계산 _230907 배경택
    // 라운드 결과 계산 _230907 배경택
    private void RoundResult()
    {
        if (!isTeam1Win)
        {
            team1HP -= teamDamage;
            isTeam1Win = true;
        }

        if (!isTeam2Win)
        {
            team2HP -= teamDamage;
            isTeam2Win = true;
        }

        if (!isTeam3Win)
        {
            team3HP -= teamDamage;
            isTeam3Win = true;
        }

        if (!isTeam4Win)
        {
            team4HP -= teamDamage;
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
