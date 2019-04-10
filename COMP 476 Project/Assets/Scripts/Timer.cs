using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer timeSing;
    private PhotonView pv;
    [SerializeField]
    private Text timerUIText;
    [SerializeField]
    private float displayTimer;

    private float timer;

    //Assigning the timer so the ui gets it
    void Start()
    {
        pv = GetComponent<PhotonView>();
        timer = displayTimer;
    }

    void Update()
    {
        if (pv.IsMine && !GameRules.GR.gameOver)
        {
            timer += Time.deltaTime;
            pv.RPC("timerUpdate", RpcTarget.All, timer);
        }
    }

    //Pun function to send to all other client 
    //synchronizing the timer 
    [PunRPC]
    public void timerUpdate(float t)
    {
        //This will calculate the minutes and seconds before 
        //displaying the value to the UI
        string minutes = Mathf.Floor(t / 60).ToString("0");
        string seconds = (t % 60).ToString("00");
        string fract = ((t * 100) % 100).ToString("0");
        //Format of Minutes:Seconds:Milliseconds
        timerUIText.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fract);
    }
}
