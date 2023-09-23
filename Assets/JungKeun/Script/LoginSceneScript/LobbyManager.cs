using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;
using Unity.VisualScripting.ReorderableList.Internal;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Button ReturnButton;
    public PhotonView PV;

    //ServerMessage, explanationMeesage
    public GameObject ServerMessage , explanationMessage;
    public TMP_Text ServerMessageconnectionInfoText;
    public TMP_Text ServerMessageexplanation;
    public TMP_Text[] Buttonexplanation;

    //Login
    public GameObject Login;
    public TMP_InputField LoginNickName;
    public Button LoginConnectServer;

    //Lobby
    public GameObject Lobby;
    public Button LobbyNextButton;
    public Button LobbyBeforeButton;
    public TMP_Text LobbyRoomandPeople;
    public Button[] LobbyRooms;
    public TMP_Text[] LobbyRoomname;
    public TMP_Text[] LobbyRoomnumber;
    public TMP_Text[] LobbyRoomHost;

    //JoinRoom
    public GameObject JoinRoom;
    public TMP_Text JoinRoomName;
    public Button JoinRoomMakeRoom;
    public Button JoinRoomQuickStart;
    public Button JoinRoomGotoLobby;
    public Button JoinRoomGotoRoom;
    public TMP_Text JoinRoomPressRoomName;
    public GameObject JoinRoomPressRoomButton;

    //MakeRoom
    public GameObject MakeRoom;
    public TMP_InputField MakeRoomRoomName;
    public Button MakeRoomMakeaRoom;
    public Button MakeRoomBacktoLobby;

    //Room
    public TMP_Text RoomInfoText;
    public TMP_Text RoomInfoText2;
    public GameObject Room;
    public TMP_Text[] RoomPlayerNickName;
    public TMP_Text[] RoomChattings;
    public Image[] RoomPlayerCharacterImg;
    public Image[] RoomCharaters;
    public TMP_InputField RoomChat;
    //public scrollview RoomScrollView;
    public Button RoomStart;
    public Button Roomback;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    //프로그램을 실행하자마자
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    //프로그램을 실행하면
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        Login.gameObject.SetActive(true);

        ServerMessageconnectionInfoText.text = "대기 중...";
        ServerMessage.gameObject.SetActive(true);
        explanationMessage.gameObject.SetActive(true);
        Login.gameObject.SetActive(true);
        Lobby.gameObject.SetActive(false);
        JoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(false);
    }

    private void Update()
    {
        LobbyRoomandPeople.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비대기인원 / " + PhotonNetwork.CountOfPlayers + "접속/" +"방 갯수" + PhotonNetwork.CountOfRooms;
        if (LoginNickName == null)
        {
            LoginConnectServer.interactable = false;
        }
        else if (LoginNickName.text.Length >= 1 && LoginNickName.text.Length < 6)
        {
            LoginConnectServer.interactable = true;
        }
    }

    //연결이 됨
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.IsConnected)  
        {
            ServerMessageconnectionInfoText.text = "서버에 접속...";
            PhotonNetwork.JoinLobby();
        }
        else
        {
            ServerMessageconnectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //마스터서버에 연결
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        ServerMessageconnectionInfoText.text = "온라인 : 마스터 서버와 연결됨";

        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        
        Login.gameObject.SetActive(false);
        Lobby.gameObject.SetActive(true);
        JoinRoom.gameObject.SetActive(true);
        PhotonNetwork.LocalPlayer.NickName = LoginNickName.text;
        JoinRoomName.text = LoginNickName.text + "님,환영합니다.";
        myList.Clear();
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    //연결이 끊김
    public override void OnDisconnected(DisconnectCause cause)
    {
        ServerMessageconnectionInfoText.text = "오프라인으로 전환완료";

        ServerMessageconnectionInfoText.text = "대기 중...";



        ServerMessage.gameObject.SetActive(true);
        explanationMessage.gameObject.SetActive(true);
        Login.gameObject.SetActive(true);
        Lobby.gameObject.SetActive(false);
        JoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(false);
    }



    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(MakeRoomRoomName.text == "" ? "Room" + Random.Range(0, 100) : MakeRoomRoomName.text, new RoomOptions { MaxPlayers = 2 });
        
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //방에 들어가기
    public override void OnJoinedRoom()
    {
        ServerMessageconnectionInfoText.text = "방 참가 성공";
        ServerMessage.gameObject.SetActive(false);
        explanationMessage.gameObject.SetActive(false);
        Lobby.gameObject.SetActive(false);
        JoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(true);
        base.OnJoinedRoom();


        RoomRenewal();
        RoomChat.text = "";
        for(int i=0; i < RoomChattings.Length; i++)
        {
            RoomChattings[i].text = "";
        }
    }
    void RoomRenewal()
    {
        RoomInfoText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            RoomInfoText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ",");
            RoomInfoText2.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        }

    }

    public void joinRoom()
    {
        //PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void JoinOrCreateRoom()
    {
        //PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 4 }, null);
    }




    //방만들기
    public override void OnCreatedRoom()
    {
        //PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 4 });
        base.OnCreatedRoom();
    }





    public override void OnCreateRoomFailed(short returnCode, string message)
    {

        RoomInfoText.text = "";
        CreateRoom();
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "방만들기 실패";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "방참가 실패";
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "랜덤방참가 실패";
    }



    public void NickName()
    {
        //PhotonNetwork.LocalPlayer.NickName = nicknameInputField.text;
    }

 




}
