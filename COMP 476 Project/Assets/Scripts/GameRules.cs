using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    public static GameRules GR;
    private GameObject[] motherships;
    private GameObject[] playerShips;
    private int numbPlayers;
    private int numbShips;
    private int numbMotherShips;

    public Text game_over_text;
    public Text quit_option;

    public bool gameOver = false;

    private float startTimer = 1f;

    private void OnEnable()
    {
        if (GameRules.GR == null)
        {
            GameRules.GR = this;
        }
    }

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

        else if (numbShips  <= 0)
            LoseCondition();

    }

    private void WinCondition()
    {
        EnabledGameOverUI();
        game_over_text.text = "CONGRATULATIONS, YOU WIN";
        gameOver = true;
    }

    private void LoseCondition()
    {
        EnabledGameOverUI();
        game_over_text.text = "SORRY, YOU LOSE...";
        gameOver = true;
    }

    void EnabledGameOverUI()
    {
        game_over_text.enabled = true;
        quit_option.enabled = true;
    }

   
}
