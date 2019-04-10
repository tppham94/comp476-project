using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Script Variables
    public static PhotonLobby lobby;
    public string room_name;
    public int room_max_player;
    public GameObject room_listing_prefab;
    public Transform rooms_panel;

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

    public void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)room_max_player };
        PhotonNetwork.CreateRoom(room_name, roomOps);
    }
    #endregion

    #region MonoBehaviourPUNCallbacks Override functions

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must be already be a room with the same name");
    }



    #endregion

    #region ILobbyCallbacks Override functions

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach(RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }
    #endregion
    #region Public function
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button was clicked");
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void OnRoomNameChanged(string name)
    {
        room_name = name;
    }

    public void OnRoomMaxPlayerChanged(string max_players)
    {
        room_max_player = int.Parse(max_players);
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    void RemoveRoomListings()
    {
        while(rooms_panel.childCount != 0)
        {
            Destroy(rooms_panel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject temp_listing = Instantiate(room_listing_prefab, rooms_panel);
            RoomButton temp_button = temp_listing.GetComponent<RoomButton>();
            temp_button.room_name = room.Name;
            temp_button.room_max_player = room.MaxPlayers;
            temp_button.SetRoom();
        }
    }
    #endregion
}
