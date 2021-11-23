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
        gameObject.SetActive(false);
        StartCoroutine(DestroyAfterDelay(1.0f));
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
