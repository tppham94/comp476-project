using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    const string player_name_pref_key = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string default_name = string.Empty;
        InputField input_field = this.GetComponent<InputField>();
        if(input_field != null)
        {
            if(PlayerPrefs.HasKey(player_name_pref_key))
            {
                default_name = PlayerPrefs.GetString(player_name_pref_key);
                input_field.text = default_name;
            }
        }

        PhotonNetwork.NickName = default_name;
    }

    public void SetPlayerName(string str)
    {
        if(string.IsNullOrEmpty(str))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = str;
        PlayerPrefs.SetString(player_name_pref_key, str);
    }
}
