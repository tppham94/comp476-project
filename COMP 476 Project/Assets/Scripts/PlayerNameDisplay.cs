using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField]
    public Text playerName;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        playerName.text = PV.Owner.NickName;
        if (PV.IsMine)
        {
            GetComponent<Canvas>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
