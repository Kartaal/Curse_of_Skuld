using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetFloat("PositionX",transform.position.x);
        PlayerPrefs.SetFloat("PositionY",transform.position.y);
        PlayerPrefs.SetFloat("PositionZ",transform.position.z);
    }
}
