using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController
{
    private Dictionary<int, Unit> players = new Dictionary<int, Unit>();        // 소속 플레이어
    private Transform[] spawnPoints;        // 스폰할 위치

    private int teamNumber;                 // 팀 번호
    private int currentHp;                  // 현재 체력
    
    #region 상수

    private const int TEAM_HP_MAX = 20;                  // 팀 최대 체력 _230906 배경택

    #endregion

    /// <summary>
    /// 팀 생성 시 초기화 진행 함수
    /// 김민섭_230919
    /// </summary>
    public void Init(int teamNumber)
    {
        this.teamNumber = teamNumber;
        currentHp = TEAM_HP_MAX;        // 최대 체력 세팅
    }

    /// <summary>
    /// 소속 플레이어 세팅 함수
    /// 김민섭_230919
    /// </summary>
    /// <param name="index">팀에 소속된 번호</param>
    /// <param name="player">팀에 참여할 플레이어</param>
    public void SettingPlayer(int index, Unit player) => players.Add(index, player);

    /// <summary>
    /// 소속 플레이어 스폰 함수
    /// 김민섭_230919
    /// </summary>
    public void SpawnPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
        }
    }

    /// <summary>
    /// 스폰 위치 세팅 함수
    /// 김민섭_230919
    /// </summary>
    /// <param name="spawnPoints">스폰 위치</param>
    public void SetSpawnPoint(Transform[] spawnPoints) => this.spawnPoints = spawnPoints;
}
