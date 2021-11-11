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
    
    [SerializeField]
    private Text text;

    private GameObject _note;
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
        // Really ugly hack but prevents issues of forgetting references in editor
        _note = GameObject.Find("NoteUIContainer").gameObject;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }

    
    //UI manager 
    
    public void ClearScreen()
    {
        text.GetComponent<Text>().text = "";
    }

    public void ClearNote()
    {
        _note.GetComponentInChildren<TextMeshProUGUI>().text = "";
        _note.SetActive(false);
    }

    public void DisplayTextOnScreen(string textToDisplay)
    {
        text.GetComponent<Text>().text = textToDisplay;
    }

    public void DisplayNoteOnScreen(string textToDisplay)
    {
        _note.GetComponentInChildren<TextMeshProUGUI>().text = textToDisplay;
        _note.SetActive(true);
    }

    /* Maybe look into this more if we need multiple page notes
    public void TurnPage()
    {
        var TMP = noteText.GetComponent<TextMeshProUGUI>();
        TMP.
    }
    */

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
