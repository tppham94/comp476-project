using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Music : MonoBehaviour
{
    public AudioSource music;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (!music.isPlaying)
            {
                PV.RPC("musicPlay", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void musicPlay()
    {
        music.Play();
    }
}
