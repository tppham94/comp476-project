using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInputw : MonoBehaviour
{
    const string player_name_pref_key = "PlayerName";

    private void Start()
    {
        string default_name = string.Empty;
        InputField IF = this.GetComponent<InputField>();
        if(IF != null)
        {
            if(PlayerPrefs.HasKey(player_name_pref_key))
            {
                default_name = PlayerPrefs.GetString(player_name_pref_key);
                IF.text = default_name;
            }
        }

        PhotonNetwork.NickName = default_name;
    }

    public void SetPlayerName(string str)
    {
        if(string.IsNullOrEmpty(str))
        {
            Debug.LogError("Player name is null or empty");
            return;
        }
        PhotonNetwork.NickName = str;
        PlayerPrefs.SetString(player_name_pref_key, str);
    }
}
