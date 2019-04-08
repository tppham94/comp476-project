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
    public float decelerationTime = 1f;

    public float mouseSpeed = 6f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    public float mouseYLimit = 45f;

    //public AudioClip thrusters;
    public AudioClip collision;
    AudioSource audioSource;
    float audiotimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            GetComponent<AudioListener>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            InputMovement();
        }
    }

    //This will decelerate as soon as the key is not click anymore
    //Applies to WASD keys
    IEnumerator decelerateSpeed()
    {
        float time = 0f;
        Vector3 fromVelocity = _rb.velocity;
        while (time < decelerationTime)
        {
            _rb.velocity = Vector3.Lerp(fromVelocity, Vector3.zero, time);
            time += Time.deltaTime / decelerationTime;
            yield return null;
        }
    }

    void InputMovement()
    {

        //Sets a max velocitty on ship
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);

        //Accelerate
        if (Input.GetKey("w"))
            _rb.velocity += transform.forward * movementSpeed;

        if (Input.GetKeyUp("w"))
            StartCoroutine(decelerateSpeed());

        if (Input.GetKey("s"))
            _rb.velocity += transform.forward * -movementSpeed;

        if (Input.GetKeyUp("s"))
            StartCoroutine(decelerateSpeed());

        //Strafe
        if (Input.GetKey("a"))
            _rb.velocity += -transform.right * movementSpeed;

        if (Input.GetKeyUp("a"))
            StartCoroutine(decelerateSpeed());

        if (Input.GetKey("d"))
            _rb.velocity += transform.right * movementSpeed;

        if (Input.GetKeyUp("d"))
            StartCoroutine(decelerateSpeed());

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                
            }
            audiotimer = 1f;
            audioSource.volume = audiotimer;

        }
        else
        {
            audiotimer -= Time.deltaTime;
            audioSource.volume = audiotimer;
            if (audiotimer < 0)
            {
                audioSource.Stop();
            }
            //if(_rb.velocity.magnitude < maxSpeed/4)//arbitrary amount? needs testing?
            //{
            //    audioSource.Stop();
            //}
                
        }

        /*if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<PlayerFireLazer>().fireLaser();
        }*/
        //Rotate
        mouseX += mouseSpeed * Input.GetAxis("Mouse X");
        mouseY += mouseSpeed* Input.GetAxis("Mouse Y");
        if(mouseY > mouseYLimit && mouseY > 0)
        {
            mouseY = mouseYLimit;
        }else 
            if(mouseY < -mouseYLimit && mouseY < 0)
        {
            mouseY = -mouseYLimit;
        }
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0.0f);

        //Slow down
        if (Input.GetKey("left shift"))
            _rb.velocity *= 0.99f;

        // leave game
        if (Input.GetKey(KeyCode.Escape))
            GameSetup.GS.DisconnectPlayer();

    }
}
