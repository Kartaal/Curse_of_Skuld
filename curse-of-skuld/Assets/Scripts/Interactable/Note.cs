using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] [Multiline] private string textToDisplayAfterInteraction;
    private bool _displayingNoteUI = false;

    private void Awake()
    {
        // Ensure note UI isn't displayed, bad because of finding by name...
        GameObject.Find("NoteUIContainer").SetActive(false);
    }

    public void Interact()
    {
        if (!_displayingNoteUI)
        {
            SystemManager.Instance.DisplayNoteOnScreen(textToDisplayAfterInteraction);
            _displayingNoteUI = true;
        }
        else
        {
            SystemManager.Instance.ClearNote();
            _displayingNoteUI = false;
        }
    }

    public void DisplayName()
    {
        SystemManager.Instance.DisplayTextOnScreen(objectName);
    }
}