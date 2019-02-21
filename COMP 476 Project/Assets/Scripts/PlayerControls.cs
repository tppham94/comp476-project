using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    Rigidbody _rb;
    public float movementSpeed = 1;
    public float maxSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {
        //Sets a max velocitty on ship
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);

        //Accelerate
        if (Input.GetKey("w"))
            _rb.velocity += transform.forward * movementSpeed;

        if (Input.GetKey("s"))
            _rb.velocity += transform.forward * -movementSpeed;

        //Rotate
        if (Input.GetKey("a"))
            transform.Rotate(Vector3.down);

        if (Input.GetKey("d"))
            transform.Rotate(Vector3.up);

        //Strafe
        if (Input.GetKey("q"))
            _rb.velocity += -transform.right * movementSpeed;

        if (Input.GetKey("e"))
            _rb.velocity += transform.right * movementSpeed;
    }
}
