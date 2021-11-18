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
    public static SystemManager Instance
    {
        get { return _instance; }
    }
    
    private Scene _currentScene;
    
   
    public GameObject playerGameObject;
    
    [SerializeField]
    private TextMeshProUGUI UItext;
    [SerializeField] private GameObject _noteContainer;
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

       // var temp = FindObjectOfType<PlayerController>();
       //  _playerGameObject = temp.gameObject;
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }

    
    //UI manager 
    
    public void ClearScreen()
    {
        UItext.text ="";
    }

    public void ClearNote()
    {
        _noteContainer.GetComponentInChildren<TextMeshProUGUI>().text = "";
        _noteContainer.SetActive(false);
    }

    public void DisplayTextOnScreen(string textToDisplay)
    {
        UItext.text = textToDisplay;
    }
    
    public void DisplayNoteOnScreen(string textToDisplay)
    {
        _noteContainer.GetComponentInChildren<TextMeshProUGUI>().text = textToDisplay;
        _noteContainer.SetActive(true);
    }

    public void DisplayAndClearTextAfterDelay(string textToDisplay,float Delay)
    {
        UItext.text = textToDisplay;
        StartCoroutine(ShortDelay(Delay));
    }

    IEnumerator ShortDelay(float Delay)
    {
        
        yield return new WaitForSeconds(3f);
        UItext.text = "";
    }
}
