using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour

   
{
    public static AudioManager instance = null;

    public EventReference footSteps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
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
        var instance = RuntimeManager.CreateInstance(footSteps);
        instance.setParameterByName("Material", 0);
        instance.start();
        instance.release();
        //RuntimeManager.PlayOneShot(footSteps, transform.position);
    }
}
