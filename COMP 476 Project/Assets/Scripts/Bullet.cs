using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTimer = 5f;
    public float shotSpeed = 1f;
    //public int thing;

    void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer < 0)
            Destroy(this.gameObject);

        transform.position += transform.forward * shotSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "mothership")
        {
            collision.gameObject.GetComponent<EnemyMothershipHealth>().GetDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {


            EnemyHealth esc_script = collision.gameObject.GetComponentInParent<EnemyHealth>();
            if (esc_script != null)
            {
                Debug.LogWarning("Emmie");
                esc_script.TakeDamage();
            }
            Destroy(gameObject);

        }
    }
}
