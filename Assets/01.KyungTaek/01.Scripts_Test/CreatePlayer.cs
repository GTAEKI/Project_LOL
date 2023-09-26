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
    public PlayerInfo loadMyCharactor;

    public void Start()
    {
        loadMyCharactor = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();

        switch (loadMyCharactor.myCharactor)
        {
            case Define.UnitName.Rumble:
                GameObject rumblePrefab = Resources.Load<GameObject>("Unit/Rumble/Rumble_Photon");
                PhotonNetwork.Instantiate(rumblePrefab.name, respawnPoint.transform.position, Quaternion.identity);
                break;
            case Define.UnitName.Ashe:
                GameObject ashePrefab = Resources.Load<GameObject>("Unit/Ashe_Photon");
                PhotonNetwork.Instantiate("Ashe_Photon", respawnPoint.transform.position, Quaternion.identity);
                break;
            case Define.UnitName.Caityln:
                break;
            case Define.UnitName.Gragas:
                break;
            case Define.UnitName.Yasuo:
                break;
        }
    }
}
