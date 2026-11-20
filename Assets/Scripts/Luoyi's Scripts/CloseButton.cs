using UnityEngine;

// Press E near this button to close the window it belongs to (its parent).
// Closing a window counts as one objective for the LevelManager.
public class CloseButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        transform.parent.gameObject.SetActive(false);

        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.CompleteObjective();
        }
    }
}