using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    int _footSoundVariable; //0 is stone, 1 is grass, 2 is wood

    // Start is called before the first frame update
    void Start()
    {
        _footSoundVariable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.0f))
        {
            if (hit.collider.tag == "GroundWood")
            {
                _footSoundVariable = 2;
            }

            if (hit.collider.tag == "GroundStone")
            {
                _footSoundVariable = 0;
            }
        }
    }

    public void PlayFootSound()
    {
        var instance = RuntimeManager.CreateInstance(AudioManager.Instance.footSteps);
        instance.setParameterByName("Material", _footSoundVariable);
        instance.start();
        instance.release();
    }
}
