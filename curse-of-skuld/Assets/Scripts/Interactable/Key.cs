using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject door;
    public void Interact()
    {
        door.GetComponent<Door>().Open();
        Destroy(this.gameObject);
    }
}
