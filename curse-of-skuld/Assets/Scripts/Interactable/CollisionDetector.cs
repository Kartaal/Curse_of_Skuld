using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Closet") ||collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.gameObject.GetComponent<IInteractable>().Interact();
        }
    }
    // Update is called once per frame
   

    void Update()
    {
        
    }
}
