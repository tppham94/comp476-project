using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject basicShotPrefab;
    private PhotonView PV;
    private PlayerAvatarSetup avatar_setup;
    private bool cooldown = false;
    private float shotCooldown;
    public float shotCooldownTime = 1f;

    private void Start()
    {
        shotCooldown = shotCooldownTime;
        PV = GetComponent<PhotonView>();
        avatar_setup = GetComponent<PlayerAvatarSetup>();
    }


    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (shotCooldown < 0)
        {
            shotCooldown = shotCooldownTime;
            cooldown = false;
        }
            
        else if (cooldown == true)
            shotCooldown -= Time.deltaTime;

        else if (((Input.GetKey("space")) || (Input.GetMouseButton(0))) && (cooldown == false))
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Bullet"), this.transform.position + (this.transform.forward * 5), this.transform.rotation);
            cooldown = true;
            Debug.Log("SHOOT!");
        }
    }

}
