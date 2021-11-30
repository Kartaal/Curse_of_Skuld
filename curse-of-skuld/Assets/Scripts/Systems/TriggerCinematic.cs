using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TriggerCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector timeline;
    
    public Camera cam;
    // Use this for initialization
    void Awake()
    {
       // timeline = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        cam.GetComponent<Camera>().enabled = true;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<CharacterController>().enabled = false;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        cam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        // timeline.Play();

    }
}
