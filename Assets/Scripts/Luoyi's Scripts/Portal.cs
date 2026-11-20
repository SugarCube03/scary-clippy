using UnityEngine;
using UnityEngine.SceneManagement;

// Touch this to go to the next level.
// Leave "Next Scene" empty to go to the next scene in the Build Profiles list.
public class Portal : MonoBehaviour
{
    public string nextScene = "";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ClippyPlayer>() == null) return;   // only Clippy can use it

        if (nextScene == "")
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}