using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWinScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string newSceneName;
    public void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(newSceneName);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
