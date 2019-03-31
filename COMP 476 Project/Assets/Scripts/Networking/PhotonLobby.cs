using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    #region Script Variables
    public static PhotonLobby lobby;

    public GameObject start_button;
    public GameObject stop_button;


    #endregion

    #region MonoBehaviour Functions
    private void Awake() {
        lobby = this;   // create the singleton, lives within the Menu scene.
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();   // connects to master photon server.
    }
    // Update is called once per frame
    void Update()
    {

    }
    void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        int random_room_name = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte) MultiplayerSetting.mp_setting.max_players };
        PhotonNetwork.CreateRoom("Room" + random_room_name, roomOps);
    }
    #endregion

    #region MonoBehaviourPUNCallbacks Override functions

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        start_button.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried o join a random game but failed. There must be no open game availabe");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must be already be a room with the same name");
        CreateRoom();
    }



    #endregion

    #region Public function
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button was clicked");
        start_button.SetActive(false);
        stop_button.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnStopButtonClicked()
    {
        Debug.Log("Stop button was clicked");
        stop_button.SetActive(false);
        start_button.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    #endregion
}
