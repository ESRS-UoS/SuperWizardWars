using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public GameObject[] text;
    public GameObject winnerText;
    //Fetch the winner and all scores and then display them 
    void Start()
    {
        //Reset game variables
        GameMaster.TieBreaker = false;

        int winnerId = GameMaster.MatchWinner.PlayerIndex + 1;
        winnerText.GetComponent<Text>().text = ("Player " + winnerId.ToString() + " Won!");

        //Show scores
        int index = 0;
        foreach (PlayerObj player in GameMaster.PlayerObjs)
        {
            int playerId = player.PlayerIndex + 1;
            int playerWins = player.Wins;
            text[index].GetComponent<Text>().text = ("Player " + playerId.ToString() + ": " + playerWins.ToString() + " Wins");
            text[index].SetActive(true);
            index++;
        }
        //Play victory sound
        SoundManager.PlaySound("victory");
    }
}
