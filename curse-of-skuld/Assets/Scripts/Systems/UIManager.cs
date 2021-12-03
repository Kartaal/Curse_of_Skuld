using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private TextMeshProUGUI _noticeText;
    [SerializeField] private GameObject _noteContainer;
    private TextMeshProUGUI _noteText;
    [SerializeField] private GameObject _menu;

    [SerializeField] private CinemachineFreeLook playerCamera;
    private float cameraXSpeed;
    private float cameraYSpeed;
    // Have this be accessible from outside if anyone needs to query if the menu is open
    public bool menuOpen = false;
    
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
        _menu.SetActive(false);

        cameraXSpeed = playerCamera.m_XAxis.m_MaxSpeed;
        cameraYSpeed = playerCamera.m_YAxis.m_MaxSpeed;
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
        _menu.SetActive(!_menu.activeSelf);
        
        // Set camera movement speed to 0 (mimic locking)
        if (menuOpen)
        {
            playerCamera.m_XAxis.m_MaxSpeed = 0f;
            playerCamera.m_YAxis.m_MaxSpeed = 0f;
        }
        else
        {   
            playerCamera.m_XAxis.m_MaxSpeed = cameraXSpeed;
            playerCamera.m_YAxis.m_MaxSpeed = cameraYSpeed;
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
