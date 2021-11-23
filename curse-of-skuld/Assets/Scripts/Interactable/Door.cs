using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour,IInteractable
{
    // Start is called before the first frame update
    private bool _canOpen = false;
    [SerializeField] private float openningSpeed;
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
            UIManager.Instance.ClearScreen();
            UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
            var childrenAnimators = GetComponentsInChildren<Animator>();
            foreach (var anim in childrenAnimators)
            {
                anim.SetTrigger("open");
                anim.SetFloat("Speed",openningSpeed);
            }
            //Destroy(this.gameObject);
            // this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            UIManager.Instance.ClearScreen();
            UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayIfCannotInteract,4f);
        }
    }

    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }

    
}
