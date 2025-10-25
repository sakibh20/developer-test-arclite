using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementManager playerMovementManager;
    [SerializeField] private string destinationReachedMsg;
    
    [Space]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        InitGameView();
    }

    // Init
    private void OnEnable()
    {
        playerMovementManager.OnTargetReached += HandleOnTargetReached;
        
        startButton.onClick.AddListener(OnClickStart);
        settingsButton.onClick.AddListener(OnClickSettings);
        closeButton.onClick.AddListener(OnClickClose);
    }

    // Cleanup
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnClickStart);
        settingsButton.onClick.RemoveListener(OnClickSettings);
        closeButton.onClick.RemoveListener(OnClickClose);
    }

    // Landing view
    private void InitGameView()
    {
        UpdateSettingsPanelVisibility(false);
        settingsButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    private void HandleOnTargetReached(int targetNo)
    {
        ToastMessage.Instance.ShowToastMessage($"{destinationReachedMsg} {targetNo}");
    }

    [ContextMenu("OnClickStart")]
    private void OnClickStart()
    {
        if(StateManager.CurrentState != GameState.Started) return;
        
        settingsButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        UpdateSettingsPanelVisibility(false);
        
        StateManager.SetState(GameState.IsPlaying);
    }
    
    [ContextMenu("OnClickSettings")]
    private void OnClickSettings()
    {
        UpdateSettingsPanelVisibility(true);
        StateManager.SetState(GameState.Paused);
    }
    
    [ContextMenu("OnClickClose")]
    private void OnClickClose()
    {
        UpdateSettingsPanelVisibility(false);
        StateManager.SetState(GameState.IsPlaying);
    }

    private void UpdateSettingsPanelVisibility(bool activate)
    {
        settingsPanel.SetActive(activate);
    }
}