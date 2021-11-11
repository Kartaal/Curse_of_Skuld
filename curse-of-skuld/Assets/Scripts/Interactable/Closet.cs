using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Closet : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    private GameObject player;

    [SerializeField] private string objectName;

    [SerializeField] private string textToDisplayAfterInteraction;

    [SerializeField] private CinemachineVirtualCamera closetCamera;

    [SerializeField] private Canvas closetCanvas;

    private bool _isPlayerInside = false;
    
    //Call this function for switching between enable and disable mesh on the character 
    public void Start()
    {
        player = SystemManager.Instance.playerGameObject;
    }

    public void Interact()
    {
        SystemManager.Instance.ClearScreen();
        _isPlayerInside = !_isPlayerInside;
        
        if (_isPlayerInside)
        {
            SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction, 2f);
            player.GetComponentInChildren<CharacterController>().detectCollisions = false;
        }
        else
        {
            player.GetComponentInChildren<CharacterController>().detectCollisions = true;
        }
        

        var childRenderers = player.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in childRenderers)
        {
            mesh.enabled = !mesh.enabled;
        }
        player.GetComponentInChildren<PlayerController>().ToggleControllerLocked();
        closetCamera.gameObject.SetActive(!closetCamera.gameObject.activeInHierarchy);
        closetCanvas.gameObject.SetActive(!closetCanvas.gameObject.activeInHierarchy);
    }

    public void DisplayName()
    {
       SystemManager.Instance.DisplayTextOnScreen(objectName);
    }
}
