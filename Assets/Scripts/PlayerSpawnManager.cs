using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public PlayerInputManager playerInputManager;

    public void StartRound()
    {
        playerInputManager = GetComponent<PlayerInputManager>();

        //For each joined player
        foreach(PlayerObj playerObj in GameMaster.PlayerObjs)
        {
            //Join each player to current scene
            playerInputManager.JoinPlayer(playerObj.PlayerIndex, -1, playerObj.ControlScheme, playerObj.Device);
            //Add each player to current players in round
            GameMaster.PlayersInRound.Add(playerObj);
        }
        GameMaster.RoundBegun = true;
    }

    void OnPlayerJoined(PlayerInput playerInput) {
        
        Debug.Log("PlayerInput ID: " + playerInput.playerIndex);

        // Set the player ID, add one to the index to start at Player 1
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;

        // Set the start spawn position of the player using the location at the associated element into the array.
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[playerInput.playerIndex].position;

        if(GameMaster.TieBreaker)
        {
            //Give every player a sword if it's a tiebreaker
            playerInput.gameObject.GetComponent<Weapon>().weapon = 1;
        }
    }

    //Handles what to do when a player dies
    void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("PlayerInput ID: " + playerInput.playerIndex + " Died");
        //Remove self from players left in round list
        foreach(PlayerObj playerObj in GameMaster.PlayersInRound)
        {
            if(playerObj.PlayerIndex == playerInput.playerIndex)
            {
                GameMaster.PlayersInRound.Remove(playerObj);
                break;
            }
        }
    }
}