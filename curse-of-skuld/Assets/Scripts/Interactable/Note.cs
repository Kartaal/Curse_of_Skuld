using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] [Multiline] private string textToDisplayAfterInteraction;
    private bool _displayingNoteUI = false;

    public void Interact()
    {
        if (!_displayingNoteUI)
        {
            UIManager.Instance.DisplayNoteOnScreen(textToDisplayAfterInteraction);
            _displayingNoteUI = true;
        }
        else
        {
            UIManager.Instance.ClearNote();
            _displayingNoteUI = false;
        }
    }

    public void DisplayName()
    {
        UIManager.Instance.DisplayTextOnScreen(objectName);
    }
}