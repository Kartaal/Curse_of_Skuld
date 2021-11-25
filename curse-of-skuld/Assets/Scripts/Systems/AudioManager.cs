using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public EventReference footSteps;

    /* Commented out for now to avoid warnings in editor
    private FMOD.Studio.Bus Master;
    private float MasterVolume = 1.0f;
    */

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        //Master = FMODUnity.RuntimeManager.GetBus("bus:/")
    }
}
