using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    #region Script Variables
    // room information
    public static PhotonRoom room;
    private PhotonView PV;

    public GameObject myPlayer;

    public bool game_loaded;
    public int current_scene;

    // player information
    private Player[] photon_players;
    public int players_in_room;
    public int my_number_in_room;
    public int player_in_game;

    // delayed start
    private bool ready_to_count;
    private bool ready_to_start;
    public float starting_time;
    private float less_than_max_players;
    private float at_max_player;
    private float time_to_start;

    public GameObject lobby_go;
    public GameObject room_go;
    public Transform players_panel;
    public GameObject player_listing_prefab;
    public GameObject start_button;

    #endregion

    #region MonoBehaviour  Functions
    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        ready_to_count = false;
        ready_to_start = false;
        less_than_max_players = starting_time;
        at_max_player = 4;
        time_to_start = starting_time;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(MultiplayerSetting.mp_setting.delayed_start)
        {
            if(players_in_room == 1)
            {
                RestartTimer();
            }
            if(!game_loaded)
            {
                if(ready_to_start)
                {
                    at_max_player -= Time.deltaTime;
                    less_than_max_players = at_max_player;
                    time_to_start = at_max_player; 
                }
                else if(ready_to_count)
                {
                    less_than_max_players -= Time.deltaTime;
                    time_to_start = less_than_max_players;
                }
                Debug.Log("Display time to start to the players " + time_to_start);
                if(time_to_start <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    public void StartGame()
    {
        game_loaded = true;
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(MultiplayerSetting.mp_setting.delayed_start)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSetting.mp_setting.mp_scene);
    }

    void RestartTimer()
    {
        less_than_max_players = starting_time;
        time_to_start = starting_time;
        at_max_player = 6;
        ready_to_count = false;
        ready_to_start = false;
    }
    
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        current_scene = scene.buildIndex;
        if(current_scene == MultiplayerSetting.mp_setting.mp_scene)
        {
            game_loaded = true;

            if(MultiplayerSetting.mp_setting.delayed_start)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }


    #endregion

    #region PunRPC Functions
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        player_in_game++;
        if (player_in_game == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }


    #endregion
    
    #region MonoBehaviourPUNCallbacks Override functions
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are in a room now");

        lobby_go.SetActive(false);
        room_go.SetActive(true);
        if(PhotonNetwork.IsMasterClient)
        {
            start_button.SetActive(true);
        }
        ClearPlayerListings();
        ListPlayers();

        photon_players = PhotonNetwork.PlayerList;
        players_in_room = photon_players.Length;
        my_number_in_room = players_in_room;

        if(MultiplayerSetting.mp_setting.delayed_start)
        {
            Debug.Log("Displayer players in room out of max players possible (" + players_in_room + ":" + MultiplayerSetting.mp_setting.max_players + ")");
            if(players_in_room > 1)
            {
                ready_to_count = true;
            }
            if(players_in_room == MultiplayerSetting.mp_setting.max_players)
            {
                ready_to_start = true;
                if(!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        //else
        //{
        //    StartGame();
        //}
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        ClearPlayerListings();
        ListPlayers();
        photon_players = PhotonNetwork.PlayerList;
        players_in_room++;
        if(MultiplayerSetting.mp_setting.delayed_start)
        {
            Debug.Log("Displayer players in room out of max players possible (" + players_in_room + ":" + MultiplayerSetting.mp_setting.max_players + ")");
            if(players_in_room > 1)
            {
                ready_to_count = true;
            }
            if(players_in_room == MultiplayerSetting.mp_setting.max_players)
            {
                ready_to_start = true;
                if(!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " has left the game!");
        players_in_room--;
        ClearPlayerListings();
        ListPlayers();
    }
    #endregion

    void ClearPlayerListings()
    {
        for(int i = players_panel.childCount - 1; i >= 0; i--)
        {
            Destroy(players_panel.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {
        if(PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject temp_listing = Instantiate(player_listing_prefab, players_panel);
                Text temp_text = temp_listing.transform.GetChild(0).GetComponent<Text>();
                temp_text.text = player.NickName;
            }
        }
    }
}
