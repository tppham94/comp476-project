using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    private GameObject[] motherships;
    private GameObject[] playerShips;
    private int numbPlayers;
    private int numbShips;
    private int numbMotherShips;

    public bool gameOver = false;

    private float startTimer = 1f;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumb();

        if ((!gameOver) && (startTimer < 0))
        {
            Debug.Log("Mothership: " + numbMotherShips + " Ships: " + numbShips);


            Condition();
        }

        else if (startTimer >= 0)
            startTimer -= Time.deltaTime;

    }

    void UpdateNumb()
    {
        motherships = GameObject.FindGameObjectsWithTag("mothership");
        playerShips = GameObject.FindGameObjectsWithTag("Ship");

        numbPlayers = PhotonRoom.room.players_in_room;
        numbShips = playerShips.Length;
        numbMotherShips = motherships.Length;
    }


    private void Condition()
    {
        if (numbMotherShips <= 0)
            WinCondition();

        else if (numbShips <= 0)
            LoseCondition();

    }

    private void WinCondition()
    {
        Debug.Log("WIN!");
        gameOver = true;
    }

    private void LoseCondition()
    {
        Debug.Log("LOSE!");
        gameOver = true;
    }

}
