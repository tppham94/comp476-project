using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedUI : MonoBehaviour
{
    [SerializeField]
    private Text speedUIText;
    [SerializeField]
    private float displaySpeed;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = displaySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //displaySpeeds();
        var playerControlsScript = GetComponent<PlayerControls>();
        float mph = playerControlsScript._rb.velocity.magnitude * 2.237f;
        speedUIText.text = Mathf.RoundToInt(mph).ToString() + " MPH";
    }

    void displaySpeeds()
    {
        
    }
}
