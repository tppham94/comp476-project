using Photon.Pun;
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

    [SerializeField]
    GameObject explosion;
    private PhotonView PV;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
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
        if (PV.IsMine )
        {
            if (collision.gameObject.tag == "mothership")
            {
                collision.gameObject.GetComponent<EnemyMothershipHealth>().GetDamage();
                GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);

            }
            if (collision.gameObject.tag == "Enemy")
            {


                EnemyHealth esc_script = collision.gameObject.GetComponent<EnemyHealth>();
                if (esc_script != null)
                {
                    esc_script.TakeDamage();
                    audioSource.PlayOneShot(laserHit);

                }
                GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(shipDestroy);
                PhotonNetwork.Destroy(gameObject);

            }
        }
    }
}
