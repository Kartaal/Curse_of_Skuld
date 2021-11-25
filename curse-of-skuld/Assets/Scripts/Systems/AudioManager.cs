using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    [SerializeField] private TextMeshProUGUI masterVolumeNumber;

    public EventReference footSteps;

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

    public void MasterVolumeLevel(float newMasterVolume)
    {
        RuntimeManager.GetBus("bus:/").setVolume((newMasterVolume));
        int volNum = (int) (newMasterVolume * 100);
        masterVolumeNumber.text = volNum + "%";
    }
}
