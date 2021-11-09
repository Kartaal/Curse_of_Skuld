using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SystemManager : MonoBehaviour
{

    private static SystemManager _instance;
    
    public static SystemManager Instance
    {
        get { return _instance; }
    }

    private Scene _currentScene;
    
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
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
}
