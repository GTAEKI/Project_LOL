using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


/// <summary>
/// 게임매니저
/// 배경택_230906
/// </summary>
public class GameManager : MonoBehaviour
{
    // 게임매니저 싱글톤_230906 배경택
    public static GameManager instance;

    // 카메라 변수_230906 배경택
    public Camera cameraWaitArea;
    public Camera cameraBattleArea1;
    public Camera cameraBattleArea2;

    // 자기장 _230906 배경택
    public GameObject magneticField1;
    public GameObject magneticField2;

    // 타이머 변수 _230906 배경택
    private float baseTime;
    private const float WAIT_AREA_TIME = 1f; //40f
    private const float BATTLE_READY_TIME = 1f; //5f
    private const float START_MAGNETIC_TIME = 1f; //30f
    private const float MAGNETIC_CYCLE = 1f; //5f

    // 팀 체력 변수 _230906 배경택
    private const int TEAM_HP = 20;

    // 캐릭터 체력 변수 _230906 배경택
    private float CharactorHP = 100f;

    // 라운드 종료 _230906 배경택
    private bool isRoundOver;

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
    private int roundNumber;


    //Test용 y축
    private int SphereY = 0;


    #region 게임 스테이지 및 모드
    // 게임 스테이지 및 모드 _230906 배경택
    private enum Stage
    {
        WAIT_STAGE,
        BATTLE_STAGE
    }
    private Stage stage;
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


    //public delegate void 


    private void Awake()
    {
        //게임매니저 싱글톤_230906 배경택
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        #region 변수 초기화
        //게임시작시 스테이지 대기모드로 초기화 _230906 배경택
        stage = Stage.WAIT_STAGE;
        roundNumber = 0;
        waitStageMode = waitStageModes[roundNumber];
        battleStageMode = BattleStageMode.READY_MODE;

        //게임 시작시 bool값 초기화 _230906 배경택
        isRoundOver = false;
        isGameOver = false;
        #endregion
    }

    private void Update()
    {
        GameFlow(); //게임 흐름 함수 _ 시간에 따른 제어
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
            case Stage.WAIT_STAGE:
                Debug.Log("대기실");

                if(baseTime >= WAIT_AREA_TIME) // 대기실 시간이 다 지나면 배틀스테이지로 이동
                {
                    baseTime = 0f;
                    stage = Stage.BATTLE_STAGE;
                    isRoundOver = false;
                    Debug.Log("경기장 이동");

                    // 랜덤 한 위치로 카메라 이동
                    int randomBattleArea = Random.Range(0, 2);
                    if (randomBattleArea == 1)
                    {
                        MoveToBattleArea1();
                    }
                    else
                    {
                        MoveToBattlArea2();
                    }

                    break;
                }

                switch (waitStageMode) // 라운드 수에 따라 모드에 맞게 실행
                {
                    case WaitStageMode.BASIC_MODE:
                        Debug.Log("대기실_기본");

                        break;

                    case WaitStageMode.MONEY_1000_MODE:
                        Debug.Log("대기실_1000_돈");

                        break;

                    case WaitStageMode.MONEY_3000_MODE:
                        Debug.Log("대기실_3000_돈");

                        break;

                    case WaitStageMode.CARD_MODE:
                        Debug.Log("대기실_카드");

                        break;
                }
                break;

            case Stage.BATTLE_STAGE:

                if (isRoundOver) // 라운드가 종료되었다면 라운드 수를 높이고 대기실로 이동
                {
                    baseTime = 0f;
                    roundNumber += 1;
                    waitStageMode = waitStageModes[roundNumber];
                    stage = Stage.WAIT_STAGE;
                    ReturnWaitArea();
                    Debug.Log("대기실로 이동");
                    break;
                }

                switch (battleStageMode) // 배틀스테이지 모드 시간에 따라 수행
                {
                    case BattleStageMode.READY_MODE:
                        if(baseTime >= BATTLE_READY_TIME)
                        {
                            baseTime = 0f;
                            battleStageMode = BattleStageMode.FIGHT_MODE;
                        }
                        Debug.Log("준비모드");
                        break;

                    case BattleStageMode.FIGHT_MODE:
                        if (baseTime >= START_MAGNETIC_TIME)
                        {
                            baseTime = 0f;
                            battleStageMode = BattleStageMode.MANETIC_FIGHT_MODE;
                        }
                        Debug.Log("전투모드");
                        break;

                    case BattleStageMode.MANETIC_FIGHT_MODE: //자기장 시작됨
                        if (baseTime >= MAGNETIC_CYCLE) // 자기장 주기에 맞춰서 실행
                        {
                            baseTime = 0f;

                            //test
                            if(SphereY < 3)
                            {
                                SphereY += 1;
                                Debug.Log("자기장 +1");
                            }
                            else if(SphereY == 3) // Test_ 자기장 일정높이 도달시 라운드종료 할 수 있도록
                            {
                                isRoundOver = true;
                                SphereY = 0;
                            }
                        }
                        Debug.Log("자기장 모드");
                        break;
                }
                break;
        }
    }

    //TODO 자기장

    //TODO 오브젝트 생성

    //TODO 승패 판정

    //TODO 팀 체력 제어

    #region 맵 지역 이동
    /// <summary>
    /// 카메라 이동
    /// TODO 캐릭터 이동
    /// 배경택 230906
    /// </summary>
    public void MoveToBattleArea1()
    {
        cameraWaitArea.gameObject.SetActive(false);
        cameraBattleArea2.gameObject.SetActive(false);
        cameraBattleArea1.gameObject.SetActive(true);
    }

    public void MoveToBattlArea2()
    {
        cameraWaitArea.gameObject.SetActive(false);
        cameraBattleArea1.gameObject.SetActive(false);
        cameraBattleArea2.gameObject.SetActive(true);
    }

    public void ReturnWaitArea()
    {
        cameraBattleArea1.gameObject.SetActive(false);
        cameraBattleArea2.gameObject.SetActive(false);
        cameraWaitArea.gameObject.SetActive(true);
    }
    #endregion
}
