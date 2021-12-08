using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public void PlayDoorSound()
    {
        RuntimeManager.PlayOneShotAttached(AudioManager.Instance.doorOpen.Guid, this.gameObject);
    }
}
