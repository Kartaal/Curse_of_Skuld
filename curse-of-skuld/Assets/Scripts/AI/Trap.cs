using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float _deathDelay;

    private void OnTriggerEnter(Collider other)
    {
        var playercController = other.GetComponent<PlayerController>();
        if (playercController!=null)
        {
            playercController.Trapped();
            //StartCoroutine(MoveSpike(other));
            GetComponentInChildren<Spike>().Move();
        }
    }

    IEnumerator MoveSpike(Collider other)
    {
        yield return new WaitForSeconds(_deathDelay);
        print("You Died");
        
        
    }
}
