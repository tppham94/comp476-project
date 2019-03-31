using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnClickPokemonPick(int which_ship)
    {
        if(PlayerInfo.PI != null)
        {
            PlayerInfo.PI.selected_ship = which_ship;
            PlayerPrefs.SetInt("MyShip", which_ship);
        }
    }
}
