//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using System;

//public class SB_ArcaneCoolTime : MonoBehaviour
//{
//    TMP_Text coolTime;

//    // Start is called before the first frame update
//    void Start()
//    {
//        coolTime = transform.GetComponent<TMP_Text>();
//        Debug.Assert(coolTime != null);

//        SB_UseArcaneSweeper.useArcane += new EventHandler(StartCoolTime);
//    }

//    public void StartCoolTime(object sender, EventArgs e)
//    {
//        StartCoroutine(CoolTime());
//    }

//    /// <summary>
//    /// 비전 탐지기 사용 후 쿨타임 시작
//    /// </summary>
//    /// <returns></returns>
//    private IEnumerator CoolTime()
//    {
//        int waitTime = 1;
//        int leftTime = 30;

//        while (leftTime > 0)
//        {
//            yield return new WaitForSeconds(waitTime);
//            leftTime -= waitTime;

//            coolTime.text = leftTime.ToString();
//        }

//        coolTime.text = string.Empty;
//        SB_UseArcaneSweeper.arcaneUsed = false;

//        StopCoroutine(CoolTime());
//    }
//}
