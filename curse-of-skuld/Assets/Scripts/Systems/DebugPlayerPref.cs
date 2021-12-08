using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool deleteAll;
    [SerializeField]
    private string key;
    [SerializeField]
    private bool deleteKey;
    [SerializeField]
    private string secondKey;
    [SerializeField]
    private float value;
    [SerializeField] 
    private bool changeKey;
    
    private Scene _currentScene;
    void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        if (deleteAll)
        {
            PlayerPrefs.DeleteAll();
        }

        if (deleteKey)
        {
            PlayerPrefs.DeleteKey(key);
        }

        if (changeKey)
        {
            PlayerPrefs.SetFloat(secondKey,value);
        }
    }
    void OnDebugRestart()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
