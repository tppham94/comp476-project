using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public float destroyTimer = 2f;
    

    // Update is called once per frame
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (PhotonNetwork.IsMasterClient)
        {
            if (destroyTimer < 0)
                PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
