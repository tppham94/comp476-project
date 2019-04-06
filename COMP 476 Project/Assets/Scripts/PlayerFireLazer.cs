using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerFireLazer : MonoBehaviour
{
    Lazer laserPrefab;

    public float spawndiff;

    public float timeBetweenShots = 1;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void fireLaser()
    {
        if (timer < 0)
        {

            timer = timeBetweenShots;
            PhotonNetwork.Instantiate("Bullet", transform.position + transform.forward * spawndiff, transform.rotation);
        }
    }
}
