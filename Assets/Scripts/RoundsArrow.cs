using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the arrow buttons on the rounds menu
public class RoundsArrow : MonoBehaviour
{
    public void AddRound()
    {
        GameMaster.AddRound();
    }

    public void SubtractRound()
    {
        GameMaster.SubtractRound();
    }
}
