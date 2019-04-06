using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 50;
    public float damage = 15;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
