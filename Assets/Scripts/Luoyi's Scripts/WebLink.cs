using UnityEngine;
using UnityEngine.SceneManagement;

// Press E near this link.
// Is Trap ticked   = clicking it kills Clippy (corrupted link)
// Is Trap unticked = opens another scene (e.g. an ending page)
public class WebLink : MonoBehaviour, IInteractable
{
    public bool isTrap = true;
    public string sceneName = "Ending";

    public void Interact()
    {
        if (isTrap)
        {
            FindFirstObjectByType<ClippyPlayer>().Die();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}