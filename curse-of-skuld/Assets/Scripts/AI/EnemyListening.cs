using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListening : MonoBehaviour
{
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _enemy.PlayerHeard(SystemManager.Instance.playerGameObject.transform.position);
        }
    }
}
