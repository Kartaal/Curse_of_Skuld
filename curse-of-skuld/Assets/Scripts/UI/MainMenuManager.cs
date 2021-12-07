using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject controlsParent;
    [SerializeField] private GameObject settingsParent;
    [SerializeField] private GameObject continueParent;

    private void Awake()
    {
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
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleControls()
    {
        controlsParent.SetActive(!controlsParent.activeSelf);
    }

    public void ToggleSettings()
    {
        settingsParent.SetActive(!settingsParent.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
