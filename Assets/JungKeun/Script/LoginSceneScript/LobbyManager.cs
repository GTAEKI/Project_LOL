using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public PlayerData playerData;
    private string gameVersion = "1";
    public Button ReturnButton;
    public PhotonView PV;

    //ServerMessage, explanationMeesage
    public GameObject ServerMessage, explanationMessage;
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

    //gotoinRoom
    public GameObject gotoinRoom;
    public TMP_Text gotoinRoomName;
    public Button gotoinRoomMakeRoom;
    public Button gotoinRoomQuickStart;
    public Button gotoinRoomGotoLobby;
    public Button gotoinRoomGotoRoom;
    public TMP_Text gotoinRoomPressRoomName;

    //MakeRoom
    public GameObject MakeRoom;
    public TMP_InputField RoomInput;
    public Button MakeRoomMakeaRoom;
    public Button MakeRoomBacktoLobby;

    //Room
    public GameObject Room;

    public TMP_Text ListText;
    public TMP_Text RoomInfoText;

    public TMP_Text[] RoomPlayerNickName;
    public TMP_Text[] RoomChattings;

    public Image[] RoomPlayerCharacterImg;
    public Image[] RoomCharaters;

    public TMP_InputField RoomChat;

    public Button[] Characters;
    public GameObject[] characters;
    public Image[] MyCharacter;
    public string[] NickName;
    public string[] CharacterName;
    public Button CharacterLock;
    int selectedCharacterIndex;
    private bool isCharacterLocked = false;
    public TMP_Text[] Ready;
    private int[] CharacterReadyState;

    private string[] lastPlayerNames;
    private Sprite[] lastPlayerCharacterImages;


    //public scrollview RoomScrollView;
    public Button RoomGameStart;
    public Button Roomback;
    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    //프로그램을 실행하자마자
    private void Awake()
    {

        Screen.SetResolution(1920, 1080, false);
        PhotonNetwork.AutomaticallySyncScene = true;

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
        gotoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(false);
        LoginConnectServer.interactable = false;
        CharacterName = new string[characters.Length];

        CharacterReadyState = new int[Characters.Length];

        for (int i = 0; i < Characters.Length; i++)
        {
            CharacterReadyState[i] = 0; // 0은 "준비중"을 나타냅니다.
        }
        for (int i = 0; i < characters.Length; i++)
        {
            CharacterName[i] = characters[i].name;

            Debug.Log(CharacterName[i]);
        }

    }

    private void Update()
    {
        LobbyRoomandPeople.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비대기인원 / " + PhotonNetwork.CountOfPlayers + "접속/" + "방 갯수" + PhotonNetwork.CountOfRooms;
        if (LoginNickName == null)
        {
            LoginConnectServer.interactable = false;
        }
        if (LoginNickName.text.Length >= 2 && LoginNickName.text.Length < 6)
        {
            LoginConnectServer.interactable = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Send();
        }

        if (Room.gameObject.activeSelf)
        {
            RoomRenewal();
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                RoomPlayerNickName[i].text = PhotonNetwork.PlayerList[i].NickName;
            }
        }
    }


    #region 연결관련
    //연결이 됨
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();


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
        gotoinRoom.gameObject.SetActive(true);
        PhotonNetwork.LocalPlayer.NickName = LoginNickName.text;
        gotoinRoomName.text = LoginNickName.text + "님,환영합니다.";
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
        gotoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(false);
    }
    #endregion


    #region 방생성
    public void FirstCreateRoom()
    {
        MakeRoom.gameObject.SetActive(true);
    }
    public void DisFirstCreatRoom()
    {
        MakeRoom.gameObject.SetActive(false);
    }

    public void CreateRoom()
    {

        int roomCount = myList.Count;
        // 001부터 100까지 순차적으로 방 번호를 생성합니다.
        string roomNumber = (myList.Count + 1).ToString("D3");


        LobbyRoomnumber[myList.Count].text = roomNumber;
        // 현재 플레이어의 닉네임을 호스트 닉네임으로 사용합니다.
        string hostName = PhotonNetwork.LocalPlayer.NickName;

        LobbyRoomHost[myList.Count].text = hostName;


        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4,
            CustomRoomPropertiesForLobby = new string[] { "RoomNumber", "HostName" },
            CustomRoomProperties = new Hashtable
        {
            { "RoomNumber", roomNumber },
            { "HostName", hostName }
        }
        };
        PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + UnityEngine.Random.Range(0, 100) : RoomInput.text, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

        RoomInfoText.text = "";
        CreateRoom();
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "방만들기 실패";
    }

    #endregion


    #region gotoinRoom

    public void JoinRoom(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            // 이미 마스터 서버에 연결되어 있을 때 방에 참여할 수 있음
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            // 마스터 서버에 연결되어 있지 않으면 먼저 연결 후 방에 참여
            PhotonNetwork.JoinLobby(); // OnJoinedLobby 콜백에서 방 참여가 처리됨
        }
    }


    public override void OnJoinedRoom()
    {
        ServerMessage.gameObject.SetActive(false);
        explanationMessage.gameObject.SetActive(false);
        Lobby.gameObject.SetActive(false);
        gotoinRoom.gameObject.SetActive(false);
        MakeRoom.gameObject.SetActive(false);
        Room.gameObject.SetActive(true);
        base.OnJoinedRoom();


        RoomRenewal();
        RoomChat.text = "";
        for (int i = 0; i < RoomChattings.Length; i++)
        {
            RoomChattings[i].text = "";
        }
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        RoomInfoText.text = "";
        CreateRoom();
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "방참가 실패";
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        //ServerMessageconnectionInfoText.text = "랜덤방참가 실패";
    }

    public void ChooseRoom(int i)
    {
        if (i >= 0 && i < myList.Count)
        {
            RoomInfo selectedRoom = myList[i];
            string roomName = selectedRoom.Name;
            int roomNumber = i + 1; // 0부터 시작하는 인덱스를 1부터 시작하는 번호로 변경
            string hostName = selectedRoom.CustomProperties["HostName"].ToString();

            // 이 부분에서 RoomName, RoomNumber, HostName을 조합하여 원하는 형식의 문자열을 생성
            string roomInfo = "[" + roomNumber + "] " + roomName + " 방장: " + hostName;

            // gotoinRoomPressRoomName 텍스트에 할당
            gotoinRoomPressRoomName.text = roomInfo;
        }
    }

    #endregion


    #region JoinRandomRoom
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion


    #region inRoom

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ",");
            RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        PV.RPC("ChatRPC", RpcTarget.AllBuffered, "<color=yellow>" + newPlayer.NickName + "님이 참가하였습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        // 해당 플레이어의 정보를 삭제
        int leftPlayerIndex = Array.IndexOf(PhotonNetwork.PlayerList, otherPlayer);
        if (leftPlayerIndex >= 0 && leftPlayerIndex < CharacterReadyState.Length)
        {
            CharacterReadyState[leftPlayerIndex] = 0; // 해당 플레이어의 준비 상태를 초기화합니다.
            Ready[leftPlayerIndex].text = "준비중"; // 해당 플레이어의 Ready 텍스트를 업데이트합니다.
            RoomPlayerCharacterImg[leftPlayerIndex].sprite = null;
            RoomPlayerNickName[leftPlayerIndex].text = "";
        }

        // 플레이어를 다시 정렬
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // 플레이어의 닉네임을 업데이트합니다.
            RoomPlayerNickName[i].text = PhotonNetwork.PlayerList[i].NickName;
        }

        RoomRenewal();
        // 게임 시작 가능 여부 확인
        CheckStartGame();
        PV.RPC("ChatRPC", RpcTarget.AllBuffered, "<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    public void SelectCharacter(int characterIndex)
    {
        if (!isCharacterLocked)
        {
            // 현재 플레이어의 인덱스를 얻어옵니다.
            int myPlayerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);

            // 해당 플레이어의 MyCharacter에 선택한 캐릭터 스프라이트를 할당합니다.
            if (myPlayerIndex >= 0 && myPlayerIndex < MyCharacter.Length)
            {
                MyCharacter[myPlayerIndex].sprite = Characters[characterIndex].image.sprite;
            }

            // 선택한 캐릭터 인덱스 저장
            selectedCharacterIndex = characterIndex;

            // RPC를 사용하여 선택한 캐릭터 정보를 다른 플레이어에게 전달
            PV.RPC("SyncSelectedCharacter", RpcTarget.AllBuffered, myPlayerIndex, selectedCharacterIndex);
        }
    }

    [PunRPC]
    void SyncSelectedCharacter(int playerIndex, int characterIndex)
    {
        // 다른 플레이어의 MyCharacter 배열에 선택한 캐릭터 스프라이트를 할당
        if (playerIndex >= 0 && playerIndex < MyCharacter.Length)
        {
            MyCharacter[playerIndex].sprite = Characters[characterIndex].image.sprite;
        }
    }
    public void Lockinandout()
    {
        isCharacterLocked = !isCharacterLocked;

        CharacterLock.interactable = isCharacterLocked;
        RoomChat.interactable = isCharacterLocked;
        int myPlayerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);



        for (int i = 0; i < Characters.Length; i++)
        {
            if (!isCharacterLocked)
            {
                Characters[i].interactable = false;
                Roomback.interactable = true;
                CharacterReadyState[myPlayerIndex] = 0; // "준비중"으로 설정
            }
            else
            {
                Characters[i].interactable = true;
                Roomback.interactable = false;
                CharacterReadyState[myPlayerIndex] = 1; // "준비중"으로 설정

            }
            // characterReadyState에 따라 Ready 텍스트 업데이트
            if (i >= 0 && i < Ready.Length)
            {
                Ready[i].text = (CharacterReadyState[i] == 0) ? "준비중" : "준비완료";
            }

            // 다른 플레이어에게 캐릭터 준비 상태를 알리기 위한 RPC 호출
            PV.RPC("UpdateCharacterLockStatus", RpcTarget.AllBuffered, myPlayerIndex, CharacterReadyState[myPlayerIndex]);


        }
        Roomback.interactable = false;


        // 게임 시작 가능 여부 확인
        CheckStartGame();

    }

    void CheckStartGame()
    {
        // 모든 플레이어가 "준비완료" 상태인지 확인
        bool allPlayersReady = true;
        for (int i = 0; i < CharacterReadyState.Length; i++)
        {
            if (CharacterReadyState[i] == 0)
            {
                allPlayersReady = false;
                break;
            }
        }

        // 모든 플레이어가 "준비완료" 상태이면 게임을 시작
        if (allPlayersReady)
        {
            // 각 플레이어의 닉네임과 캐릭터 인덱스를 PlayerPrefs에 저장
            int myPlayerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
            PlayerPrefs.SetString("SelectedCharacterName", CharacterName[selectedCharacterIndex]);
            PlayerPrefs.SetString("NickName", PhotonNetwork.LocalPlayer.NickName);
        }
    }

    [PunRPC]
    private void UpdateCharacterLockStatus(int characterIndex, int readyState)
    {
        // 다른 플레이어에게 캐릭터 준비 상태 업데이트
        CharacterReadyState[characterIndex] = readyState;

        // 다른 플레이어에게 캐릭터 준비 상태에 따라 Ready 텍스트 업데이트
        Ready[characterIndex].text = (readyState == 0) ? "준비중" : "준비완료";
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            bool allPlayersReady = true;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (Ready[i].text != "준비완료")
                {
                    allPlayersReady = false;
                    break;
                }
            }

            if (allPlayersReady)
            {
                // 선택한 캐릭터 이름과 닉네임을 PlayerData에 설정
                PlayerData.Instance.SetCharacterAndNickName(CharacterName[selectedCharacterIndex], PhotonNetwork.LocalPlayer.NickName);

                PhotonNetwork.LoadLevel("KyungTaek_Player");
            }
        }
    }


    public override void OnLeftRoom()
    {
        // 방 정보 초기화
        RoomInfoText.text = "";
        ListText.text = "";
        PhotonNetwork.LeaveRoom();
        // Lobby와 gotoinRoom 활성화, Room 비활성화
        Lobby.gameObject.SetActive(true);
        gotoinRoom.gameObject.SetActive(true);
        Room.gameObject.SetActive(false);


        base.OnLeftRoom();
    }


    public void MyList(int num)
    {
        if (num == -2)
        {
            --currentPage;
        }

        else if (num == -1)
        {
            ++currentPage;
        }

        else
        {
            PhotonNetwork.JoinRoom(myList[multiple + num].Name);
            MyListRenewal();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;

        // 방 목록 갱신 전에 현재 선택된 방을 저장
        string selectedRoomName = gotoinRoomPressRoomName.text;

        // 현재 방 목록 초기화
        for (int i = 0; i < LobbyRooms.Length; i++)
        {
            LobbyRoomnumber[i].text = "";
            LobbyRoomHost[i].text = "";
        }

        // 유효한 방 정보만 myList에 추가
        myList.Clear();
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList && roomList[i].IsOpen && roomList[i].PlayerCount < roomList[i].MaxPlayers)
            {
                myList.Add(roomList[i]);
            }
        }

        // 방 목록 갱신
        MyListRenewal();

        // 현재 선택된 방을 다시 표시
        if (!string.IsNullOrEmpty(selectedRoomName))
        {
            for (int i = 0; i < myList.Count; i++)
            {
                string roomInfo = "[" + (i + 1) + "] " + myList[i].Name + " 방장: " + myList[i].CustomProperties["HostName"].ToString();
                if (roomInfo == selectedRoomName)
                {
                    gotoinRoomPressRoomName.text = roomInfo;
                    break;
                }
            }
        }
    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }



    void MyListRenewal()
    {
        maxPage = (myList.Count % LobbyRooms.Length == 0) ? myList.Count / LobbyRooms.Length : myList.Count / LobbyRooms.Length + 1;

        LobbyBeforeButton.interactable = (currentPage <= 1) ? false : true;
        LobbyNextButton.interactable = (currentPage >= maxPage) ? false : true;

        multiple = (currentPage - 1) * LobbyRooms.Length;

        for (int i = 0; i < LobbyRooms.Length; i++)
        {
            bool isRoomValid = (multiple + i < myList.Count);
            LobbyRooms[i].interactable = isRoomValid;

            if (isRoomValid)
            {
                TMP_Text roomNameText = LobbyRooms[i].transform.GetChild(0).GetComponent<TMP_Text>();
                roomNameText.text = myList[multiple + i].Name;
                roomNameText.text += " " + myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers;

                // 방 번호와 호스트 닉네임을 설정
                LobbyRoomnumber[i].text = myList[multiple + i].CustomProperties["RoomNumber"].ToString();
                LobbyRoomHost[i].text = myList[multiple + i].CustomProperties["HostName"].ToString();
            }
            else
            {
                // 방 정보가 없는 경우 빈 문자열로 설정
                LobbyRoomnumber[i].text = "";
                LobbyRoomHost[i].text = "";
            }

        }

    }

    public void Send()
    {
        string msg = PhotonNetwork.NickName + " : " + RoomChat.text;
        PV.RPC("ChatRPC", RpcTarget.AllBuffered, PhotonNetwork.NickName + " : " + RoomChat.text);
        RoomChat.text = "";
    }



    [PunRPC]
    void ChatRPC(string msg)
    {
        bool IsInput = false;
        for (int i = 0; i < RoomChattings.Length; i++)
        {
            if (RoomChattings[i].text == "")
            {
                IsInput = true;
                RoomChattings[i].text = msg;
                break;
            }

        }
        if (!IsInput)
        {
            for (int i = 1; i < RoomChattings.Length; i++)
            {
                RoomChattings[i - 1].text = RoomChattings[i].text;
                RoomChattings[RoomChattings.Length - 1].text = msg;
            }
        }
    }
    #endregion
}
