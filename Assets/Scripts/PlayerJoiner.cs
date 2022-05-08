using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerJoiner : MonoBehaviour
{
    private ArrayList playerInputs = new ArrayList();
    public GameObject[] playerButtons;
    GameObject currentButton;
    private readonly string text = "Press    or Enter to Join";
    public GameObject startButton;

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Joined PlayerInput ID: " + playerInput.playerIndex);
        currentButton = playerButtons[playerInput.playerIndex];
        //Disable A button image
        currentButton.transform.GetChild(2).gameObject.SetActive(false);
        //Change text in button
        currentButton.transform.GetChild(1).GetComponent<Text>().text = "Joined";
        //Set controller name
        currentButton.transform.GetChild(3).GetComponent<Text>().text = playerInput.currentControlScheme;
        //Add playerInput to arraylist
        playerInputs.Add(playerInput);
        //Update static vars
        GameMaster.PlayersJoined = playerInputs.Count;
        GameMaster.PlayerInputs = playerInputs;
        SoundManager.PlaySound("confirm");
    }

    void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("Left PlayerInput ID: " + playerInput.playerIndex);
        //Reset every button to its default state
        foreach(GameObject obj in playerButtons)
        {
            if(obj != null)
            {
                //Enable A button image
                obj.transform.GetChild(2).gameObject.SetActive(true);
                //Reset text in button
                obj.transform.GetChild(1).GetComponent<Text>().text = text;
                //Remove controller name
                obj.transform.GetChild(3).GetComponent<Text>().text = "";
            }
        }

        //Remove input from arraylist
        playerInputs.Remove(playerInput);

        //Refill buttons based on still connected players
        foreach (PlayerInput obj in playerInputs)
        {
            if(currentButton != null)
            {
                currentButton = playerButtons[obj.playerIndex];
                //Disable A button image
                currentButton.transform.GetChild(2).gameObject.SetActive(false);
                //Change text in button
                currentButton.transform.GetChild(1).GetComponent<Text>().text = "Joined";
                //Set controller name
                currentButton.transform.GetChild(3).GetComponent<Text>().text = obj.currentControlScheme;
            }
        }

        //Update static vars
        GameMaster.PlayersJoined = playerInputs.Count;
        GameMaster.PlayerInputs = playerInputs;
    }

    private void Update()
    {
        //If 2 or more players are in, enable the start prompt
        if(playerInputs.Count > 1)
        {
            startButton.SetActive(true);
        }
        //Otherwise disable the prompt
        else
        {
            startButton.SetActive(false);
        }
    }
}
