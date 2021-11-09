using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        var interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            collision.gameObject.GetComponent<IInteractable>().Interact();
        }
    }

  
}
