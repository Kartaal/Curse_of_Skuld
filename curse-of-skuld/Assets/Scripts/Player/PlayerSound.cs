using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootSound()
    {
        var instance = RuntimeManager.CreateInstance(AudioManager.instance.footSteps);
        instance.setParameterByName("Material", 0);
        instance.start();
        instance.release();
        //RuntimeManager.PlayOneShot(footSteps, transform.position);
    }
}