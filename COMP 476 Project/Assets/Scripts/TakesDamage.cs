using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour
{
    public int maxhealth;

    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collison with damage script");
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            Debug.Log("We took damage!");
            health -= 5;
            Destroy(other.gameObject);

            if (health <= 0)
            {
                noHealth();
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collison with damage script");
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Debug.Log("We took damage!");
            health -= 5;
            Destroy(collision.gameObject);

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    void noHealth()
    {
        gameObject.active = false;
    }
}
