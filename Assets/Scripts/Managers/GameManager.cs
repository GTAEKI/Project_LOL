using System.Collections.Generic;
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
        WAIT_STAGE, BATTLE_STAGE, RESULT_STAGE
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

    private CurrentStageMode currentStage;     // 스테이지 종류 (대기 / 전투)
    private WaitStageMode waitStage;           // 대기 스테이지 타입 (기본 / 1000원 지급 / 3000원 지급)
    private BattleStageMode battleStage;       // 전투 스테지이 타입 (대기 / 전투 / 자기장 전투)

    private Dictionary<string, PlayerController> originPlayerDict = new Dictionary<string, PlayerController>();     // 플레이어 정보 원본 데이터 변수
    private List<PlayerController> players = new List<PlayerController>();                                          // 현재 플레이가 가능한 플레이어 정보 데이터 변수
    private MagneticFieldController magneticField;                                                                  // 자기장

    private Transform[] waitAreaResPoint;       // 웨이팅존 스폰 포인트
    private Transform[] battleResPoint;         // 배틀존 스폰 포인트
    private GameObject director;                // 카메라 관리자

    private int roundNumber;            // 현재 라운드 번호
    private float baseTimer;            // 게임 타이머

    // 라운드 별 패배시 데미지 _230907 배경택
    private int[] teamRoundDamages =
    {
        0,2,2,2,4,4,4,6,6,6,8,8,8,10,10,10
    };

    #region 프로퍼티

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

            switch (currentStage)
            {
                case CurrentStageMode.WAIT_STAGE: OnWaitStage(); break;
                case CurrentStageMode.BATTLE_STAGE: OnBattleStage(); break;
                case CurrentStageMode.RESULT_STAGE: OnResultStage(); break;
            }
        }
    }

    #region 현재 스테이지 프로퍼티 함수

    /// <summary>
    /// 웨이팅존 전환시 발동 함수
    /// 김민섭_230926
    /// </summary>
    private void OnWaitStage()
    {
        director.transform.position = new Vector3(0f, 0f, WAITZONE_POSITION_Z);     // 카메라 이동

        // 살아남은 플레이어 웨이팅존으로 이동
        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerUnit.transform.position = waitAreaResPoint[i].position;
            players[i].PlayerUnit.transform.SetParent(waitAreaResPoint[i]);
        }

        // 현재 라운드에 맞춰서 웨이팅 이벤트 실행
        WaitStage = waitStageModes[roundNumber];
    }

    /// <summary>
    /// 배틀존 전환시 발동 함수
    /// 김민섭_230926
    /// </summary>
    private void OnBattleStage()
    {
        director.transform.position = new Vector3(0f, 0f, BATTLEZONE_POSITION_Z);       // 카메라 이동

        // 살아남은 플레이어 배틀존으로 이동
        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerUnit.transform.position = battleResPoint[i].position;
            players[i].PlayerUnit.transform.SetParent(battleResPoint[i]);
        }
    }

    private void OnResultStage()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].PlayerUnit.CurrentUnitStat.Hp > 0)
            {   // 승리한 플레이어
                Debug.Log($"{players[i].PlayerUnit.gameObject.name} 이 승리했습니다.");
            }
            else
            {   // 패배한 플레이어
                Debug.Log($"{players[i].PlayerUnit.gameObject.name} 이 패배했습니다.");
                players[i].OnDamaged(teamRoundDamages[roundNumber]);            // 라운드 별 데미지를 입힘 (플레이어)
            }
        }
    }

    #endregion

    /// <summary>
    /// 대기 상태 이벤트 종류 프로퍼티
    /// 김민섭_230926
    /// </summary>
    public WaitStageMode WaitStage
    {
        get => waitStage;
        set
        {
            waitStage = value;

            Debug.Log($"{waitStage} 이벤트 실행");

            switch (waitStage)
            {
                case WaitStageMode.MONEY_1000_MODE: OnGetMoney(1000); break;
                case WaitStageMode.MONEY_3000_MODE: OnGetMoney(3000); break;
            }
        }
    }

    #region 대기 상태 프로퍼티 함수

    /// <summary>
    /// 골드 지급 함수
    /// 김민섭_230926
    /// </summary>
    /// <param name="value"></param>
    private void OnGetMoney(int value)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].OnChangeGold(value);
        }
    }

    #endregion

    /// <summary>
    /// 전투 상태 이벤트 종류 프로퍼티
    /// 김민섭_230926
    /// </summary>
    public BattleStageMode BattleStage
    {
        get => battleStage;
        set
        {
            battleStage = value;

            Debug.Log($"{battleStage} 이벤트 실행");

            switch (battleStage)
            {
                case BattleStageMode.READY_MODE: OnReady(); break;
                case BattleStageMode.MANETIC_FIGHT_MODE: OnManetic(); break;
            }
        }
    }

    private void OnReady()
    {
        // TODO: 게임에서 사용할 오브젝트 생성
    }

    /// <summary>
    /// 자기장 이벤트 실행 함수
    /// 김민섭_230926
    /// </summary>
    private void OnManetic()
    {
        magneticField.Generate();
    }

    #endregion

    #region 상수

    private const int PLAYER_COUNT = 4;                 // 플레이어 수
    private const int MAGNETIC_STACK = 5;               // 자기장 최대 스택
    private const float WAIT_AREA_TIME = 5f;            // 40f
    private const float BATTLE_READY_TIME = 1f;         // 5f
    private const float START_MAGNETIC_TIME = 10f;       // 30f
    //private const float MAGNETIC_CYCLE = 20f;            // 20f
    private const float WAITZONE_POSITION_Z = -93f;     // 대기존 카메라 z좌표       
    private const float BATTLEZONE_POSITION_Z = 0f;     // 배틀존 카메라 z좌표

    #endregion

    public void Init()
    {
        SettingSpawnPoint();

        // TODO: 서버에서 받아온 플레이어 정보를 가져와서 세팅하도록 수정해야함
        SettingPlayer();

        // 자기장 초기화
        magneticField = GameObject.Find("MagneticField").GetComponent<MagneticFieldController>();
        magneticField.Clear();

        director = GameObject.Find("Director");

        roundNumber = 0;
        CurrentStage = CurrentStageMode.WAIT_STAGE;
    }

    /// <summary>
    /// 스폰 포인트 세팅 함수
    /// 김민섭_230926
    /// </summary>
    private void SettingSpawnPoint()
    {
        // 웨이팅존 세팅
        waitAreaResPoint = new Transform[PLAYER_COUNT];

        GameObject waitAreaZone = GameObject.Find("WaitAreaZone");
        for (int i = 0; i < waitAreaZone.transform.childCount; i++)
        {
            Transform spawnPoint = waitAreaZone.transform.GetChild(i).Find("SpawnPoint");
            waitAreaResPoint[i] = spawnPoint;
        }

        // 배틀존 세팅
        battleResPoint = new Transform[PLAYER_COUNT];

        GameObject battleAreaZone = GameObject.Find("BattleAreaZone/RespawnPoints");
        for (int i = 0; i < battleAreaZone.transform.childCount; i++)
        {
            Transform spawnPoint = battleAreaZone.transform.GetChild(i);
            battleResPoint[i] = spawnPoint;
        }
    }

    /// <summary>
    /// 게임에 참여하는 플레이어 세팅 함수
    /// 김민섭_230926
    /// </summary>
    private void SettingPlayer()
    {
        for (int i = 0; i < PLAYER_COUNT; i++)
        {
            PlayerController player = new PlayerController();
            originPlayerDict.Add(i.ToString(), player);
            players.Add(player);
        }

        // TODO: 서버에서 받아온 플레이어 유닛 생성
        for (int i = 0; i < PLAYER_COUNT; i++)
        {
            // TEST: 임시로 야스오 4마리 생성
            GameObject player = Managers.Resource.Instantiate("Unit/Yasuo/Player_Yasuo", waitAreaResPoint[i]);
            players[i].SettingUnit(player.GetComponent<Yasuo>());
        }
    }

    public void OnUpdate()
    {
        baseTimer += Time.deltaTime;

        switch (currentStage)
        {
            case CurrentStageMode.WAIT_STAGE: UpdateWaitStage(); break;
            case CurrentStageMode.BATTLE_STAGE: UpdateBattleStage(); break;
        }
    }

    #region 업데이트 함수

    /// <summary>
    /// 대기존 업데이트 함수
    /// 김민섭_230925
    /// </summary>
    private void UpdateWaitStage()
    {
        if (baseTimer >= WAIT_AREA_TIME)
        {   // 대기실 시간이 다 지나면 배틀스테이지로 이동한다.
            baseTimer = 0f;

            CurrentStage = CurrentStageMode.BATTLE_STAGE;
            return;
        }
    }

    /// <summary>
    /// 배틀존 업데이트 함수
    /// 김민섭_230925
    /// </summary>
    private void UpdateBattleStage()
    {
        // TODO: 라운드 종료 처리

        switch (BattleStage)
        {
            case BattleStageMode.READY_MODE: UpdateBattle_ReadyMode(); break;
            case BattleStageMode.FIGHT_MODE: UpdateBattle_FightMode(); break;
            case BattleStageMode.MANETIC_FIGHT_MODE: UpdateBattle_Manetic(); break;
        }
    }

    private void UpdateBattle_ReadyMode()
    {
        if (baseTimer >= BATTLE_READY_TIME)
        {   // 전투 대기 시간이 종료되면 실행
            baseTimer = 0f;
            BattleStage = BattleStageMode.FIGHT_MODE;

            Debug.Log("전투가 시작됩니다.");

            return;
        }
    }

    private void UpdateBattle_FightMode()
    {
        if (baseTimer >= START_MAGNETIC_TIME)
        {
            baseTimer = 0f;
            BattleStage = BattleStageMode.MANETIC_FIGHT_MODE;

            Debug.Log("자기장이 생성됩니다.");

            return;
        }
    }

    private void UpdateBattle_Manetic()
    {
        if (magneticField.MagneticCount >= MAGNETIC_STACK)
        {
            magneticField.Clear();

            baseTimer = 0f;

            roundNumber++;
            CurrentStage = CurrentStageMode.WAIT_STAGE;
            return;
        }
    }

    #endregion
}