using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Constantly checks on the state of the round in progress
public class RoundManager : MonoBehaviour
{
    public GameObject RoundText;
    public GameObject SpawnManager;

    private void Start()
    {
        GameMaster.CurrentRound++;
        //Reset round variables
        GameMaster.RoundOver = false;
        GameMaster.PlayersInRound = new ArrayList();
        GameMaster.RoundWinner = -1;
        GameMaster.RoundBegun = false;
        if(GameMaster.TieBreaker)
        {
            RoundText.GetComponent<Text>().text = "SUDDEN DEATH";
        }
        else
        {
            RoundText.GetComponent<Text>().text = "Round " + GameMaster.CurrentRound.ToString();
        }
        DisplayText();
        SpawnPlayers();
    }

    //Coroutine that displays text for 1.5 seconds
    private void DisplayText()
    {
        StartCoroutine(TextRoutine());
    }
    private IEnumerator TextRoutine()
    {
        RoundText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        RoundText.SetActive(false);
    }

    //Coroutine for spawning in players after 1.5 seconds
    private void SpawnPlayers()
    {
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnManager.GetComponent<PlayerSpawnManager>().StartRound();
    }

    //Coroutine that displays text for 2 seconds after a win
    private void DisplayWinText()
    {
        StartCoroutine(WinTextRoutine());
    }
    private IEnumerator WinTextRoutine()
    {
        RoundText.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (GameMaster.CurrentRound >= GameMaster.TotalRounds)
        {
            //Go to winner screen or sudden death
            //Check if winner or tie
            int wins = 0;
            PlayerObj mostWins = (PlayerObj) GameMaster.PlayerObjs[0];
            ArrayList tiedPlayers = new ArrayList();
            foreach(PlayerObj playerObj in GameMaster.PlayerObjs)
            {
                if(playerObj.Wins > wins)
                {
                    wins = playerObj.Wins;
                    tiedPlayers.Clear();
                    tiedPlayers.Add(playerObj);
                    mostWins = playerObj;
                }
                else if(playerObj.Wins == wins) {
                    tiedPlayers.Add(playerObj);
                }
            }

            //If there is a winner, go to the results page
            if(tiedPlayers.Count == 1)
            {
                GameMaster.MatchWinner = mostWins;
                SceneManager.LoadScene("MatchResults");

            }
            //Go to tiebreaker stage
            else {
                GameMaster.TieBreaker = true;
                SceneManager.LoadScene("SuddenDeath");
            }
        }
        else
        {
            //Choose next stage
            SceneManager.LoadScene("StageSelect");
        }
    }

    //Coroutine that displays text for 2 seconds after a draw
    private void DisplayDrawText()
    {
        StartCoroutine(DrawTextRoutine());
    }
    private IEnumerator DrawTextRoutine()
    {
        RoundText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //Display draw text and move to stage selector or sudden death
        if (GameMaster.TieBreaker)
        {
            //Go to sudden death
            SceneManager.LoadScene("SuddenDeath");
        }
        else
        {
            //Choose next stage
            SceneManager.LoadScene("StageSelect");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //If one player is remaining, they win the round
        if (GameMaster.PlayersInRound.Count == 1 && !GameMaster.RoundOver && GameMaster.RoundBegun)
        {
            GameMaster.RoundOver = true;
            foreach (PlayerObj playerObj in GameMaster.PlayerObjs)
            {
                if(GameMaster.PlayersInRound.Contains(playerObj))
                {
                    playerObj.Wins++;
                    GameMaster.RoundWinner = playerObj.PlayerIndex;

                    RoundText.SetActive(true);
                    RoundText.GetComponent<Text>().text = "Player " + (GameMaster.RoundWinner + 1).ToString() + " Wins";
                    DisplayWinText();
                    break;
                }
            }
        }
        //If somehow zero players are remaining, it's a tie
        else if (GameMaster.PlayersInRound.Count == 0 && !GameMaster.RoundOver && GameMaster.RoundBegun)
        {
            GameMaster.RoundOver = true;
            RoundText.SetActive(true);
            RoundText.GetComponent<Text>().text = "Draw";
            DisplayDrawText();
        }
    }
}
