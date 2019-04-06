using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipSpawn : MonoBehaviour
{
    public float timeToSpawn = 15f;
    public int numbPlayers = 1;

    private float spawnTimer;

    public Transform spawnA;
    public Transform spawnB;

    public GameObject Enemy1;
    //public GameObject Enemy2;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime * numbPlayers;
        
        if (spawnTimer < 0)
        {
            GameObject a = Instantiate(Enemy1, spawnA.position, Quaternion.identity);
            GameObject b = Instantiate(Enemy1, spawnB.position, Quaternion.identity);

            a.name = "A";
            b.name = "B";

            a.GetComponent<EnemyStateController>().target = GameObject.FindGameObjectWithTag("Player").transform;
            b.GetComponent<EnemyStateController>().target = GameObject.FindGameObjectWithTag("Player").transform;

            Debug.Log("Instantiate spawns");

            spawnTimer = timeToSpawn;
        }
    }
}
