using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Closet : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;

    [SerializeField] private string objectName;

    [SerializeField] private string textToDisplayAfterInteraction;

    private bool _isPlayerInside = false;
    
    //Call this function for switching between enable and disable mesh on the character 
    public void Interact()
    {
        SystemManager.Instance.ClearScreen();
        _isPlayerInside = !_isPlayerInside;
        if(_isPlayerInside)
            SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,2f);
        var childRenderers = player.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in childRenderers)
        {
            mesh.enabled = !mesh.enabled;
        }
    }

    public void DisplayName()
    {
       SystemManager.Instance.DisplayTextOnScreen(objectName);
    }
}
