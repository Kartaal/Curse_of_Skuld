using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private GameObject noteContainer;
    private TextMeshProUGUI _noteText;
    [SerializeField] private GameObject menu;

    [SerializeField] private CinemachineFreeLook playerCamera;
    private float _cameraXSpeed;
    private float _cameraYSpeed;
    // Have this be accessible from outside if anyone needs to query if the menu is open
    public bool menuOpen = false;

    [SerializeField] private GameObject keyListContainer;
    private Dictionary<string,string> _heldKeys = new Dictionary<string, string>();
    private TextMeshProUGUI _keyList;
    
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
        _noteText = noteContainer.GetComponentInChildren<TextMeshProUGUI>();
        menu.SetActive(false);

        _cameraXSpeed = playerCamera.m_XAxis.m_MaxSpeed;
        _cameraYSpeed = playerCamera.m_YAxis.m_MaxSpeed;

        _keyList = keyListContainer.GetComponentInChildren<TextMeshProUGUI>();
        keyListContainer.SetActive(false);
    }

    // Hacky solution to keep cursor visible while menu is open
    // opening the menu sets timescale to 0, closing sets it to 1
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    // Toggle menu and pause/unpause the game
    public void OnOpenEscapeMenu()
    {
        menuOpen = !menuOpen;
        // Toggle cursor lock so menu can be clicked
        SystemManager.Instance.ToggleCursorLock();
        menu.SetActive(!menu.activeSelf);
        
        // Set camera movement speed to 0 (mimic locking)
        if (menuOpen)
        {
            playerCamera.m_XAxis.m_MaxSpeed = 0f;
            playerCamera.m_YAxis.m_MaxSpeed = 0f;
        }
        else
        {   
            playerCamera.m_XAxis.m_MaxSpeed = _cameraXSpeed;
            playerCamera.m_YAxis.m_MaxSpeed = _cameraYSpeed;
        }

        // Pause game time while menu is open
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    
    public void ClearScreen()
    {
        noticeText.text = "";
    }

    public void ClearNote()
    {
        _noteText.text = "";
        noteContainer.SetActive(false);
    }

    public void DisplayTextOnScreen(string textToDisplay)
    {
        noticeText.text = textToDisplay;
    }
    
    public void DisplayNoteOnScreen(string textToDisplay)
    {
        _noteText.text = textToDisplay;
        noteContainer.SetActive(true);
    }

    public void DisplayAndClearTextAfterDelay(string textToDisplay,float delay)
    {
        noticeText.text = textToDisplay;
        StartCoroutine(ShortDelay(delay));
    }

    // Not using the parameter...
    private IEnumerator ShortDelay(float delay)
    {
        yield return new WaitForSeconds(3f);
        noticeText.text = "";
    }

    // id is the associated door object's object name, mimics the PlayerPref system
    public void AddKeyToList(string id, string keyName)
    {
        _heldKeys.Add(id, keyName);

        UpdateKeyListText();
        if (_heldKeys.Count != 0)
        {
            keyListContainer.SetActive(true);
        }
    }

    public void RemoveKeyFromList(string id)
    {
        _heldKeys.Remove(id);

        UpdateKeyListText();
        if (_heldKeys.Count == 0)
        {
            keyListContainer.SetActive(false);
        }
    }

    private void UpdateKeyListText()
    {
        var keys = string.Join("\n", _heldKeys.Values);
        _keyList.text = keys;
    }
}
