using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    [SerializeField] private GameObject _player;


    //Call this function for switching between enable and disable mesh on the character 
    public void Interact()
    {
        var childRenderers = _player.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in childRenderers)
        {
            mesh.enabled = !mesh.enabled;
        }
    }
}
