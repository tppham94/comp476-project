using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    private PhotonView PV;
    public int numOfHealthBars;
    public Image[] bars;


    private void Start()
    {
        health = numOfHealthBars;
        PV = GetComponent<PhotonView>();

        foreach (Image o in bars)
            o.enabled = false;
    }

    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }
        DisplayHealth();

    }


    void DisplayHealth()
    {
        if ((this.gameObject.transform.Find("PlayerShip(Clone)") != null) && (health <= 0))
            PhotonNetwork.Destroy(this.gameObject.transform.Find("PlayerShip(Clone)").gameObject);

        foreach (Image o in bars)
            o.enabled = false;

        if (health > 0)
            bars[(health - 1)].enabled = true;
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
    }


}
