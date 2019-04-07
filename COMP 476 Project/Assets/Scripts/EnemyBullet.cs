using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 50;
    public float damage = 15;
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
                Debug.LogWarning("COLLIDED WITH PLAYER");
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
