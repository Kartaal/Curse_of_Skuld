using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using TMPro;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    [SerializeField] private TextMeshProUGUI masterVolumeNumber;

    public EventReference footSteps;

    public EventReference ambience;

    public EventReference monsterPassive;

    public EventReference monsterAlert;

    private EventInstance _ambienceInstance;

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
    }

    private void OnDestroy()
    {
        print("in destroy");
        this._ambienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        RuntimeManager.GetBus("bus:/").setVolume((newMasterVolume));
        int volNum = (int) (newMasterVolume * 100);
        masterVolumeNumber.text = volNum + "%";
    }

    public void StartPlayingAmbience()
    {
        this._ambienceInstance = FMODUnity.RuntimeManager.CreateInstance(ambience);
        _ambienceInstance.setParameterByName("AmbienceParameters", 1);
        _ambienceInstance.start();
    }

    public void StopPlayingAmbience()
    {
        this._ambienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopPlayingAmbienceWithFadeout()
    {
        _ambienceInstance.setParameterByName("AmbienceParameters", 2);
        _ambienceInstance.release();
    }
}
