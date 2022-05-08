using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Handles moving to stages
public class MoveToStage : MonoBehaviour
{
    public string stage;

    public void LoadStage()
    {
        SceneManager.LoadScene(stage);
    }
}
