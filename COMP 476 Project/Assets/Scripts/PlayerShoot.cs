using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject basicShotPrefab;

    private bool cooldown = false;
    private float shotCooldown;
    public float shotCooldownTime = 1f;

    private void Start()
    {
        shotCooldown = shotCooldownTime;
    }


    void Update()
    {
        if (shotCooldown < 0)
        {
            shotCooldown = shotCooldownTime;
            cooldown = false;
        }
            
        else if (cooldown == true)
            shotCooldown -= Time.deltaTime;

        else if ((Input.GetKey("space")) && (cooldown == false))
        {
            Instantiate(basicShotPrefab, this.transform.position + (this.transform.forward * 5), this.transform.rotation);
            cooldown = true;
            Debug.Log("SHOOT!");
        }
            
        
    }
}
