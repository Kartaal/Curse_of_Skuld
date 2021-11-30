using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    
    [SerializeField] Transform startPos;

    void OnTeleportDebug()
    {
        SystemManager.Instance.playerGameObject.GetComponent<PlayerController>().MoveTo(startPos.position, Vector3.forward);
    }
}
