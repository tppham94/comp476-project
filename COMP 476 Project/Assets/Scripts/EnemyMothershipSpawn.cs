using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipSpawn : MonoBehaviour
{
    public float timeToSpawn = 20f;
    public int numbPlayers = 1;

    private float spawnTimer;

    public Transform spawnA;
    public Transform spawnB;

    //public GameObject Enemy1;
    //public GameObject Enemy2; //Add in 2nd type of enemy

    void Start()
    {
        spawnTimer = 0;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime / numbPlayers;
        
<<<<<<< HEAD
<<<<<<< HEAD
        if (spawnTimer < 0 )
=======
        if (spawnTimer < 0 && PhotonNetwork.IsMasterClient)
>>>>>>> parent of d6b2584... Update EnemyMothershipSpawn.cs
=======
        if (spawnTimer < 0 /*&& PhotonNetwork.IsMasterClient*/)
>>>>>>> parent of f924ae5... Merge branch 'master' into Thomas
        {
            GameObject a = PhotonNetwork.Instantiate("Squad", spawnA.position, Quaternion.identity);
            GameObject b = PhotonNetwork.Instantiate("Squad", spawnB.position, Quaternion.identity);

            a.name = "A";
            b.name = "B";

            a.GetComponent<SquadController>().squad_target = GameObject.FindGameObjectWithTag("Player").transform;
            b.GetComponent<SquadController>().squad_target = GameObject.FindGameObjectWithTag("Player").transform;

            Debug.Log("Instantiate spawns");

            spawnTimer = Mathf.Infinity;
        }
    }
}
