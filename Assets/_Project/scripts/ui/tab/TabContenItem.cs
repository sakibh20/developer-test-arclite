using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabContentItem : MonoBehaviour
{
    [SerializeField] private Color32 color;
    [SerializeField] private Image background;
    private TabContext _context;
    private Toggle _toggle;
    
    private Coroutine _colorCoroutine;

    public event Action OnItemSelected;

    // Init
    public void Initialize(TabContext context, ToggleGroup group)
    {
        _context = context;
        _toggle = GetComponent<Toggle>();
        _toggle.group = group;
        background.color = color;
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    
    // Cleanup
    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    // Listens to Toggle value change
    private void OnToggleChanged(bool isOn)
    {
        if (!isOn) return;
        if (_context == null) return;
        
        // Stop any ongoing color transition
        if (_colorCoroutine != null)
            StopCoroutine(_colorCoroutine);

        // Start new color transition
        DynamicColorManager.Instance.ChangeColor(_context.TargetRenderer, color, 0.5f);

        OnItemSelected?.Invoke();
    }

    // Handle on select
    public void Select()
    {
        _toggle.isOn = true;
    }
}