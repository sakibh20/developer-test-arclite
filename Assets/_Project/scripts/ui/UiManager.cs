using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    
    [ContextMenu("OnClickStart")]
    private void OnClickStart()
    {
        StateManager.SetState(GameState.IsPlaying);
    }
    
    [ContextMenu("OnClickSettings")]
    private void OnClickSettings()
    {
        StateManager.SetState(GameState.Paused);
    }
    
    [ContextMenu("OnClickClose")]
    private void OnClickClose()
    {
        StateManager.SetState(GameState.IsPlaying);
    }
}