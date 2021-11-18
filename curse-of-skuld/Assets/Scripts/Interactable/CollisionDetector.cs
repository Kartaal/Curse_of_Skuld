using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private static CollisionDetector _instance;
    public static CollisionDetector Instance
    {
        get { return _instance; }
    }

    private bool _isInCollider;
    
    private GameObject _other;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _isInCollider = false;
    }
    public void InteractionKeyPressed()
    { 
        if (_isInCollider&&_other)
        {
            _other.GetComponent<IInteractable>().Interact();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>()!=null&&collision.gameObject!=null)
        {
            collision.gameObject.GetComponent<IInteractable>().DisplayName();
            _isInCollider = true;
            _other = collision.gameObject;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>()!=null&&collision.gameObject!=null)
        {
            UIManager.Instance.ClearScreen();
            UIManager.Instance.ClearNote();
            _isInCollider = false;
            _other = null;
        }
    }
}
