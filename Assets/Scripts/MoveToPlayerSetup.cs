using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToPlayerSetup : MonoBehaviour
{
    //Moves to Player setup scene
    public void moveScene ()
    {
        SceneManager.LoadScene("PlayerSetup");
    }
}
