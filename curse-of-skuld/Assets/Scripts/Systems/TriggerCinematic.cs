using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FMOD;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class TriggerCinematic : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;


    private void Update()
    {
        if (cam.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            cam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            SystemManager.Instance.playerGameObject.GetComponentInChildren<CharacterController>().enabled = true;
            SystemManager.Instance.playerGameObject.GetComponentInChildren<PlayerController>().enabled = true;
            SystemManager.Instance.playerGameObject.GetComponentInChildren<Animator>().enabled = true;
            //SystemManager.Instance.playerGameObject.GetComponentInChildren<MeshRenderer>().enabled = enabled;
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        cam.GetComponent<Camera>().enabled = true;
       // volume.enabled = true;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<CharacterController>().enabled = false;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<PlayerController>().enabled = false;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<Animator>().enabled = false;
     //   SystemManager.Instance.playerGameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        cam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        cam.GetComponent<Animator>().SetTrigger("Play");
    }

 
}
