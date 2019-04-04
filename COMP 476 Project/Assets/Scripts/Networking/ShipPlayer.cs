using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject my_ship;
    //List<int> spawn_indexes = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawn_pos_pick = (PV.ViewID / 1000) - 1; // take the view id of the player and use it as spawn index position.
        if(PV.IsMine)
        {
            my_ship = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ShipAvatar"), GameSetup.GS.spawn_positions[spawn_pos_pick].position, GameSetup.GS.spawn_positions[spawn_pos_pick].rotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
