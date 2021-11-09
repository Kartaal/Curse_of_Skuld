using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject door;

    [SerializeField] private string objectName;
    [SerializeField] private string textToDisplayAfterInteraction;
    public void Interact()
    {
        SystemManager.Instance.ClearScreen();
        SystemManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
        door.GetComponent<Door>().CanOpen();
        Destroy(this.gameObject);
    }

    public void DisplayName()
    {
        SystemManager.Instance.DisplayTextOnScreen(objectName);
    }
}
