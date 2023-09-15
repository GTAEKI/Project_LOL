using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템(비전 탐지기) 설치 가능 사정거리
/// </summary>

public class SB_FirstArcaneRange : MonoBehaviour
{
    GameObject firstRange; // 비전탐지기 설치 가능 범위 오브젝트
    GameObject secondRange; // 비전탐지기 효과 범위 오브젝트
    SpriteRenderer firstSprite; // 첫번째 감지 범위
    SpriteRenderer secondSprite; // 두번째 감지 범위

    bool firstLimit = false;

    SB_ArcaneCoolTime arcaneCoolTime = new SB_ArcaneCoolTime(); // 첫 쿨타임 30초

    // Start is called before the first frame update
    void Start()
    {
        firstRange = transform.GetChild(0).gameObject;
        secondRange = firstRange.transform.GetChild(0).gameObject;
        firstSprite = firstRange.GetComponent<SpriteRenderer>();
        secondSprite = secondRange.GetComponent<SpriteRenderer>();
        arcaneCoolTime = GameObject.Find("Cool Time").transform.GetComponent<SB_ArcaneCoolTime>();

        firstSprite.enabled = false;
        secondSprite.enabled = false;

        //arcaneCoolTime.StartCoolTime();
        //StartCoroutine(StartLimit());
    }

    /// <summary>
    /// 규칙: 턴 시작 30초 동안 비전 탐지기 사용 금지
    /// </summary>
    /// <returns></returns>
    IEnumerator StartLimit()
    {
        yield return new WaitForSeconds(30);
        firstLimit = true;
    }

    // Update is called once per frame
    void Update()
    {
        firstLimit = false;

        if (!firstLimit) // 첫 제한 30초 후
        {
            if (!SB_UseArcaneSweeper.arcaneUsed && Input.GetKeyDown(KeyCode.Alpha4)) // 범위 활성화
            {
                firstSprite.enabled = true;
                secondSprite.enabled = true;
            }
        }
    }

    /// <summary>
    /// 비전 탐지기 작동 시 쿨타임 끝날 때까지 작동 중지
    /// </summary>
    public void RunArcane()
    {
        firstSprite.enabled = false;
        secondSprite.enabled = false;
    }
}
