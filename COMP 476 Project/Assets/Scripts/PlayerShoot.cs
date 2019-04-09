using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject basicShotPrefab;
    private PhotonView PV;
    private PlayerAvatarSetup avatar_setup;
    private bool cooldown = false;
    private float shotCooldown;
    public float shotCooldownTime = 1f;
    LineRenderer laser;
    Camera fpscam;
    private WaitForSeconds effectduration = new WaitForSeconds( .1f);
    public AudioClip laserHit;
    public AudioClip fireLaser;
    public AudioClip shipDestroy;
    AudioSource audioSource;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        shotCooldown = shotCooldownTime;
        PV = GetComponent<PhotonView>();
        avatar_setup = GetComponent<PlayerAvatarSetup>();
        fpscam = GetComponent<PlayerAvatarSetup>().my_camera;
        laser.enabled = false;
        audioSource = GetComponent<AudioSource>();

    }


    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (shotCooldown < 0)
        {
            shotCooldown = shotCooldownTime;
            cooldown = false;
        }
            
        else if (cooldown == true)
            shotCooldown -= Time.deltaTime;

        else if (((Input.GetKey("space")) || (Input.GetMouseButton(0))) && (cooldown == false))
        {
           // GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Bullet"), this.transform.position + (this.transform.forward * 5), this.transform.rotation);
        
            cooldown = true;
            Debug.Log("SHOOT!");
            Laser();
            laser.enabled = true;
            audioSource.PlayOneShot(fireLaser);
        }
      Debug.DrawRay(fpscam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0)), fpscam.transform.forward * 500, Color.green);
    

    }
    Vector3 rayOrigin;
    void Laser()
    {
        StartCoroutine(Effect());
        rayOrigin = fpscam.ViewportToWorldPoint(new Vector3(.5f,.5f, 0));
        RaycastHit hit;
        laser.SetPosition(0, GetComponentInChildren<MeshRenderer>().transform.position);
        if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, 500))
        {
            laser.SetPosition(1, hit.point);
            Debug.LogWarning(hit.collider.gameObject.name);
        }
        else
        {
            laser.SetPosition(1, rayOrigin+( fpscam.transform.forward * 500));

        }
        if (hit.collider != null && PV.IsMine)
        {
            /*  if (hit.collider.gameObject.tag == "mothership")
              {
                  hit.collider.gameObject.GetComponentInParent<EnemyMothershipHealth>().GetDamage();
                  audioSource.PlayOneShot(laserHit);
              }
              if (hit.collider.gameObject.tag == "Enemy")
              {


                  EnemyHealth esc_script = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();
                  if (esc_script != null)
                  {
                      Debug.LogWarning("Emmie");
                      esc_script.TakeDamage();
                      audioSource.PlayOneShot(laserHit);
                  }
                  audioSource.PlayOneShot(shipDestroy);

              }*/
            string collidertag = hit.collider.gameObject.tag;
        if(collidertag == "mothership" || collidertag == "Enemy")
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), hit.point, this.transform.rotation);

            }
        }
    }

    private IEnumerator Effect()
    {
        laser.enabled = true;

        yield return effectduration;

        laser.enabled = false;
    }
}
