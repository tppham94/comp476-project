using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int numOfHealthBars;
    public Image[] bars;

    private void Start()
    {
        health = numOfHealthBars;

        foreach (Image o in bars)
            o.enabled = false;
    }

    void Update()
    {

        DisplayHealth();

    }


    void DisplayHealth()
    {
        if (health != numOfHealthBars)
            bars[health].enabled = false;

        if (health > 0)
            bars[(health - 1)].enabled = true;
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage()
    {
        //TODO Figure out what to do with 2 TakeDamage() on same frame. Reduces health by 2 but DisplayHealth() messes up.
        health--;
        Debug.Log("Health: " + health);
    }


}
