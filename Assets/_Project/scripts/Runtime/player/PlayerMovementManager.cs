using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Customization Params")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float stoppingDistance = 0.1f;
    
    [Space]
    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] private bool loopMovement = true;

    private int _currentTargetIndex;
    private bool _isMoving;

    public event Action<int> OnTargetReached;

    private void OnEnable()
    {
        StateManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        StateManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void Update()
    {
        if (StateManager.CurrentState != GameState.IsPlaying)
            return;

        if (_isMoving && targets.Count > 0)
        {
            MoveTowardsTarget();
        }
    }

    [ContextMenu("StartMovement")]
    public void StartMovement()
    {
        if (targets.Count == 0)
        {
            Debug.LogWarning("No targets found.");
            return;
        }

        _currentTargetIndex = 0;
        _isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        Transform currentTarget = targets[_currentTargetIndex];
        if (!currentTarget) return;

        Vector3 targetPos = currentTarget.position;
        Vector3 direction = (targetPos - transform.position);
        direction.y = 0f;

        if (direction.magnitude <= stoppingDistance)
        {
            OnTargetReached?.Invoke(_currentTargetIndex);
            _currentTargetIndex++;

            if (_currentTargetIndex >= targets.Count)
            {
                if (loopMovement)
                    _currentTargetIndex = 0;
                else
                    _isMoving = false;
            }

            return;
        }

        // Move to target
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // Rotate only on Y axis
        RotateTowards(targetPos);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position);
        dir.y = 0f; // resets Y axis diff so it only rotates on other
        if (dir == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Paused)
        {
            _isMoving = false;
        }
        else if (newState == GameState.IsPlaying && targets.Count > 0)
        {
            _isMoving = true;
        }
    }
}