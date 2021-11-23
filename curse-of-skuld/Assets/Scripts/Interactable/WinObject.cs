using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinObject : MonoBehaviour,IInteractable
{
   
    [SerializeField] private string objectName;
    [SerializeField] private string textToDisplayAfterInteraction;
    
    public void Interact()
    {
        UIManager.Instance.ClearScreen();
        UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
        SceneManager.LoadScene("Scene_Win");
      
    }
    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }
}
