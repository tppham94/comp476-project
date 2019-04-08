using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 50;
    public float damage = 15;
    AudioSource audioSource;
    public AudioClip laserHit;
    public AudioClip fireLaser;
    public AudioClip shipDestroy;


    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("Booped");

        if (PhotonNetwork.IsMasterClient)
        {

            if (collision.gameObject.tag == "Player")
            {
                //Damage player
<<<<<<< HEAD
                audioSource.PlayOneShot(laserHit);
=======
                Debug.LogWarning("COLLIDED WITH PLAYER");
>>>>>>> parent of cd45f95... Merge pull request #13 from n04x/Thomas
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
