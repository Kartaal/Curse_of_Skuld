using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SystemManager : MonoBehaviour
{

    private static SystemManager _instance;
    public static SystemManager Instance => _instance;

    private Scene _currentScene;
    
   
    public GameObject playerGameObject;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.Instance.StartPlayingAmbience();
    }

    public void ToggleMenuControls()
    {
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        // Might want to pause game time so world state doesn't change while menu is open
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
}
