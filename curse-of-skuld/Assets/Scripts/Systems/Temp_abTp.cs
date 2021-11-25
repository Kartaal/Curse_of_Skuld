using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_abTp : MonoBehaviour
{
    [SerializeField] Transform startPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.GetComponent<PlayerController>().MoveTo(startPos.position, Vector3.forward);
        }
    }
}
