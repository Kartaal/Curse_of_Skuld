using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private TextMeshProUGUI _noticeText;
    [SerializeField] private GameObject _noteContainer;
    private TextMeshProUGUI _noteText;
    
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
        
        // Yay child object setup!
        _noteText = _noteContainer.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void ClearScreen()
    {
        _noticeText.text = "";
    }

    public void ClearNote()
    {
        _noteText.text = "";
        _noteContainer.SetActive(false);
    }

    public void DisplayTextOnScreen(string textToDisplay)
    {
        _noticeText.text = textToDisplay;
    }
    
    public void DisplayNoteOnScreen(string textToDisplay)
    {
        _noteText.text = textToDisplay;
        _noteContainer.SetActive(true);
    }

    public void DisplayAndClearTextAfterDelay(string textToDisplay,float delay)
    {
        _noticeText.text = textToDisplay;
        StartCoroutine(ShortDelay(delay));
    }

    // Not using the parameter...
    private IEnumerator ShortDelay(float delay)
    {
        yield return new WaitForSeconds(3f);
        _noticeText.text = "";
    }
}
