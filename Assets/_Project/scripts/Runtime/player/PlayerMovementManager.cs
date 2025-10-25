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

    private Vector3 _targetPos;
    private Vector3 _direction;

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
        _targetPos = _currentTarget.position;
        _direction = _targetPos - transform.position;
        _direction.y = 0f;

        if (_direction.magnitude <= stoppingDistance)
        {
            _isMoving = false;
            OnTargetReached?.Invoke();
            return;
        }

        // Move
        transform.position += _direction.normalized * moveSpeed * Time.deltaTime;

        // Rotate only on Y axis
        RotateTowards();
    }

    private void RotateTowards()
    {
        if (_direction == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(_direction);
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