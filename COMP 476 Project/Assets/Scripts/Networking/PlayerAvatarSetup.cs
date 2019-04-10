using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarSetup : MonoBehaviour
{
    #region Script Variables
    private PhotonView PV;
    private int ship_value;

    public GameObject my_ship;
    public Camera my_camera;
    //public AudioListener my_al;
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
        else
        {
            Destroy(my_camera);
            // Destroy(my_al);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 player_offset = new Vector3(0,-5f,0);
    [PunRPC] void RPC_AddShip(int which_ship)
    {
        ship_value = which_ship;
        my_ship = Instantiate(PlayerInfo.PI.all_ships[which_ship], transform.position+player_offset, transform.rotation, transform);

    }
}
