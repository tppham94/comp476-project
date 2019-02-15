using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    Rigidbody _rb;
    public float MovementSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w"))
            _rb.velocity += transform.forward * MovementSpeed;

        if (Input.GetKey("s"))
            _rb.velocity += transform.forward * -MovementSpeed;

        if (Input.GetKey("a"))
            transform.Rotate(Vector3.down);

        if (Input.GetKey("d"))
            transform.Rotate(Vector3.up);
    }
}
