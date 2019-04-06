using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipHealth : MonoBehaviour
{
    public static EnemyMothershipHealth EMH;
    private PhotonView PV;

    public int health = 15;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Bullet")
    //    {
    //        health--;
    //        Destroy(other.gameObject);
    //        Debug.Log("Mothership Health: " + health);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Bullet" && PhotonNetwork.IsMasterClient)
    //    {
    //        PV.RPC("MotherShipHit", RpcTarget.All);
    //        //health--;
    //        //Destroy(collision.gameObject);
    //    }
    //}

    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if(health <= 0)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
            PV.RPC("MotherShipHealth", RpcTarget.Others, health);
        }
    }

    public void GetDamage()
    {
        health--;
        Debug.Log(gameObject.name + " hp left: " + health);
    }
    [PunRPC] void MotherShipHealth(int h)
    {
        health = h;
    }
}
