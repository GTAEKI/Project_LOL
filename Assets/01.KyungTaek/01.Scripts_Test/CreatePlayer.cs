using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;


public class CreatePlayer : MonoBehaviourPun
{
    public GameObject respawnPoint;

    public void Start()
    {
        Debug.Log("캐릭터 생성 ING");

        PhotonNetwork.Instantiate("Ashe_Photon", respawnPoint.transform.position, respawnPoint.transform.rotation);
        Debug.Log("캐릭터 생성완료");
    }
}
