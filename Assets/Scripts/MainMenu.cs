using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu, optionsMenu, optionsMainButtons, optionsGraphicsScreen, optionsSoundScreen;

    public GameObject firstOptionMainMenu, firstOptionOptionsMenu, closedButtonOptionsMenu, firstOptionsGraphics, firstOptionSound;

    public Toggle fullscreenToggle, vsyncToggle;

    public List<ResItem> resolutions = new List<ResItem>();

    private int selectedResolutionValue;

    public TMP_Text resolutionLabel, masterLabel, musicLabel, SFXLabel;

    public Slider mastSlider, musicSlider, sfxSlider;

    public AudioMixer audioMixer;

    public void QuitGame ()
    {
        Debug.Log("Application closed");
        Application.Quit();
    }

    public void PlayLocalGame ()
    {
        PlayerPrefs.SetInt("GameType", 0);
        SceneManager.LoadScene("PlayerSetup");
    }

    public void PlayOnlineGame()
    {
        PlayerPrefs.SetInt("GameType", 1);
        SceneManager.LoadScene(0);
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("ControllerButtons");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        optionsGraphicsScreen.SetActive(false);
        optionsSoundScreen.SetActive(false);
        optionsMainButtons.SetActive(true);
        mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionOptionsMenu);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        optionsGraphicsScreen.SetActive(false);
        optionsSoundScreen.SetActive(false);
        optionsMainButtons.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closedButtonOptionsMenu);
    }

    public void OpenGraphicsScreen()
    {
        optionsGraphicsScreen.SetActive(true);
        optionsSoundScreen.SetActive(false);
        optionsMainButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsGraphics);
    }

    public void OpenSoundScreen()
    {
        optionsGraphicsScreen.SetActive(false);
        optionsSoundScreen.SetActive(true);
        optionsMainButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionSound);
    }

    public void OpenMainMenuScreen()
    {
        optionsGraphicsScreen.SetActive(false);
        optionsSoundScreen.SetActive(false);
        optionsMainButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionOptionsMenu);
    }

    public void ResLeft()
    {
        selectedResolutionValue--;
        if (selectedResolutionValue == -1)
        {
            selectedResolutionValue = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolutionValue++;
        if (selectedResolutionValue >= resolutions.Count)
        {
            selectedResolutionValue = 0;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolutionValue].horizontal.ToString() + " X " + resolutions[selectedResolutionValue].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenToggle.isOn;
        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        } else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolutionValue].horizontal, resolutions[selectedResolutionValue].vertical, fullscreenToggle.isOn);

        optionsGraphicsScreen.SetActive(false);
        optionsMainButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionOptionsMenu);
    }

    public void SetMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();

        audioMixer.SetFloat("MasterVol", mastSlider.value);

        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
    }

    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        audioMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        SFXLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        audioMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void ApplySoundSettings()
    {
        optionsSoundScreen.SetActive(false);
        optionsMainButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionOptionsMenu);
    }

    void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        optionsMainButtons.SetActive(false);
        optionsGraphicsScreen.SetActive(false);
        optionsSoundScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionMainMenu);
        fullscreenToggle.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        } else
        {
            vsyncToggle.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolutionValue = i;
                UpdateResLabel();
            }
        }
        if (!foundRes)
        {
            ResItem resItem = new ResItem();
            resItem.horizontal = Screen.width;
            resItem.vertical = Screen.height;
            resolutions.Add(resItem);
            selectedResolutionValue = resolutions.Count - 1;
            UpdateResLabel();
        }

        float vol = 0f;
        audioMixer.GetFloat("MasterVol", out vol);
        mastSlider.value = vol;
        audioMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        audioMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        SFXLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
