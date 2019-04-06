using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PhotonView PV;
    Rigidbody _rb;
    public float movementSpeed = 1;
    public float maxSpeed = 100;

    public float mouseSpeed = 6f;
    private float mouseX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            InputMovement();
        }
    }
    void InputMovement()
    {
        //Sets a max velocitty on ship
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);

        //Accelerate
        if (Input.GetKey("w"))
            _rb.velocity += transform.forward * movementSpeed;

        if (Input.GetKey("s"))
            _rb.velocity += transform.forward * -movementSpeed;

        //Strafe
        if (Input.GetKey("q"))
            _rb.velocity += -transform.right * movementSpeed;

        if (Input.GetKey("e"))
            _rb.velocity += transform.right * movementSpeed;

        //Rotate
        mouseX += mouseSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0.0f, mouseX, 0.0f);

        //Slow down
        if (Input.GetKey("left shift"))
            _rb.velocity *= 0.99f;

        // leave game
        if (Input.GetKey(KeyCode.Escape))
            GameSetup.GS.DisconnectPlayer();

    }
}
