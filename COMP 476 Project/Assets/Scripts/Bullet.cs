using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTimer = 5f;
    public float shotSpeed = 1f;
    AudioSource audioSource;
    public AudioClip laserHit;
    public AudioClip fireLaser;
    public AudioClip shipDestroy;
    //public int thing;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(fireLaser);
    }

    

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
            audioSource.PlayOneShot(shipDestroy);
            Destroy(gameObject);
            
        }
        if (collision.gameObject.tag == "Enemy")
        {


            EnemyHealth esc_script = collision.gameObject.GetComponentInParent<EnemyHealth>();
            if (esc_script != null)
            {
                Debug.LogWarning("Emmie");
                esc_script.TakeDamage();
                audioSource.PlayOneShot(laserHit);
            }
            audioSource.PlayOneShot(shipDestroy);
            Destroy(gameObject);

        }
    }
}
