using System;
using UnityEngine;

public enum GameState
{
    Paused,
    IsPlaying
}

public class StateManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; } = GameState.Paused;
    public static event Action<GameState> OnGameStateChanged;

    public static void SetState(GameState newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}