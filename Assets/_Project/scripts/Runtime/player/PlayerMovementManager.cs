using UnityEngine;
using System;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Customization Params")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float stoppingDistance = 0.1f;

    private Transform _currentTarget;
    private bool _isMoving;

    public event Action OnTargetReached;

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
        if (StateManager.CurrentState != GameState.IsPlaying || !_isMoving || !_currentTarget)
            return;

        MoveTowardsTarget();
    }

    public void SetNextTarget(Transform nextTarget)
    {
        _currentTarget = nextTarget;

        if (_currentTarget)
        {
            _isMoving = true;
            MoveTowardsTarget();
        }
        
        else _isMoving = false;
    }

    private void MoveTowardsTarget()
    {
        Vector3 targetPos = _currentTarget.position;
        Vector3 direction = (targetPos - transform.position);
        direction.y = 0f;

        if (direction.magnitude <= stoppingDistance)
        {
            _isMoving = false;
            OnTargetReached?.Invoke();
            return;
        }

        // Move
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // Rotate only on Y axis
        RotateTowards(targetPos);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position);
        dir.y = 0f;
        if (dir == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Paused)
            _isMoving = false;
        else if (newState == GameState.IsPlaying && _currentTarget != null)
            _isMoving = true;
    }
}