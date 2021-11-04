using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    private GameObject _player;

    //YOU MUST SET PLAYER TAG TO "PLAYER" 
    public void Awake()
    {
        _player=GameObject.FindGameObjectWithTag("Player");
    }

    //Call this function for switching between enable and disable mesh on the character 
    public void Interact()
    {
        _player.gameObject.GetComponent<MeshRenderer>().enabled = !_player.gameObject.GetComponent<MeshRenderer>().enabled;
    }
}
