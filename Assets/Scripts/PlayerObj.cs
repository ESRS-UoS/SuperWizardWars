using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Holds all info needed to join a player
public class PlayerObj
{
    public int PlayerIndex { get; set; }
    public string ControlScheme { get; set; }
    public InputDevice Device { get; set; }
    public int Wins { get; set; }

    public PlayerObj(int playerIndex, string controlScheme, InputDevice device)
    {
        this.PlayerIndex = playerIndex;
        this.ControlScheme = controlScheme;
        this.Device = device;
    }
}
