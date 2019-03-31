using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour
{
    public static MultiplayerSetting mp_setting;

    public bool delayed_start;
    public int max_players;
    public int menu_scene;
    public int mp_scene;

    private void Awake()
    {
        if(MultiplayerSetting.mp_setting == null)
        {
            MultiplayerSetting.mp_setting = this;
        }
        else
        {
            if(MultiplayerSetting.mp_setting != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
