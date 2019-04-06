using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipHealth : MonoBehaviour
{
    public int health = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            health--;
            Destroy(other.gameObject);
            Debug.Log("Mothership Health: " + health);
        }
    }

    void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
