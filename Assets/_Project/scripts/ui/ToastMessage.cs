using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class ToastMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    [Space]
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float floatUpDistance = 10f;
    [SerializeField] private float showDuration = 2f;

    private Vector3 _initialPosition;
    private Coroutine _activeCoroutine;
    
    private CanvasGroup _toastPanel;

    public static ToastMessage Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        _toastPanel = GetComponent<CanvasGroup>();
        _initialPosition = _toastPanel.transform.localPosition;
        _toastPanel.alpha = 0f;
    }

    public void ShowToastMessage(string message)
    {
        // If another toast is running, stop it first
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
            HideCurrentMessage();
        }

        messageText.text = message;
        _activeCoroutine = StartCoroutine(PlayToastAnimation());
    }

    private IEnumerator PlayToastAnimation()
    {
        _toastPanel.alpha = 0f;
        _toastPanel.transform.localPosition = _initialPosition;

        // Step 1: Fade In
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _toastPanel.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        _toastPanel.alpha = 1f;

        // Step 2: Float up & Fade out simultaneously
        timer = 0f;
        Vector3 startPos = _initialPosition;
        Vector3 endPos = _initialPosition + Vector3.up * floatUpDistance;

        while (timer < showDuration)
        {
            timer += Time.deltaTime;
            float t = timer / showDuration;

            // move upward
            _toastPanel.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            // fade out gradually (start fading out immediately)
            _toastPanel.alpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        HideCurrentMessage();
        _activeCoroutine = null;
    }

    private void HideCurrentMessage()
    {
        _toastPanel.alpha = 0f;
        _toastPanel.transform.localPosition = _initialPosition;
    }
}