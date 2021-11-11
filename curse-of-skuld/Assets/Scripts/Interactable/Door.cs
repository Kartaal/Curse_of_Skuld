using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour,IInteractable
{
    // Start is called before the first frame update
    private bool _canOpen = false;
    [SerializeField] private string objectName;
    [SerializeField] private string textToDisplayAfterInteraction;
    [SerializeField] private string textToDisplayIfCannotInteract;
    
    public void CanOpen()
    {
        _canOpen = true;
    }

    public void Interact()
    {
        if (_canOpen)
        {
            SystemManager.Instance.ClearScreen();
            SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
            Destroy(this.gameObject);
            // this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            SystemManager.Instance.ClearScreen();
            SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayIfCannotInteract,4f);
        }
    }

    public void DisplayName()
    {
        SystemManager.Instance.DisplayTextOnScreen(objectName);
    }

    
}
