using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTimer = 5f;
    public float shotSpeed = 1f;


    void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer < 0)
            Destroy(this.gameObject);

        transform.position += transform.forward * shotSpeed;
    }
}
