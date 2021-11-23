using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    
    [SerializeField]
    private bool canStuck;
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController!=null)
        {
            if (canStuck)
            {
                playerController.Trapped();
            }
            GetComponentInChildren<Spike>().Move();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<Spike>().Stop();
    }
}
