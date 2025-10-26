using System.Collections.Generic;
using UnityEngine;

public class TargetFeeder : MonoBehaviour
{
    [SerializeField] private PlayerMovementManager playerMovementManager;
    [SerializeField] private List<Transform> targets = new();
    
    [SerializeField] private string destinationReachedMsg;
    [SerializeField] private bool loopTargets = true;

    private int _currentIndex;

    private void OnEnable()
    {
        if (playerMovementManager == null) return;

        StateManager.OnGameStateChanged += HandleOnGameStateChanged;
        playerMovementManager.OnTargetReached += HandleTargetReached;
    }

    private void OnDisable()
    {
        if (playerMovementManager == null) return;

        StateManager.OnGameStateChanged += HandleOnGameStateChanged;
        playerMovementManager.OnTargetReached -= HandleTargetReached;
    }

    private void Start()
    {
        _currentIndex = 0;
    }

    private void HandleOnGameStateChanged(GameState currentState)
    {
        if (currentState == GameState.IsPlaying)
        {
            FeedNextTarget();
        }
    }

    private void HandleTargetReached()
    {
        ToastMessage.Instance.ShowToastMessage($"{destinationReachedMsg} {_currentIndex-1}");
        
        FeedNextTarget();
    }

    private void FeedNextTarget()
    {
        if (targets == null || targets.Count == 0)
        {
            Debug.LogWarning("TargetFeeder: No targets available.");
            playerMovementManager.SetNextTarget(null);
            return;
        }

        if (_currentIndex >= targets.Count)
        {
            if (loopTargets)
                _currentIndex = 0;
            else
            {
                Debug.Log("TargetFeeder: Completed all targets.");
                playerMovementManager.SetNextTarget(null);
                return;
            }
        }

        var nextTarget = targets[_currentIndex];
        _currentIndex++;

        playerMovementManager.SetNextTarget(nextTarget);
    }
}