using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarSetup : MonoBehaviour
{
    #region Script Variables
    private PhotonView PV;
    public GameObject my_ship;
    private int ship_value;

    public Transform start_position;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            int spawn_pos_pick = (PV.ViewID / 1000) - 1;
            start_position = GameSetup.GS.spawn_positions[spawn_pos_pick];  // stored it for respawning position if player get killed.
            PV.RPC("RPC_AddShip", RpcTarget.AllBuffered, PlayerInfo.PI.selected_ship);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC] void RPC_AddShip(int which_ship)
    {
        ship_value = which_ship;
        my_ship = Instantiate(PlayerInfo.PI.all_ships[which_ship], transform.position, transform.rotation, transform);
    }
}
