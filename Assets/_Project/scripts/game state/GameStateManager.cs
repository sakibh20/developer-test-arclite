using System;
using UnityEngine;

public enum GameState
{
    Undefined,
    Started,
    Paused,
    IsPlaying,
    Ended
}

public class StateManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; } = GameState.Paused;
    public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        SetState(GameState.Started);
    }

    public static void SetState(GameState newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}