using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipHealth : MonoBehaviour
{
    public static EnemyMothershipHealth EMH;
    private PhotonView PV;

    public AudioClip takedamage;
    public AudioClip defeat;

    public Vector3 deathfallposvector = new Vector3(1.0f, -2.0f, 1.0f);
    public Vector3 deathfallanglevector = new Vector3(1.0f, 1.0f, 1.0f);

    public float deathfallspeed = 50;
    public float deathfallanglespeed = 100;

    bool destroyed = false;
    

    AudioSource audioSource;

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }
    void Update()
    {
        //if(PhotonNetwork.IsMasterClient)
        //{
            if(health <= 0 && !destroyed)
            {
                audioSource.volume = 4.0f;
                audioSource.pitch = 0.5f;
                audioSource.PlayOneShot(defeat);
                destroyed = true;
                GetComponent<EnemyMothershipSpawn>().enabled = false;
                
                //PhotonNetwork.Destroy(this.gameObject);
            }
        //}

        if (destroyed)
        {
            transform.position += deathfallposvector * Time.deltaTime * deathfallspeed;
            transform.Rotate(deathfallanglevector, deathfallanglespeed * Time.deltaTime);
        }
    }

    public void GetDamage()
    {
        Debug.LogWarning(gameObject.name + " hp left: " + health);
        health--;

        audioSource.pitch = 3.5f;
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(takedamage);
        
        PV.RPC("Health", RpcTarget.All, health);
       
    }

    [PunRPC] void Health(int h)
    {
        health = h;
    }
}
