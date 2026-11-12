using UnityEngine;

// controlls what the player sees on screen
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // different states' panels
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;

    private void OnEnable()
    {
        // subscribe state changing function to state changing event
        gameManager.OnGameStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        // unsubscribe state changing function to state changing event
        gameManager.OnGameStateChanged -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState newState)
    {
        // disable all four
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);

        // based on state change, change what is displayed
        if (newState == GameState.MainMenu)
        {
            mainMenuPanel.SetActive(true);

        }
        else if (newState == GameState.Paused)
        {
            pauseMenuPanel.SetActive(true);
        }
        else if (newState == GameState.GameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else if (newState == GameState.Victory)
        {
            victoryPanel.SetActive(true);
        }
    }

    private void Start()
    {
        // start on main menu panel
        HandleGameStateChange(gameManager.CurrentState());
    }
}
