using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text name_text;
    public Text max_player_text;

    public string room_name;
    public int room_max_player;
    public void SetRoom()
    {
        name_text.text = room_name;
        max_player_text.text = room_max_player.ToString();
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(room_name);
    }
}
