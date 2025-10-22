using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabContentItem : MonoBehaviour
{
    [SerializeField] private Color32 color;
    [SerializeField] private Image background;
    private Renderer _targetRenderer;

    private Toggle _toggle;

    public void Initialize(Renderer target, ToggleGroup group)
    {
        _targetRenderer = target;
        _toggle = GetComponent<Toggle>();
        _toggle.group = group;

        background.color = color;
    }
    
    public void OnClick()
    {
        if (_targetRenderer)
        {
            ColorUtils.ApplyThemeTint(_targetRenderer, color, 1);
        }
    }
}