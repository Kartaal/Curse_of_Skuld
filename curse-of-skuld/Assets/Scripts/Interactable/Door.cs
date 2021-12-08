using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour,IInteractable
{
    // Start is called before the first frame update
    private bool _canOpen = false;
    private bool _isOpen = false;
    [SerializeField] private float openningSpeed;
    [SerializeField] private string objectName;
    public string ObjectName
    {
        get { return objectName; }
    }
    [SerializeField] private string textToDisplayAfterInteraction;
    [SerializeField] private string textToDisplayIfCannotInteract;
    public void CanOpen()
    {
        _canOpen = true;
    }

    public void Awake()
    {
        if (PlayerPrefs.GetInt(objectName) == 1)
        {
            var childrenAnimators = GetComponentsInChildren<Animator>();
            foreach (var anim in childrenAnimators)
            {
                anim.SetTrigger("open");
                anim.SetFloat("speed",openningSpeed);
            }

            _isOpen = true;
        }
    }

    public void Interact()
    {
        if (_canOpen && !_isOpen)
        {
            UIManager.Instance.ClearScreen();
            UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
            var childrenAnimators = GetComponentsInChildren<Animator>();

            foreach (var anim in childrenAnimators)
            {
                anim.SetTrigger("open");
                anim.SetFloat("speed",openningSpeed);
            }

            RuntimeManager.PlayOneShotAttached(AudioManager.Instance.doorUnlock.Guid, this.gameObject);

            PlayerPrefs.SetInt(objectName,1);
            _isOpen = true;
            
            UIManager.Instance.RemoveKeyFromList(objectName);
        }
        else
        {
            UIManager.Instance.ClearScreen();
            UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayIfCannotInteract,4f);
        }

        if (!_canOpen && !_isOpen)
        {
            RuntimeManager.PlayOneShotAttached(AudioManager.Instance.doorLocked.Guid, this.gameObject);
        }
    }

    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }
    
    
}
