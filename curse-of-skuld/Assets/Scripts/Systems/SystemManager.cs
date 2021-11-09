using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SystemManager : MonoBehaviour
{

    private static SystemManager _instance;
    
    public static SystemManager Instance
    {
        get { return _instance; }
    }

    private Scene _currentScene;
    
    [SerializeField]
    private Text text;
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
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }

    
    //UI manager 
    
    public void ClearScreen()
    {
        text.GetComponent<Text>().text ="";
    }

    public void DisplayTextOnScreen(string textToDisplay)
    {
        text.GetComponent<Text>().text = textToDisplay;
    }

    public void DisplayAndClearTextAfterDelay(string textToDisplay,float Delay)
    {
        text.GetComponent<Text>().text = textToDisplay;
        StartCoroutine(ShortDelay(Delay));
    }

    IEnumerator ShortDelay(float Delay)
    {
        
        yield return new WaitForSeconds(3f);
        text.GetComponent<Text>().text = "";
    }
}