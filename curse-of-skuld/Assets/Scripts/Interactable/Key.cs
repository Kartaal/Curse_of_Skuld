using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private Material _defaultMat;
    [SerializeField] private Material _pickedUp;

    public void Interact()
    {
        var rend = gameObject.GetComponent<MeshRenderer>();

        if (rend.material == _defaultMat)
        {
            rend.material = _pickedUp;
        }
        else
        {
            rend.material = _defaultMat;
        }
    }
}
