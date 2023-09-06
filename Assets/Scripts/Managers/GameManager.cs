using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 게임매니저
/// 배경택_230906
/// </summary>
public class GameManager : MonoBehaviour
{
    // 게임매니저 싱글톤_230906 배경택
    public static GameManager instance;

    // 카메라_230906 배경택
    public Camera cameraSafeArea;
    public Camera cameraBattle1;
    public Camera cameraBattle2;

    // 타이머 _230906 배경택

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
    }

    private void Update()
    {
        
    }

    //TODO 타이머
    //TODO 자기장
    //TODO 오브젝트 생성
    //TODO 게임스테이지 흐름제어
    //TODO 카메라 제어
    //TODO 승패 판정
    //TODO 팀 체력 제어

    #region 카메라 이동
    public void MoveToBattle1()
    {
        cameraSafeArea.gameObject.SetActive(false);
        cameraBattle2.gameObject.SetActive(false); ;
        cameraBattle1.gameObject.SetActive(true);
    }

    public void MoveToBattle2()
    {
        cameraSafeArea.gameObject.SetActive(false);
        cameraBattle1.gameObject.SetActive(false);
        cameraBattle2.gameObject.SetActive(true);
    }

    public void ReturnSafeZone()
    {
        cameraBattle1.gameObject.SetActive(false);
        cameraBattle2.gameObject.SetActive(false);
        cameraSafeArea.gameObject.SetActive(true);
    }
    #endregion
}
