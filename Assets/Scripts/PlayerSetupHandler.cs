using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSetupHandler : MonoBehaviour
{
    //If user presses B or backspace in player setup screen, remove them
    void OnCancel()
    {
        Destroy(gameObject);
    }

    //Move to next scene if user presses start or space
    void OnPause()
    {
        Debug.Log("Players Joined: " + GameMaster.PlayersJoined);
        GameMaster.PlayerObjs = new ArrayList();
        GameMaster.CurrentRound = 0;
        //Only allow it if more than 2 players have joined the game
        if(GameMaster.PlayersJoined > 1)
        {
            //Save input info
            foreach(PlayerInput playerInput in GameMaster.PlayerInputs)
            {
                GameMaster.PlayerObjs.Add(new PlayerObj(playerInput.playerIndex, playerInput.currentControlScheme, playerInput.devices[0]));
            }
            //Reset scores
            //Load rounds menu
            SceneManager.LoadScene("RoundsMenu");
        }
    }
}
