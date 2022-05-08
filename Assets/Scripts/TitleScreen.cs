using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject audioManager;
    //Take user to main menu on pressing confirm
    public void OnButtonClick()
    {
        SoundManager.PlaySound("confirm");
        DontDestroyOnLoad(audioManager);
        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {
        if (1.0f / Time.smoothDeltaTime < 60)
        {
            Application.targetFrameRate = 60;
        }
    }
}
