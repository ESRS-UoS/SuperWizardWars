using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Static class holding variables that can be accessed in any scene
public static class GameMaster
{
    public static ArrayList PlayerInputs { get; set; }
    public static ArrayList PlayerObjs { get; set; }
    public static ArrayList PlayersInRound { get; set; }
    public static int RoundWinner { get; set; }
    public static PlayerObj MatchWinner { get; set; }
    public static int PlayersJoined { get; set; }
    public static int TotalRounds { get; set; }
    public static int CurrentRound { get; set; }
    public static bool RoundOver { get; set; }
    public static bool RoundBegun { get; set; }
    public static bool TieBreaker { get; set; }

    //Handles adding rounds in the rounds config menu
    public static void AddRound()
    {
        if(TotalRounds == 10)
        {
            TotalRounds = 10;
        }
        else
        {
            TotalRounds++;
        }
    }

    //Handles removing rounds in the rounds config menu
    public static void SubtractRound()
    {
        if (TotalRounds == 1)
        {
            TotalRounds = 1;
        }
        else
        {
            TotalRounds--;
        }
    }
}
