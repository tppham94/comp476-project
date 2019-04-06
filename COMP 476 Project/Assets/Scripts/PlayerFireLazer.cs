using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerFireLazer : MonoBehaviour
{
    Lazer laserPrefab;

    public float spawndiff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireLaser()
    {
        Debug.Log("Firing laser");
        PhotonNetwork.Instantiate("Bullet", transform.position + transform.forward * spawndiff, transform.rotation);
    }
}
