using UnityEngine;

// Press E near this button to close the window it belongs to (its parent).
public class CloseButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        transform.parent.gameObject.SetActive(false);
    }
}