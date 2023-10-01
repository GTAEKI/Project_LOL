using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class CreatePlayer : MonoBehaviourPun
{
    public int GetMyIndex() => Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);       // 인덱스 가져오기

    /// <summary>
    /// 플레이어 인덱스에 맞춰서 캐릭터 생성
    /// 김민섭_230927
    /// </summary>
    public void Create(PlayerController player, Transform resPoint)
    {
        // TODO: 서버에서 받아온 플레이어 유닛 생성
        string playerUnit = "";
        switch (PlayerData.Instance.myCharactor)
        {
            case Define.UnitName.Rumble: playerUnit = "Rumble_Photon"; break;
            case Define.UnitName.Ashe: playerUnit = "Ashe_Photon"; break;
            case Define.UnitName.Caityln: playerUnit = "Caityln_Photon"; break;
            case Define.UnitName.Gragas: playerUnit = "Gragas_Photon"; break;
            case Define.UnitName.Yasuo: playerUnit = "Yasuo_Photon"; break;
        }

        Managers.UI.ShowSceneUI<UI_UnitBottomLayer>();      // 하단 UI 생성

        GameObject myCharacter = PhotonNetwork.Instantiate(playerUnit, resPoint.position, Quaternion.identity);
        UI_UnitBottomLayer bottomLayer = Managers.UI.GetScene<UI_UnitBottomLayer>();

        switch (PlayerData.Instance.myCharactor)
        {
            case Define.UnitName.Rumble: player.SettingUnit(myCharacter.GetComponent<Rumble>()); break;
            case Define.UnitName.Ashe: player.SettingUnit(myCharacter.GetComponent<Ashe>()); break;
            //case Define.UnitName.Caityln: players[i].SettingUnit(myCharacter.GetComponent<cait>()); break;
            // case Define.UnitName.Gragas: players[i].SettingUnit(myCharacter.GetComponent<gragas>()); ; break;
            case Define.UnitName.Yasuo: player.SettingUnit(myCharacter.GetComponent<Yasuo>()); ; break;
        }
        bottomLayer?.SetTarget(PlayerData.Instance.myCharactor);
    }

    public void MovePoint(PlayerController player, Transform resPoint)
    {
        //if(photonView.IsMine)
        {
            player.PlayerUnit.transform.position = resPoint.position;
            player.PlayerUnit.transform.SetParent(resPoint);
        }
    }
}
