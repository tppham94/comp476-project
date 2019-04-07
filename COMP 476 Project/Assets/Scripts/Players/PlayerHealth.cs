using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHealthBars;
    public Image[] bars;

    void Update()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (i < numOfHealthBars)
            {
                bars[i].enabled = true;
            }
            else
            {
                bars[i].enabled = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            numOfHealthBars--;
        }
    }
}
