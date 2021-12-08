using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [SerializeField] private AudioManager audioManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShotAttached(audioManager.doorUnlock.Guid, Camera.main.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //RuntimeManager.PlayOneShotAttached(audioManager.doorLocked.Guid, Camera.main.gameObject);
    }
}
