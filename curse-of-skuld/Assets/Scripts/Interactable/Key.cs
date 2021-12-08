using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;

    [SerializeField] private string objectName;
    [SerializeField] private string textToDisplayAfterInteraction;

    public void Awake()
    {
        if (PlayerPrefs.GetInt(door.GetComponent<Door>().ObjectName) == 1)
        {
            this.gameObject.SetActive(false); 
        }
        
    }

    public void Interact()
    {
        if(!gameObject.activeSelf)
            return;
        
        UIManager.Instance.ClearScreen();
        UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
        door.GetComponent<Door>().CanOpen();
        RuntimeManager.PlayOneShotAttached(AudioManager.Instance.keyPickup.Guid, this.gameObject);
        gameObject.SetActive(false);
        
        UIManager.Instance.AddKeyToList(door.GetComponent<Door>().ObjectName, this.gameObject.name);

        //why destroy? also has a bug 
        // StartCoroutine(DestroyAfterDelay(1.0f));
    }

    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
