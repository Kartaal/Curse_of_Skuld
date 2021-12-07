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

    public EventReference doorOpen;

    public EventReference closetCreak;

    public EventReference keyPickup;

    public EventReference whispers;

    public EventReference panting;

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
        this._ambienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        RuntimeManager.GetBus("bus:/").setVolume(newMasterVolume);
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

    public void PlayRandomWhispersInBackground(int minSecondsBetween, int maxSecondsBetween)
    {
        StartCoroutine(PlayWhisperSound(minSecondsBetween, maxSecondsBetween));
    }

    private IEnumerator PlayWhisperSound(int minSecondsBetween, int maxSecondsBetween)
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSecondsBetween, maxSecondsBetween));
            RuntimeManager.PlayOneShotAttached(whispers.Path, Camera.main.gameObject);
            yield return new WaitForSeconds(10);
        }
    }
}
