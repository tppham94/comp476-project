using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{

    #region Script Variables
    private PhotonView PV;

    public static GameSetup GS;
    public Transform[] spawn_positions;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
}
