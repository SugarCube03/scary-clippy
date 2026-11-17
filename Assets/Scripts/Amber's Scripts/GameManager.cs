using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // loads next level of the game
    public void LoadNextLevel(string nextLevelName)
    {
        SceneManager.LoadScene(nextLevelName);
    }


}
