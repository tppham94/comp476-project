    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    public int selected_ship;

    public GameObject[] all_ships;

    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if(PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MyPlayer"))
        {
            selected_ship = PlayerPrefs.GetInt("MyPlayer");
        }
        else
        {
            selected_ship = 0;
            PlayerPrefs.SetInt("MyPlayer", selected_ship);
        }
    }
}
