using UnityEngine;
using UnityEngine.SceneManagement;


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
        AudioManager.Instance.PlayRandomWhispersInBackground(60, 180);
        Time.timeScale = 1f;
    }

    public void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(_currentScene.name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
