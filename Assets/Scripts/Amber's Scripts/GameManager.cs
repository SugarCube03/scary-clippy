using UnityEngine;
using System;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
    private GameState currentState;

    public event Action<GameState> OnGameStateChanged;

    // for other scripts to check the state we are in
    public GameState CurrentState()
    {
        return currentState;
    }

    // update current state and notify subscribers
    public void ChangeState(GameState newState)
    {
        // avoids unnecessary reassignment
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // should start off at the main menu
        currentState = GameState.MainMenu;
    }
}
