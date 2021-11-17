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
        SystemManager.Instance.ClearScreen();
        SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
        SceneManager.LoadScene("Scene_Win");
      
    }
    public void DisplayName()
    {
        SystemManager.Instance.DisplayTextOnScreen(objectName);
    }
}
