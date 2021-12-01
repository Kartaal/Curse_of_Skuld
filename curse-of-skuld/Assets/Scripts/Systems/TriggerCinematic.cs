using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TriggerCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector timeline;
    
    public Camera cam;
    [SerializeField]
    private Volume postProcessing;

    private Vignette _vignette;
    // Use this for initialization
    void Awake()
    {
        postProcessing.profile.TryGet<Vignette>(out _vignette);
       // postProcessing.enabled = false;
    }

    private void Update()
    {
        if (cam.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dummy"))
        {
            print("fuck U");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        cam.GetComponent<Camera>().enabled = true;
        postProcessing.enabled = true;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<CharacterController>().enabled = false;
        SystemManager.Instance.playerGameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
       // _vignette.intensity.value = math.lerp(_vignette.intensity.value, 1, Time.deltaTime * 0.2f);
        cam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        cam.GetComponent<Animator>().SetTrigger("Play");
        // timeline.Play();
    }
    bool AnimatorIsPlaying(){
        return cam.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
               cam.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
