using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject controlsParent;
    [SerializeField] private GameObject settingsParent;
    [SerializeField] private GameObject continueParent;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        audioManager.StartPlayingAmbience();
        if (PlayerPrefs.HasKey("PlayedBefore"))
        {
            continueParent.SetActive(true);
            
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayedBefore", 1);
        SceneManager.LoadScene(1);
        
        if(controlsParent.activeSelf)
            continueParent.SetActive(false);
        
        if(settingsParent.activeSelf)
            settingsParent.SetActive(false);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
        if(controlsParent.activeSelf)
            controlsParent.SetActive(false);
        
        if(settingsParent.activeSelf)
            settingsParent.SetActive(false);
    }

    public void ToggleControls()
    {
        if(settingsParent.activeSelf)
            settingsParent.SetActive(false);
        
        controlsParent.SetActive(!controlsParent.activeSelf);
    }

    public void ToggleSettings()
    {
        if(controlsParent.activeSelf)
            controlsParent.SetActive(false);
        settingsParent.SetActive(!settingsParent.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        RuntimeManager.PlayOneShotAttached(audioManager.doorUnlock.Guid, Camera.main.gameObject);
    }

}
