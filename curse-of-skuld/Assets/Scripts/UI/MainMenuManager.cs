using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject controlsParent;
    [SerializeField] private GameObject settingsParent;

    public void StartGame()
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
