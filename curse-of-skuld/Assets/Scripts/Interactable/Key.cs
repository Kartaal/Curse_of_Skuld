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
        UIManager.Instance.ClearScreen();
        UIManager.Instance.DisplayAndClearTextAfterDelay(textToDisplayAfterInteraction,4f);
        door.GetComponent<Door>().CanOpen();
        Destroy(this.gameObject);
    }

    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }
}
