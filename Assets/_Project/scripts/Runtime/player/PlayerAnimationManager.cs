using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _playerAnimator;
    private static readonly int IsRun = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StateManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        StateManager.OnGameStateChanged -= HandleGameStateChanged;
    }
    
    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Paused)
        {
            PlayIdle();
        }
        else if (newState == GameState.IsPlaying)
        {
            PlayRun();
        }
    }

    private void PlayIdle()
    {
        _playerAnimator.SetBool(IsRun, false);
    }
    
    private void PlayRun()
    {
        _playerAnimator.SetBool(IsRun, true);
    }
}