using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public EventReference footSteps;

    // These fields are taken from https://scottgamesounds.com/wp-content/uploads/2018/12/C.AudioSettings.txt
    FMOD.Studio.Bus Master;
    float MasterVolume = 1f;
    

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
        
        // This initialisation is taken from https://scottgamesounds.com/wp-content/uploads/2018/12/C.AudioSettings.txt
        Master = FMODUnity.RuntimeManager.GetBus ("bus:/");
    }
    
    // Functions after this line are taken from https://scottgamesounds.com/wp-content/uploads/2018/12/C.AudioSettings.txt
    void Update () 
    {
        Master.setVolume (MasterVolume);
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        print("Volume now: " +MasterVolume);
        MasterVolume = newMasterVolume;
    }
}
