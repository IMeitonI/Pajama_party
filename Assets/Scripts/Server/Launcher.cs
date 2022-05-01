using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{


    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInpuField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] string modeSelectorMenu;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    [SerializeField] TMP_InputField nickNameInpuField;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting Master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player_" + Random.Range(0, 1000).ToString("0000");
        nickNameInpuField.text = PhotonNetwork.NickName.ToString();
    }

    public void ChangeName()
    {
        if (!string.IsNullOrEmpty(nickNameInpuField.text))
        {
            PhotonNetwork.NickName = nickNameInpuField.text.ToString();
            Debug.Log("the new name is: " + PhotonNetwork.NickName.ToString());

        }
        else
        {
            nickNameInpuField.text = PhotonNetwork.NickName.ToString();
            Debug.Log("the new name is: " + PhotonNetwork.NickName.ToString());

        }

    }
    

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInpuField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInpuField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform item in playerListContent)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");

    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");


    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");

    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftLobby()
    {
        SceneManager.LoadScene(modeSelectorMenu);
        PhotonNetwork.Disconnect();
        Destroy(this);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

}
